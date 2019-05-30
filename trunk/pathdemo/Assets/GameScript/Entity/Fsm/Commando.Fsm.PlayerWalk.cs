using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace Commando
{
    namespace Fsm
    {

        public class PlayerWalk : FsmState<Entity.Logic.Character>
        {
            protected override void OnInit(IFsm<Entity.Logic.Character> fsm)
            {
                base.OnInit(fsm);

            }

            protected override void OnEnter(IFsm<Entity.Logic.Character> fsm)
            {
                base.OnEnter(fsm);
                fsm.Owner.SetPos( fsm.Owner.Data.x, fsm.Owner.Data.y);
                ChangeState<PlayerStay>(fsm);
            }

            protected override void OnUpdate(IFsm<Entity.Logic.Character> fsm, float elapseSeconds, float realElapseSeconds)
            {
                base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
                
            }

            protected override void OnLeave(IFsm<Entity.Logic.Character> fsm, bool isShutdown)
            {
                base.OnLeave(fsm, isShutdown);
            }

            protected override void OnDestroy(IFsm<Entity.Logic.Character> fsm)
            {
                base.OnDestroy(fsm);
            }
        }
    }
}
