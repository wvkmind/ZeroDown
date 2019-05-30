namespace Commando
{ 
    namespace Terrain
    {
        public static class Config
        {
            public static int XBoundary = 50;
            public static int YBoundary = 50;
            public static int Count = 10000;
            public static int Width = 100;
            public static int Height = 100;
            public static int BatchNumber = 200;
            public static int SpawnNumber = 8;
            public static int SpawnSparse = 10;
            public static void InitSpawn(int n ,int s)
            {
                SpawnNumber = n;
                SpawnSparse = s;
            }
            public static bool InitBoundary(int x,int y)
            {
                if((4*x*y/(4*City.Config.Sparse*City.Config.Sparse)>City.Config.Number)&&(4*x*y/(4*SpawnSparse*SpawnSparse)>SpawnNumber))
                {
                    XBoundary = x;
                    YBoundary = y;
                    Width = x*2;
                    Height = y*2;
                    Count = 4*XBoundary*YBoundary;
                    return true;
                }   
                else
                {
                    return false;
                }
            }
        }
    }
}