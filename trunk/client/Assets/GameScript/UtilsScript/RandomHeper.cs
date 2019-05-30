using UnityEngine;
namespace Commando
{
    public  static class RandomHeper
    {
        public static float NewValue()
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
            return Random.value * 100f;
        }
    }
}
