using GameFramework.Procedure;
using GameFramework.Event;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using UnityGameFramework.Runtime;

namespace Commando
{
    public class ProcedureDemo : Commando.ProcedureBase
    {
        EntityComponent entityComponent;
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            
            entityComponent = GameEntry.GetComponent<EntityComponent>();
            entityComponent.ShowEntity<PlayerLogic>(1,"Assets/Prefab/Kimchi.prefab","PlayerGroup");


            UIComponent uIComponent = GameEntry.GetComponent<UIComponent>();
            uIComponent.OpenUIForm("Assets/Prefab/UIDemo.prefab", "UI");

            GameEntry.GetComponent<SoundComponent>().PlaySound("Assets/Sound/5.wav","BackGround");
        }
        
    }

}
