using System.Collections.Generic;
using System.Collections;

namespace Commando
{ 
    namespace Terrain
    {
        public static class BigMapInfo
        {   
            private static Dictionary<int,PathBlock> PathBlockDec = new Dictionary<int, PathBlock>();
            private static ArrayList StackableBlock = new ArrayList();
            private static ArrayList StackableBlockForPath = new ArrayList();
            public static  ArrayList CityIds = new ArrayList();
            public static  ArrayList SpawnIds = new ArrayList();
            public static void Add(int i,PathBlock d)
            {   
                PathBlockDec.Add(i,d);
                if(d.HeightType==HeightType.Plain)
                {
                    AddStackable(i);
                    AddStackableForPath(i);
                }
            }
            public static PathBlock Get(int i)
            {
                PathBlockDec.TryGetValue(i,out PathBlock ret);
                return ret;
            }
            
            public static int GetStackableBlock(int i)
            {
                object ret = StackableBlock[i];
                return (int)ret;
            }
            public static int StackableBlockSize()
            {
                return StackableBlock.Count;
            }
            public static void AddStackable(int id)
            {
                StackableBlock.Add(id);
            }
            public static void UseStackable(int id)
            {
                StackableBlock.Remove(id);
            }

            public static void AddStackableForPath(int id)
            {
                StackableBlockForPath.Add(id);
            }
            public static bool BlockIsStackableForPath(int x,int y)
            { 
                return StackableBlockForPath.Contains(y*Config.Width+x);
            }
        }
    }
}