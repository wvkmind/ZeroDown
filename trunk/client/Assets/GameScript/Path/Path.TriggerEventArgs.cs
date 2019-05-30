using GameFramework.Event;
namespace Commando 
{
    namespace Path 
    {
        public class TriggerEventArgs: GameEventArgs
        {
            public static readonly int EventId = typeof(Commando.Path.TriggerEventArgs).GetHashCode();
            public TriggerEventArgs(object user_data)
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