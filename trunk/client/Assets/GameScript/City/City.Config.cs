namespace Commando
{ 
    namespace City
    {
        public static class Config
        {
            public static int Number = 6;//城市数量
            public static int Sparse = 10;//城市稀疏度
            public static void InitCity(int n ,int s)
            {
                Number = n;
                Sparse = s;
            }
        }
    }
}