using System.Collections;

namespace ConfigData.Runtime
{
    public abstract class Data
    {
        public readonly string Path = "Assets/UtilsScript/ConfigData/Asset/NORMAL_TABLE_";
         
        private readonly ArrayList arrayList = new ArrayList();
        public void Add(object data)
        {
            arrayList.Add(data);
        }
        public object Get(int index) 
        {
            return arrayList[index];
        }
        public abstract void Parse(string data_table_asset_name, object data_table_asset);
        public abstract DataType Type();
    }
}

