using UnityGameFramework.Runtime;
using UnityEngine;
namespace Commando
{ 
    public class UIGameInfoSettingsLogic: UIFormLogic
    {
        public UnityEngine.UI.Slider MapSlider;
        public UnityEngine.UI.Text MapSize;
        public UnityEngine.UI.Button Exit;
        public UnityEngine.UI.Button Next;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            MapSlider.onValueChanged.AddListener((v)=>{
                DataNodeComponent dataNodeComponent = GameEntry.GetComponent<DataNodeComponent>();
                dataNodeComponent.SetData<VarInt>("GameInfo.MapSize",new VarInt((int)v));
                if(v==1)
                {
                    MapSize.text = "小";
                    
                }
                else if(v==2)
                {
                    MapSize.text = "中";
                }
                else if(v==3)
                {
                    MapSize.text = "大";
                }
            });
            Exit.onClick.AddListener(()=>{
                GameEntry.GetComponent<UIComponent>().CloseUIForm(this.UIForm);
            });
            Next.onClick.AddListener(()=>{
                GameEntry.GetComponent<UIComponent>().OpenUIForm("Assets/Prefab/UI/CreateCharacter.prefab", "NormalUI",true,userData);
            });
        }
    }
}