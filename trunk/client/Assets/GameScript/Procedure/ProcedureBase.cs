using GameFramework.Event;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using UnityGameFramework.Runtime;

namespace Commando
{
    public class ProcedureBase : GameFramework.Procedure.ProcedureBase
    {   
        bool go_next_scence = false;
        bool go_next_procedure = false;
        bool next = false;
        protected string scence_name;
        protected System.Type procedure;
        public void SetNextScene(string _scence_name,System.Type _procedure)
        {
            if(!next)
            {
                scence_name = _scence_name;
                procedure = _procedure;
                next = true;
            }
        }
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner,elapseSeconds,realElapseSeconds);
            if(next&&!go_next_scence)
            {
                //[HELP]场景的使用
                SceneComponent scene = UnityGameFramework.Runtime.GameEntry.GetComponent<SceneComponent>();
                //如果前面不是初始场景要先卸载其他场景
                string[] loadedSceneAssetNames = scene.GetLoadedSceneAssetNames();
                for (int i = 0; i < loadedSceneAssetNames.Length; i++)
                {
                    scene.UnloadScene(loadedSceneAssetNames[i]);
                }
                GameEntry.GetComponent<EventComponent>().Subscribe(LoadSceneSuccessEventArgs.EventId,GoToNext);
                scene.LoadScene(scence_name,this);
                go_next_scence = true;
            }
            else if (go_next_scence && go_next_procedure)
            {
                //流程看起来和场景没有关系
                ChangeState(procedureOwner,procedure);
            }
        }
        private void GoToNext(object sender, GameEventArgs e)
        {
            LoadSceneSuccessEventArgs _e = e as LoadSceneSuccessEventArgs;
            ProcedureOwner procedureOwner = _e.UserData as ProcedureOwner;
            GameEntry.GetComponent<EventComponent>().Unsubscribe(LoadSceneSuccessEventArgs.EventId,GoToNext);
            go_next_procedure = true;
        }
    }
}