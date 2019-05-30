using UnityEngine;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using System.Collections;
namespace Commando
{ 
    namespace Terrain
    {
        public class Generator
        {
            private EventComponent eventComponent;
            private EntityComponent entityComponent;
            private ArrayList blocks = new ArrayList();
            private ArrayList water_blocks = new ArrayList();
            private int BatchBegin = 0;
            private int PerlinNoise(int x,int y,int maxType,float relief,float seedX ,float seedY )
            {
                float t = 0;
                float xSample = (x + seedX) / relief;
                float ySample = (y+ seedY) / relief;
                float noise = Mathf.PerlinNoise(xSample, ySample);
                t = maxType * noise;
                t = Mathf.Round(t);
                return (int)t;
            }
            public Generator()
            {
                int width = Config.Width;
                int height = Config.Height;
                float seedX = RandomHeper.NewValue();
                float seedY = RandomHeper.NewValue();
                float HillsX = RandomHeper.NewValue();
                float HillsY = RandomHeper.NewValue();
                float GrasslandX = RandomHeper.NewValue();
                float GrasslandY = RandomHeper.NewValue();
                float ForestX = RandomHeper.NewValue();
                float ForestY = RandomHeper.NewValue();
                float WetlandsX = RandomHeper.NewValue();
                float WetlandsY = RandomHeper.NewValue();
                float DesertX = RandomHeper.NewValue();
                float DesertY = RandomHeper.NewValue();
                float SnowfieldX = RandomHeper.NewValue();
                float SnowfieldY = RandomHeper.NewValue();
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int height_type = PerlinNoise(x,y,2,15.0f,seedX,seedY);
                        var entity_id = EntityHelper.EntityCreate.GenerateId();
                        float dy = 0.0f;
                        HeightType _heightType = HeightType.None;
                        Type _type = Type.None;
                        string asset_name = "";
                        string sprite_str = "GoldenSkullStudios/Grass/ISO_Tile_Dirt_01_Grass_01";
                        int id = y*width+x;
                        if (height_type == 0&&x!=0&&x!=width-1&&y!=0&&y!=height-1)
                        {
                            int montain_height = PerlinNoise(x,y,5,15.0f,seedX,seedY);
                            if(montain_height ==1)montain_height=0;
                            else montain_height =1;
                            dy = montain_height*0.7f+0.7f;
                            _heightType = HeightType.Highland;
                            _type = Type.Hills;
                            asset_name = "Assets/Prefab/Terrain/Mountain.prefab";
                        }
                        else if (height_type == 1||x==0||x==width-1||y==0||y==height-1)
                        {
                            dy = 0f;
                            _heightType = HeightType.Plain;
                            _type = Type.Grassland;
                            height_type = 1;
                            asset_name = "Assets/Prefab/Terrain/Ground.prefab";
                        }
                        else
                        {
                            dy = -0.7f;
                            _heightType = HeightType.Low;
                            _type = Type.Water;
                            water_blocks.Add(id);
                            asset_name = "Assets/Prefab/Terrain/Water.prefab";
                        }

                        if(height_type==0|| height_type==1)
                        {
                            int g_type = PerlinNoise(x, y, 2, 50, GrasslandX, GrasslandY);
                            int h_type = PerlinNoise(x, y, 2, 49, HillsX, HillsY);
                            int f_type = PerlinNoise(x, y, 2, 48, ForestX, ForestY);
                            int w_type = PerlinNoise(x, y, 2, 47, WetlandsX, WetlandsY);
                            int d_type = PerlinNoise(x, y, 2, 46, DesertX, DesertY);
                            int s_type = PerlinNoise(x, y, 2, 45, SnowfieldX, SnowfieldY);
                            if (g_type != 1)
                            {
                                _type = Type.Grassland;
                                sprite_str = "GoldenSkullStudios/Grass/ISO_Tile_Dirt_01_Grass_01";
                            }
                            else if (f_type != 1)
                            {
                                _type = Type.Forest;
                                sprite_str = "GoldenSkullStudios/Grass/ISO_Tile_Dirt_01_GrassPatch_01";
                            }
                            else if (h_type != 1)
                            {
                                _type = Type.Hills;
                                sprite_str = "GoldenSkullStudios/Stone/ISO_Tile_Riverbed_01";
                            }
                            else if (w_type != 1)
                            {
                                _type = Type.Wetlands;
                                sprite_str = "GoldenSkullStudios/Dirt/ISO_Tile_Dirt_01";
                            }
                            else if (d_type != 1)
                            {
                                _type = Type.Desert;
                                sprite_str = "GoldenSkullStudios/Sand/ISO_Tile_Sand_01";
                            }
                            else if(s_type != 1)
                            {
                                _type = Type.Snowfield;
                                sprite_str = "GoldenSkullStudios/Snow/ISO_Tile_Snow_01";
                            }
                        }
                        blocks.Add(new BlockPreData
                        {
                            Type = _type,
                            Id =id,
                            EntityId = entity_id,
                            X = 0.5f*x+0.5f+y*0.5f,
                            Y = y * 0.25f - 0.38f - (y * 0.5f) + x * 0.25f,
                            Z = dy,
                            BlockX = x,
                            BlockY = y,
                            HeightType = _heightType,
                            AssetName = asset_name,
                            SpriteStr = sprite_str
                        });
                    }
                }
                eventComponent = GameEntry.GetComponent<EventComponent>();
                entityComponent = GameEntry.GetComponent<EntityComponent>();
                eventComponent.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowTerrainEntitySuccess);
                BatchCreate(0,Config.BatchNumber);
            }
            private void BatchCreate(int begin,int end)
            {
                BatchBegin = BatchBegin + 1;
                for(int i=begin;i<end;i++)
                {
                    BlockPreData t = (BlockPreData)blocks[i];
                    t.BatchEnd = end;
                    t.BatchBegin = BatchBegin;
                    entityComponent.ShowEntity<Logic>(t.EntityId, t.AssetName, "Ground", 0, t);
                }
            }
            private void OnShowTerrainEntitySuccess(object sender, GameEventArgs e)
            {
                ShowEntitySuccessEventArgs _e = e as ShowEntitySuccessEventArgs;
                BlockPreData t = (BlockPreData)_e.UserData;
                int have = entityComponent.GetEntityGroup("Ground").GetAllEntities().Length;
                if(BatchBegin == t.BatchBegin&&have==t.BatchEnd)
                {
                    Loading.loadingUI.GetComponentInChildren<UnityEngine.UI.Text>().text =  ((float)have/(float)Config.Count).ToString()+"%";
                    if(have == Config.Count)
                    {
                        eventComponent.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowTerrainEntitySuccess);
                        ProcessWaterEdge();
                    }
                    else
                    {
                        BatchCreate(have,(have+Config.BatchNumber<Config.Count ? have+Config.BatchNumber : Config.Count) );
                    }
                }
            }
            //-------------------------------------------------------------
            private void ProcessWaterEdge()
            {
                foreach (int id in water_blocks)
                {
                    (entityComponent.GetEntity(BigMapInfo.Get(id).EntityId).Logic as Logic).FlashWaterDirection();
                }
                ProcessCity();
            }
           //-------------------------------------------------------------
            private void PrintCity(int id)
            {
                int width = Config.Width;
                int y = id/width;
                int x = id%width;
                var entity_id = EntityHelper.EntityCreate.GenerateId();
                entityComponent.ShowEntity<City.Logic>( entity_id,"Assets/Prefab/Terrain/City.prefab" , "City", 0, new BlockPreData
                {
                    Id =id,
                    EntityId = entity_id,
                    X = 0.5f*x+0.5f+y*0.5f,
                    Y = y * 0.25f - 0.38f - (y * 0.5f) + x * 0.25f,
                    Z = 0.7f,
                    BlockX = x,
                    BlockY = y
                });
            }
            private void CalculateBox(int id,int spare)
            {
                int width = Config.Width;
                int y = id/width;
                int x = id%width;
                for(int i=x - spare;i<x + spare;i++)
                {
                    for(int j=y - spare;j<y + spare;j++)
                    {
                        BigMapInfo.UseStackable(j*width+i);
                    }
                }
            }
            private void AddCity()
            {
                int index = Random.Range(0,BigMapInfo.StackableBlockSize());
                int id = BigMapInfo.GetStackableBlock(index);
                BigMapInfo.CityIds.Add(id);
                CalculateBox(id,City.Config.Sparse);
                PrintCity(id);
            }
            private void ProcessCity()
            {
                eventComponent.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowCityEntitySuccess);
                for(int i = 0;i<City.Config.Number;i++)
                {
                    AddCity();
                }   
            }
            private void OnShowCityEntitySuccess(object sender, GameEventArgs e)
            {
                int have = entityComponent.GetEntityGroup("City").GetAllEntities().Length;
                Loading.loadingUI.GetComponentInChildren<UnityEngine.UI.Text>().text =  ((float)have/(float)City.Config.Number).ToString()+"%";
                if(have == City.Config.Number)
                {
                    eventComponent.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowCityEntitySuccess);
                    ProcessSpawn();
                }
            }
           //-------------------------------------------------------------
            private void PrintPlayer(int id,bool is_huimen)
            {
                var entity_id = EntityHelper.EntityCreate.GenerateId();
                if(is_huimen)
                {
                    entityComponent.ShowEntity<PlayerLogic>(entity_id,"Assets/Prefab/Characters/FootSoldier.prefab","PlayerGroup",new PlayerLogic.InitData{
                        id = id,
                        CanWalkSteps = 6,
                        EntityId = entity_id
                    });
                }
                else
                {
                    entityComponent.ShowEntity<AIPlayerLogic>(entity_id,"Assets/Prefab/Characters/FootSoldier.prefab","PlayerGroup",new AIPlayerLogic.InitData{
                        id = id,
                        CanWalkSteps = 6,
                        EntityId = entity_id
                    });
                }
            }
            private void OnShowPlayerEntitySuccess(object sender, GameEventArgs e)
            {
                if(entityComponent.GetEntityGroup("PlayerGroup").GetAllEntities().Length==BigMapInfo.SpawnIds.Count)
                {
                    eventComponent.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowPlayerEntitySuccess);
                    ProcessRoad();
                }
            }
            private void AddSpawn(bool is_huimen)
            {
                int index = Random.Range(0,BigMapInfo.StackableBlockSize());
                int id = BigMapInfo.GetStackableBlock(index);
                BigMapInfo.SpawnIds.Add(id);
                CalculateBox(id,Config.SpawnSparse);
                PrintPlayer(id,is_huimen);
            }
            private void ProcessSpawn()
            {
                eventComponent.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowPlayerEntitySuccess);
                for(int i = 0;i<Config.SpawnNumber;i++)
                {
                    AddSpawn(i==0);
                }
            }
            //-------------------------------------------------------------
            private void ProcessRoad()
            {
                //TODO 初始化路
                eventComponent.Fire(this,new CreateSuccessEventArgs(null));
            }
            
        }
    }
}