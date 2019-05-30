using UnityGameFramework.Runtime;
using UnityEngine;
namespace Commando
{ 
    public class UIStartMenuLogic: UIFormLogic
    {
        public UnityEngine.UI.Button BtnStart;
        public UnityEngine.UI.Button BtnReadArchive;
        public UnityEngine.UI.Button BtnSettings;
        public UnityEngine.UI.Button BtnProducer;
        public UnityEngine.UI.Button BtnExit;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            BtnExit.onClick.AddListener(()=>{
                Commando.Loading.loadingUI.gameObject.SetActive(true);
                Commando.Loading.loadingUI.GetComponentInChildren<UnityEngine.UI.Text>().text = "Saving...";
                Application.Quit(0);
            });

            BtnStart.onClick.AddListener(()=>{
                GameEntry.GetComponent<UIComponent>().OpenUIForm("Assets/Prefab/UI/SelectMode.prefab", "NormalUI",true,userData);
            });
        }
    }
}