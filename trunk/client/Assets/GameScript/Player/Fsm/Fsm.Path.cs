using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace Commando
{
    namespace Fsm
    {
        public class Path : FsmState<PlayerLogic> {
            private bool start_walk = false;
            Commando.Path.Logic.Struct Data;
            protected override void OnInit (IFsm<PlayerLogic> fsm) { 
                base.OnInit(fsm);
            }
            
            protected override void OnEnter (IFsm<PlayerLogic> fsm) {
                base.OnEnter(fsm);
                GameEntry.GetComponent<EventComponent>().Subscribe(Commando.Path.TriggerEventArgs.EventId,OnPathClick);
                fsm.Owner.DrawPath();
            }
            private void OnPathClick(object sender, GameFramework.Event.GameEventArgs e)
            {
                Commando.Path.TriggerEventArgs _e = e as Commando.Path.TriggerEventArgs;
                Data =(Commando.Path.Logic.Struct) _e.UserData;
                start_walk = true;
            }
            private Commando.Path.Logic.Struct FindNearPath(Commando.Path.Logic.Struct _data)
            {
                Commando.Path.Logic.Struct __data = _data;
                __data.Weight = -1;
                foreach (var child in GameEntry.GetComponent<EntityComponent>().GetEntityGroup("PathBlock").GetAllEntities())
                {
                    var data = (child.Handle as GameObject).GetComponent<Commando.Path.Logic>().Data;
                    if(data.Weight == _data.Weight-1)
                    {
                        if(System.Math.Abs(_data.RelatedX-data.RelatedX)<=1&&System.Math.Abs(_data.RelatedY-data.RelatedY)<=1)
                        {
                            __data = data;
                            return __data;
                        }
                    }
                }
                return __data;
            }
            protected override void OnUpdate (IFsm<PlayerLogic> fsm, float elapseSeconds, float realElapseSeconds) {
                base.OnUpdate(fsm,elapseSeconds,realElapseSeconds);
                if(start_walk)
                {
                    Commando.Path.Logic.Struct cur = Data;
                    UnityEngine.Debug.LogError("Data"+cur.Weight);
                    fsm.Owner.work_id_list.Insert(0,cur.ID);
                    cur = FindNearPath(cur);
                    while(cur.Weight != -1)
                    {
                        fsm.Owner.work_id_list.Insert(0,cur.ID);
                        UnityEngine.Debug.LogError("cur"+cur.Weight);
                        cur = FindNearPath(cur);
                    }

                    GameEntry.GetComponent<EventComponent>().Unsubscribe(Commando.Path.TriggerEventArgs.EventId,OnPathClick);
                    fsm.Owner.DeletePath();
                    start_walk = false;
                    UnityEngine.Debug.LogError("fsm.Owner.work_id_list"+fsm.Owner.work_id_list.Count);
                    ChangeState<Walk>(fsm);
                }
            }
            
            protected override void OnLeave (IFsm<PlayerLogic> fsm, bool isShutdown) {
                base.OnLeave(fsm,isShutdown);
               
            }
            
            protected override void OnDestroy (IFsm<PlayerLogic> fsm) {
                base.OnDestroy (fsm);
            }
        }
    }
}
