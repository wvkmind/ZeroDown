﻿using GameFramework.Event;

namespace Commando
{
    namespace Entity
    {
        namespace Helper
        {
            public  class EntityCreateSuccessEventArgs: GameEventArgs
            {
                public static readonly int EventId = typeof(Commando.Entity.Helper.EntityCreateSuccessEventArgs).GetHashCode();
                public EntityCreateSuccessEventArgs(object user_data)
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
}
