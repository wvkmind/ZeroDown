namespace Commando
{
    namespace EntityHelper
    {
        public  static class EntityCreate
        {
            private static int BeforeEntityId = int.MinValue;
            public static int GenerateId()
            {
                return BeforeEntityId++ ;
            }
        }
    }
}
