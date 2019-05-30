

namespace ConfigData.Editor
{
    public class NormalTable : Base
    {
        public override string ModelDesc()
        {
            return "正经二维表";
        }

        public override DataType ModelType()
        {
            return DataType.NORMAL_TABLE;
        }
    }
}