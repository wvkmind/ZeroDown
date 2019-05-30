using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace Commando
{
	namespace Fsm
    {
		public class Walk : FsmState<PlayerLogic> {
			
			protected override void OnInit (IFsm<PlayerLogic> fsm) { 
				base.OnInit(fsm);
			}
			
			protected override void OnEnter (IFsm<PlayerLogic> fsm) {
				base.OnEnter(fsm);
				fsm.Owner.work_index = 0;
			}
			
			protected override void OnUpdate (IFsm<PlayerLogic> fsm, float elapseSeconds, float realElapseSeconds) {
				
				if(fsm.Owner.work_index==-1)
				{
					ChangeState<Idle>(fsm);
				}
			}
			
			protected override void OnLeave (IFsm<PlayerLogic> fsm, bool isShutdown) {
			}
			
			protected override void OnDestroy (IFsm<PlayerLogic> fsm) {
				base.OnDestroy (fsm);
			}
		}
	}
}
