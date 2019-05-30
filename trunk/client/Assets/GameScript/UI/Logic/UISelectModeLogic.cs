using UnityGameFramework.Runtime;
using UnityEngine;
namespace Commando
{ 
    public class UISelectModeLogic: UIFormLogic
    {
        public UnityEngine.UI.Button Story;
        public UnityEngine.UI.Button Freedom;
        public UnityEngine.UI.Button Exit;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            Freedom.onClick.AddListener(()=>{
                GameEntry.GetComponent<UIComponent>().OpenUIForm("Assets/Prefab/UI/GameInfoSettings.prefab", "NormalUI",true,userData);
            });
            Exit.onClick.AddListener(()=>{
                GameEntry.GetComponent<UIComponent>().CloseUIForm(this.UIForm);
            });
        }
    }
}