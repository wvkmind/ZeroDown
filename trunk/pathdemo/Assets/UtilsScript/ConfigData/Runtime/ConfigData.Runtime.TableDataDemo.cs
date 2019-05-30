using UnityEngine;

namespace ConfigData.Runtime
{
    public class TableDataDemoData
    {
        public int Id;
        public string Value1;
        public string Value2;
    }
    public class TableDataDemo : Data
    {
        public override DataType Type()
        {
            return DataType.NORMAL_TABLE;
        }
        public override void Parse(string data_table_asset_name, object data_table_asset)
        {
            TextAsset _text_asset = data_table_asset as TextAsset;
            int _index = 0;
            foreach (var str in _text_asset.text.Split('\n'))
            {
                if(_index!=0)
                {
                    string[] values = str.Split('\t');
                    if (values.Length > 1)
                    {
                        string _id = values[0].ToString();
                        TableDataDemoData _data = new TableDataDemoData();
                        _data.Id = int.Parse(_id);
                        _data.Value1 = values[1].ToString();
                        _data.Value2 = values[2].ToString();
                        Add(_data);
                    }
                }
                _index++;
            }
        }
    }
}