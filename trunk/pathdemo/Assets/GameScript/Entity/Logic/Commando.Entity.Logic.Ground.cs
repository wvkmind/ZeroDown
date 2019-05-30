using UnityGameFramework.Runtime;

namespace Commando
{ 
    namespace Entity
    {
        namespace Logic
        {

            public class Ground : EntityLogic
            {
                public static int XBoundary = 75;
                public static int YBoundary = 50;
                public static void InitBoundary(int x,int y)
                {
                    XBoundary = x;
                    YBoundary = y;
                }
                public struct GroundStruct
                {
                    public int EntityId;
                    public Ground.HeightType HeightType;
                    public Ground.Type Type;
                    public int Id;
                    public float X;
                    public float Y;
                    public float Z;
                }

                public GroundStruct Data;
                public enum HeightType 
                { 
                    None,
                    Highland,
                    Plain,
                    Low
                }
                public enum Type 
                { 
                    None,
                    Forest,
                    Hills,
                    Grassland,
                    Wetlands,
                    Water,
                    Desert,
                    Snowfield,
                    Town,
                    Character
                }
                protected Ground()
                {

                }
                protected override void OnInit(object userData)
                {
                    base.OnInit(userData);
                    GroundStruct data = (GroundStruct)userData;
                    var x = data.Id % (XBoundary*2) - XBoundary;
                    var y = data.Id / (XBoundary*2) - YBoundary;
                    // var real_x = x + 0.5f;
                    // var real_y = y + 0.5f;
                    // Data = data;
                    // Data.X = real_x;
                    // Data.Y = real_y;
                    transform.localPosition = new UnityEngine.Vector3(data.X, data.Y+data.Z, data.Y);
                    GameEntry.GetComponent<DataNodeComponent>().SetData("Ground." + x+"."+y, new VarObject(data.Type));
                    GetComponent<UnityEngine.SpriteRenderer>().sortingLayerName = "Ground";
                    GetComponent<UnityEngine.SpriteRenderer>().sortingOrder = (int)(  data.Y*-100f);
                }
                protected override void OnShow(object userData)
                {
                    base.OnShow(userData);

                }
            }
        }
    }
}
