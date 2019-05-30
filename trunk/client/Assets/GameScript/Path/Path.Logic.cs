using UnityGameFramework.Runtime;
using UnityEngine;

namespace Commando
{
    namespace Path
    {
        public class Logic : EntityLogic
        {
            public struct Struct
            {
                public int EntityId;
                public int Weight;
                public float RelatedX;
                public float RelatedY;
                public PlayerLogic Owner;
                public int ID;
            }
            public Struct Data;
            private  DataNodeComponent dataNodeComponent;
            protected Logic()
            {
            }
            protected override void OnInit(object userData)
            {
                base.OnInit(userData);
                dataNodeComponent = GameEntry.GetComponent<DataNodeComponent>();
                Struct data = (Struct)userData;
                Data = data;

                int width = Commando.Terrain.Config.Width;
                int height = Commando.Terrain.Config.Height;
                int id = Data.Owner.cur_block_id;

                int y = id/width+(int)Data.RelatedY;
                int x = id%width+(int)Data.RelatedX;
                
                Data.ID = y*height+x;

                float X = 0.5f*x+0.5f+y*0.5f;
                float Y = y * 0.25f - 0.38f - (y * 0.5f) + x * 0.25f;
                float Z = 0.7f;
                transform.localPosition = new UnityEngine.Vector3(X, Y+Z, Y+100.0f);
            }
            protected override void OnShow(object userData)
            {
                base.OnShow(userData);
            }

            protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
            {
                base.OnUpdate(elapseSeconds,realElapseSeconds);
                if(Input.GetKeyUp(KeyCode.Mouse0))
                {
                    var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if(GetComponent<PolygonCollider2D>().OverlapPoint(new Vector2(pos.x,pos.y)))
                    {
                        GameEntry.GetComponent<EventComponent>().Fire(TriggerEventArgs.EventId,new TriggerEventArgs(Data));
                    }
                }
            }
        }
    }
    
}
