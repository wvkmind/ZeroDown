using UnityGameFramework.Runtime;

namespace Commando
{
    namespace Entity
    {
        namespace Logic
        {
            public class PathBlock : EntityLogic
            {
                public struct PathBlockStruct
                {
                    public int EntityId;
                    public Character Owner;
                    public int Weight;
                    public float RelatedX;
                    public float RelatedY;
                }

                public PathBlockStruct Data;
                private  DataNodeComponent dataNodeComponent;
                protected PathBlock()
                {

                }
                protected override void OnInit(object userData)
                {
                    base.OnInit(userData);
                    dataNodeComponent = GameEntry.GetComponent<DataNodeComponent>();
                    PathBlockStruct data = (PathBlockStruct)userData;
                    Data = data;
                    transform.localPosition = new UnityEngine.Vector3(Data.RelatedX+ Data.Owner.Data.x + 0.5f, Data.RelatedY + Data.Owner.Data.y + 0.5f, 0);
                    GetComponent<UnityEngine.SpriteRenderer>().sortingLayerName = "PathNode";
                }
                protected override void OnShow(object userData)
                {
                    base.OnShow(userData);

                }
            }
        }
    }
}
