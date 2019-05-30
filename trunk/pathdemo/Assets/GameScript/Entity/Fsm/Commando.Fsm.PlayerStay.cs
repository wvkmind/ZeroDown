using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace Commando
{
    namespace Fsm {

        public class PlayerStay : FsmState<Entity.Logic.Character> {
       

            protected override void OnInit(IFsm<Entity.Logic.Character> fsm) {
                base.OnInit(fsm);
            }

            protected override void OnEnter(IFsm<Entity.Logic.Character> fsm) {
                base.OnEnter(fsm);
                ShowPathing(fsm.Owner);
            }

            protected override void OnUpdate(IFsm<Entity.Logic.Character> fsm, float elapseSeconds, float realElapseSeconds) {
                base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
                if(Input.GetKeyUp(KeyCode.W))
                {
                    fsm.Owner.Data.y++;
                    ChangeState<PlayerWalk>(fsm);
                } 
                else if(Input.GetKeyUp(KeyCode.S))
                {
                    fsm.Owner.Data.y--;
                    ChangeState<PlayerWalk>(fsm);
                }
                else if(Input.GetKeyUp(KeyCode.A))
                {
                    fsm.Owner.Data.x--;
                    ChangeState<PlayerWalk>(fsm);
                }
                else if(Input.GetKeyUp(KeyCode.D))
                {
                    fsm.Owner.Data.x++;
                    ChangeState<PlayerWalk>(fsm);
                }
            }

            protected override void OnLeave(IFsm<Entity.Logic.Character> fsm, bool isShutdown) {
                base.OnLeave(fsm, isShutdown);
            }

            protected override void OnDestroy(IFsm<Entity.Logic.Character> fsm) {
                base.OnDestroy(fsm);
            }

            private void ShowPathing(Entity.Logic.Character owner)
            {
                owner.DrawPath();
            }

        }
    }
}
