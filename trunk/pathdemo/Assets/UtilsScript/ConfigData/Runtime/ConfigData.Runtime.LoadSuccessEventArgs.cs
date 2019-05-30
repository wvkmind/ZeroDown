using GameFramework.Event;
namespace ConfigData.Runtime 
{
    public class LoadSuccessEventArgs: GameEventArgs
    {
        public static readonly int EventId = typeof(LoadSuccessEventArgs).GetHashCode();
        public LoadSuccessEventArgs(object user_data)
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