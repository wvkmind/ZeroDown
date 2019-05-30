using UnityGameFramework.Runtime;

namespace Commando
{ 
    namespace Terrain
    {
        public class Logic : EntityLogic
        {
            private int BlockX;
            private int BlockY;
            protected override void OnInit(object userData)
            {
                base.OnInit(userData);
                BlockPreData data = (BlockPreData)userData;
                BlockX = data.BlockX;
                BlockY = data.BlockY;
                transform.localPosition = new UnityEngine.Vector3(data.X, data.Y+data.Z, data.Y+100.0f);
                if(data.Z>0.7)GetComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = 1;
                BigMapInfo.Add(data.Id,new PathBlock{
                    EntityId = data.EntityId,
                    Type = data.Type,
                    HeightType = data.HeightType
                });
                ChangeSprite(data.SpriteStr);
            }
            public void FlashWaterDirection()
            {
                int width = Config.XBoundary*2;
                int left_id = -1;
                int right_id = -1;
                int down_id = -1;
                int up_id = -1;
                int [] flags  = new int[8];
                if(BlockX!=0)left_id = BlockY*width+(BlockX - 1);
                if(BlockX!=Config.Width)right_id = BlockY*width+(BlockX + 1);
                if(BlockY!=0)down_id = (BlockY - 1)*width+BlockX;
                if(BlockY!=Config.Height)up_id = (BlockY + 1)*width+BlockX;
                
                if(left_id!=-1&&BigMapInfo.Get(left_id).Type != Type.Water)flags[0] = 1;
                if(right_id!=-1&&BigMapInfo.Get(right_id).Type != Type.Water)flags[1] = 1;
                if(down_id!=-1&&BigMapInfo.Get(down_id).Type != Type.Water)flags[2] = 1;
                if(up_id!=-1&&BigMapInfo.Get(up_id).Type != Type.Water)flags[3] = 1;
                
                if(left_id!=-1&&up_id!=-1&&BigMapInfo.Get(up_id-1).Type != Type.Water)flags[4] = 1;
                if(right_id!=-1&&up_id!=-1&&BigMapInfo.Get(up_id+1).Type != Type.Water)flags[5] = 1;
                if(left_id!=-1&&down_id!=-1&&BigMapInfo.Get(down_id-1).Type != Type.Water)flags[6] = 1;
                if(right_id!=-1&&down_id!=-1&&BigMapInfo.Get(down_id+1).Type != Type.Water)flags[7] = 1;


                if(flags[0]==1)
                {
                    flags[4] = 0;
                    flags[6] = 0;
                }
                if(flags[3]==1)
                {
                    flags[4] = 0;
                    flags[5] = 0;
                }
                if(flags[1]==1)
                {
                    flags[5] = 0;
                    flags[7] = 0;
                }
                if(flags[2]==1)
                {
                    flags[6] = 0;
                    flags[7] = 0;
                }
                
                ChangeSprite("GoldenSkullStudios/Water/Water"+flags[7]+flags[1]+flags[5]+flags[2]+flags[3]+flags[6]+flags[0]+flags[4]);
            }
            public void ChangeSprite(string assert_path)
            {
                UnityEngine.Sprite sprite  = UnityEngine.Resources.Load(assert_path, typeof(UnityEngine.Sprite)) as UnityEngine.Sprite;
                GetComponentInChildren<UnityEngine.SpriteRenderer>().sprite = sprite;
            }
        }
    }
}