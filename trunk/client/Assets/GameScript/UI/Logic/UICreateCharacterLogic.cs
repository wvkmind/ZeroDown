using UnityGameFramework.Runtime;
using GameFramework.Event;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

using UnityEngine;
namespace Commando
{ 
    public class UICreateCharacterLogic: UIFormLogic
    {
        public UnityEngine.UI.Button Exit;
        public UnityEngine.UI.Button Next;
         
        protected override void OnInit(object userData)
        {
            ProcedureStartMenu procedureStartMenu = userData as ProcedureStartMenu;
            base.OnInit(userData);
            Exit.onClick.AddListener(()=>{
                GameEntry.GetComponent<UIComponent>().CloseUIForm(this.UIForm);
            });
            Next.onClick.AddListener(()=>{
                Commando.Loading.loadingUI.gameObject.SetActive(true);
                GameEntry.GetComponent<UIComponent>().CloseAllLoadedUIForms();
                procedureStartMenu.SetNextScene("Assets/Scenes/BigMap.unity",typeof(ProcedureBigMap));
            });
        }
        
    }
   
}