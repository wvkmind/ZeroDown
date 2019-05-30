using GameFramework.Fsm;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace Commando
{
	namespace Fsm
    {
		public class Idle : FsmState<PlayerLogic> {
			protected override void OnInit (IFsm<PlayerLogic> fsm) { 
				base.OnInit(fsm);
			}
			
			protected override void OnEnter (IFsm<PlayerLogic> fsm) {
				base.OnEnter(fsm);
				fsm.Owner.GetComponentInChildren<FootSoldier>().Idle();
			}
			
			protected override void OnUpdate (IFsm<PlayerLogic> fsm, float elapseSeconds, float realElapseSeconds) {
				base.OnUpdate(fsm,elapseSeconds,realElapseSeconds);
				float inputVertical = Input.GetAxis ("Vertical");
				if (inputVertical != 0) {
					ChangeState<Path>(fsm);
				}
			}
			
			protected override void OnLeave (IFsm<PlayerLogic> fsm, bool isShutdown) {
				base.OnLeave(fsm,isShutdown);
				Log.Info("状态离开");
			}
			
			protected override void OnDestroy (IFsm<PlayerLogic> fsm) {
				base.OnDestroy (fsm);
			}
		}
	}
}
