using UnityGameFramework.Runtime;
using UnityEngine;
namespace Commando
{ 
    public class UIDemoLogic: UIFormLogic
    {
        public UnityEngine.UI.Text text;
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            text.text = GameEntry.GetComponent<LocalizationComponent>().GetString(text.text);
        }
    }
}