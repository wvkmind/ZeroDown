using GameFramework.Event;
namespace Commando 
{
    namespace Terrain 
    {
        public class CitySuccessEventArgs: GameEventArgs
        {
            public static readonly int EventId = typeof(Commando.Terrain.CitySuccessEventArgs).GetHashCode();
            public CitySuccessEventArgs(object user_data)
            {
                UserData = user_data;
            }
            public object UserData
            {
                get;
                private set;
            }
            public override int Id
            {
                get
                {
                    return EventId;
                }
            }
            public override void Clear()
            {
                UserData = default;
            }
        }
    }
    
}