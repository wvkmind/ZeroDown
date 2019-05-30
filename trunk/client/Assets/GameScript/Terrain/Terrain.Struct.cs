namespace Commando
{ 
    namespace Terrain
    {
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
            City,
            Character
        }
        public struct PathBlock//判断地形的时候快速数据
        {
            public int EntityId;//框架实体id;
            public Commando.Terrain.Type Type;//地形类型
            public Commando.Terrain.HeightType HeightType;//高度
        }
        
        public struct BlockPreData//一块地形的信息
        {
            public int EntityId;//框架实体id
            public Commando.Terrain.HeightType HeightType;//高度
            public Commando.Terrain.Type Type;//地形类型
            public int Id;//全部地形块的id
            public float X;//45°坐标X
            public float Y;//45°坐标Y
            public float Z;//45°坐标Z(图层相关)
            public string AssetName;
            public int BatchEnd;
            public int BatchBegin;
            public int BlockX;
            public int BlockY;
            public string SpriteStr;
        }
    }
}