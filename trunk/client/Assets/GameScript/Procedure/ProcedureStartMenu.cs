using GameFramework.Procedure;
using GameFramework.Event;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using UnityGameFramework.Runtime;

namespace Commando
{
    public class ProcedureStartMenu : Commando.ProcedureBase
    {   
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            UIComponent uIComponent = GameEntry.GetComponent<UIComponent>();
            uIComponent.OpenUIForm("Assets/Prefab/UI/StartMenu.prefab", "NormalUI",this);
            GameEntry.GetComponent<EventComponent>().Subscribe(OpenUIFormSuccessEventArgs.EventId,OnOpenUIFormSuccess);
        }
        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            GameEntry.GetComponent<EventComponent>().Unsubscribe(OpenUIFormSuccessEventArgs.EventId,OnOpenUIFormSuccess);
            Commando.Loading.loadingUI.gameObject.SetActive(false);
        }
    }
}