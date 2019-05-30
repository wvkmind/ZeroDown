using UnityEngine;
using GameFramework.Procedure;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace Commando
{
    public class ProcedureDemo : ProcedureBase
    {
        private EventComponent eventComponent;
        private EntityComponent entityComponent;
        private DataNodeComponent dataNodeComponent;
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
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            eventComponent = GameEntry.GetComponent<EventComponent>();
            entityComponent = GameEntry.GetComponent<EntityComponent>();
            dataNodeComponent = GameEntry.GetComponent<DataNodeComponent>();
            
            eventComponent.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            int width = Entity.Logic.Ground.XBoundary*2;
            int height = Entity.Logic.Ground.YBoundary*2;
            Random.InitState((int)System.DateTime.Now.Ticks);

            float seedX = Random.value * 100f;
            float seedY = Random.value * 100f;
            Random.InitState((int)System.DateTime.Now.Ticks);
            float HillsX = Random.value * 100f;
            float HillsY = Random.value * 100f;
            Random.InitState((int)System.DateTime.Now.Ticks);
            float GrasslandX = Random.value * 100f;
            float GrasslandY = Random.value * 100f;
            Random.InitState((int)System.DateTime.Now.Ticks);
            float ForestX = Random.value * 100f;
            float ForestY = Random.value * 100f;
            Random.InitState((int)System.DateTime.Now.Ticks);
            float WetlandsX = Random.value * 100f;
            float WetlandsY = Random.value * 100f;
            Random.InitState((int)System.DateTime.Now.Ticks);
            float DesertX = Random.value * 100f;
            float DesertY = Random.value * 100f;
            Random.InitState((int)System.DateTime.Now.Ticks);
            float SnowfieldX = Random.value * 100f;
            float SnowfieldY = Random.value * 100f;



            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    
                    int height_type = PerlinNoise(x,y,2,15.0f,seedX,seedY);
                    float dy = 0.0f;
                    Entity.Logic.Ground.HeightType _heightType = Entity.Logic.Ground.HeightType.None;
                    Entity.Logic.Ground.Type _type = Entity.Logic.Ground.Type.None;
                    string asset_name = "";
                    if (height_type == 0)
                    {
                        int montain_height = PerlinNoise(x,y,5,15.0f,seedX,seedY);
                        if(montain_height ==1)montain_height=0;
                        else montain_height =1;
                        dy = montain_height*0.4f+0.4f;
                        _heightType = Entity.Logic.Ground.HeightType.Highland;
                        _type = Entity.Logic.Ground.Type.Hills;
                        asset_name = "Assets/Prefab/Hills.prefab";
                    }
                    else if (height_type == 1)
                    {
                        dy = 0f;
                        _heightType = Entity.Logic.Ground.HeightType.Plain;
                        _type = Entity.Logic.Ground.Type.Grassland;
                        asset_name = "Assets/Prefab/Grassland.prefab";
                    }
                    else
                    {
                        dy = -0.4f;
                        _heightType = Entity.Logic.Ground.HeightType.Low;
                        _type = Entity.Logic.Ground.Type.Water;
                        asset_name = "Assets/Prefab/Water.prefab";
                    }


                    if(height_type==0|| height_type==1)
                    {
                        int g_type = PerlinNoise(x, y, 2, 50, GrasslandX, GrasslandY);
                        int h_type = PerlinNoise(x, y, 2, 49, HillsX, HillsY);
                        int f_type = PerlinNoise(x, y, 2, 48, ForestX, ForestY);
                        int w_type = PerlinNoise(x, y, 2, 47, WetlandsX, WetlandsY);
                        int d_type = PerlinNoise(x, y, 2, 46, DesertX, DesertY);
                        int s_type = PerlinNoise(x, y, 2, 45, SnowfieldX, SnowfieldY);

                        Debug.Log(g_type);

                        if (g_type != 1)
                        {
                            _type = Entity.Logic.Ground.Type.Grassland;
                            asset_name = "Assets/Prefab/Grassland.prefab";
                        }
                        else if (f_type != 1)
                        {
                            _type = Entity.Logic.Ground.Type.Forest;
                            asset_name = "Assets/Prefab/Forest.prefab";
                        }
                        else if (h_type != 1)
                        {
                            _type = Entity.Logic.Ground.Type.Hills;
                            asset_name = "Assets/Prefab/Hills.prefab";
                        }
                        else if (w_type != 1)
                        {
                            _type = Entity.Logic.Ground.Type.Wetlands;
                            asset_name = "Assets/Prefab/Wetlands.prefab";
                        }
                        else if (d_type != 1)
                        {
                            _type = Entity.Logic.Ground.Type.Desert;
                            asset_name = "Assets/Prefab/Desert.prefab";
                        }
                        else if(s_type != 1)
                        {
                            _type = Entity.Logic.Ground.Type.Snowfield;
                            asset_name = "Assets/Prefab/Snowfield.prefab";
                        }
                    }


                    var entity_id = Entity.Helper.EntityCreate.GenerateId();
                    entityComponent.ShowEntity<Entity.Logic.Ground>(entity_id, asset_name, "Ground", 0, new Entity.Logic.Ground.GroundStruct
                    {
                        Type = _type,
                        Id =x*height+y,
                        EntityId = entity_id,
                        X = 0.5f*x+0.5f+y*0.5f,
                        Y = y * 0.25f - 0.38f - (y * 0.5f) + x * 0.25f,
                        Z = dy,
                        HeightType = _heightType
                    });

                }
            }
        }
        private void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            if(entityComponent.GetEntityGroup("Ground").GetAllEntities().Length == 10000)
            {
                var eid = Entity.Helper.EntityCreate.GenerateId();
                Entity.Helper.Character.OwnAdd(eid);
                Entity.Helper.Character.Select(eid);
                entityComponent.ShowEntity<Entity.Logic.Character>(eid, "Assets/Prefab/Smile.prefab", "Player", 0, new Entity.Logic.Character.CharacterStruct
                {
                    EntityId = eid,
                    GroundId = 0,
                    CanWalkSteps = 3,
                    x = 0,
                    y = 0
                });
                eventComponent.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            }
        }
    }

}
