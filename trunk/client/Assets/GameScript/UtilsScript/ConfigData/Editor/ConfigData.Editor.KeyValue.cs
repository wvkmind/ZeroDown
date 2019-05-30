
namespace ConfigData.Editor
{
    public class KeyValue : Base
    {
        public override string ModelDesc()
        {
            return "键值对";
        }

        public override DataType ModelType()
        {
            return DataType.KEY_VALUE;
        }
    }
}