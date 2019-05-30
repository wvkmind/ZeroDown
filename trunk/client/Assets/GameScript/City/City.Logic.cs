using UnityGameFramework.Runtime;

namespace Commando
{ 
    namespace City
    {
        public class Logic : EntityLogic
        {
            protected override void OnInit(object userData)
            {
                base.OnInit(userData);
                Terrain.BlockPreData data = (Terrain.BlockPreData)userData;
                transform.localPosition = new UnityEngine.Vector3(data.X, data.Y+data.Z, data.Y+100.0f);
            }
        }
    }
}