namespace Commando
{
    namespace Entity
    {
        namespace Helper
        {
            public  static class Character
            {
                private static int select=-1;
                private static int []  ownme = new int[256];
                private static int mi=0;
                private static int []  other = new int[256];
                private static int oi=0;
                public static void OwnAdd(int i)
                {
                    if(mi>256)return;
                    lock (ownme.SyncRoot)
                    {
                        ownme[mi]= i;
                        mi++;
                    }
                }
                public static void OtherAdd(int i)
                {
                    if(oi>256)return;
                    lock (other.SyncRoot)
                    {
                        other[oi]= i;
                        oi++;
                    }
                }
                public static void Select(int i)
                {
                    select = i;
                }
                public static int GetSel()
                {
                    return select;
                }
                public static int [] GetOwn()
                {
                    return ownme;
                }
                public static int [] GetOther()
                {
                    return other;
                }
                public static void Clear()
                {
                    select = -1;
                    mi = 0;
                    oi = 0;
                    System.Array.Clear(ownme,0,ownme.Length);
                    System.Array.Clear(other,0,other.Length);
                }
                public static void RemoveOwn(int i)
                {
                    lock (ownme.SyncRoot)
                    {
                        int index = -1;
                        for(int _i = 0;_i<mi;_i++)
                        {
                            if(ownme[_i]==i)
                            {
                                index = _i;
                            }
                        }
                        if(index!=-1)
                        {
                            for(int _i = index;_i<mi-1;_i++)
                            {
                                ownme[_i] = ownme[_i+1];
                            }
                            mi--;
                        }
                    }
                }
                public static void RemoveOther(int i)
                {
                    lock (other.SyncRoot)
                    {
                        int index = -1;
                        for(int _i = 0;_i<mi;_i++)
                        {
                            if(other[_i]==i)
                            {
                                index = _i;
                            }
                        }
                        if(index!=-1)
                        {
                            for(int _i = index;_i<mi-1;_i++)
                            {
                                other[_i] = other[_i+1];
                            }
                            mi--;
                        }
                    }
                }
                public static Logic.Character.CharacterStruct [] GetOtherData()
                {
                    System.Collections.ArrayList a = new System.Collections.ArrayList();
                    for(int i=0;i<oi;i++)
                    {
                        a.Add((UnityGameFramework.Runtime.GameEntry.GetComponent<UnityGameFramework.Runtime.EntityComponent>().GetEntity(other[i]).Logic as Logic.Character).Data);
                    }
                    return ( Logic.Character.CharacterStruct []) a.ToArray(typeof(Logic.Character.CharacterStruct));
                }
            }
        }
    }
}
