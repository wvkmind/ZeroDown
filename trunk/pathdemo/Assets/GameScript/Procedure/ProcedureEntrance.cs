using GameFramework.Procedure;
using GameFramework.Event;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using UnityGameFramework.Runtime;

namespace Commando
{
    public class ProcedureEntrance : ProcedureBase
    {
        ResourceComponent resourceComponent;
        EventComponent eventComponent;
        BaseComponent baseComponent;
        MyConfigComponent configComponent;
        LocalizationComponent localizationComponent;
        DataNodeComponent dataNodeComponent;
        EntityComponent entityComponent;

        bool myConfigIsDone = false;
        bool localizationIsDone = false;
        bool loadScene = false;
        //[HELP]Procedure的使用
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            resourceComponent = GameEntry.GetComponent<ResourceComponent>();
            eventComponent = GameEntry.GetComponent<EventComponent>();
            baseComponent = GameEntry.GetComponent<BaseComponent>();
            configComponent = GameEntry.GetComponent<MyConfigComponent>();
            localizationComponent = GameEntry.GetComponent<LocalizationComponent>();
            dataNodeComponent = GameEntry.GetComponent<DataNodeComponent>();

            //[HELP]AssetBundle的使用
            Log.Debug(this.GetType() + ":OnUpdate");
            if (baseComponent.EditorResourceMode)
            {
                LoadAssetSuccessCallBack();
            }
            else
            {
                resourceComponent.InitResources(new GameFramework.Resource.InitResourcesCompleteCallback(LoadAssetSuccessCallBack));
            }
        }
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            if(myConfigIsDone&&localizationIsDone&&!loadScene)
            {
                //[HELP]场景的使用
                SceneComponent scene = GameEntry.GetComponent<SceneComponent>();
                //如果前面不是初始场景要先卸载其他场景
                string[] loadedSceneAssetNames = scene.GetLoadedSceneAssetNames();
                for (int i = 0; i < loadedSceneAssetNames.Length; i++)
                {
                    scene.UnloadScene(loadedSceneAssetNames[i]);
                }
                scene.LoadScene("Assets/Scenes/BigmapScenes.unity", this);
                eventComponent.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
                dataNodeComponent.SetData<VarObject>("procedureOwner",new VarObject(procedureOwner));
                loadScene = true;
            }
        }
        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {
            LoadSceneSuccessEventArgs _e = e as LoadSceneSuccessEventArgs;
            if(_e.UserData!=this)
            {
                return;
            }
            //流程看起来和场景没有关系
            ChangeState<ProcedureDemo>(dataNodeComponent.GetData<VarObject>("procedureOwner").Value as ProcedureOwner);
        }
        private void LoadAssetSuccessCallBack() 
        {
            Log.Info("Assets loaded");
            //[HELP]MyConfig使用
            configComponent.GetManager().BatchBegin(this);
            configComponent.GetManager().LoadAsset<ConfigData.Runtime.TableDataDemo>("TableData");
            configComponent.GetManager().BatchEnd();
            eventComponent.Subscribe(ConfigData.Runtime.LoadSuccessEventArgs.EventId, OnLoadConfigSuccess);
            //[HELP]多语言使用
            GameEntry.GetComponent<LocalizationComponent>().Language = GameFramework.Localization.Language.ChineseSimplified;
            localizationComponent.LoadDictionary("Default", "Assets/GameScript/Localization/Default.xml", GameFramework.LoadType.Text, 2, this);
            eventComponent.Subscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
           
        }
        private void OnLoadConfigSuccess(object sender, GameEventArgs e)
        {
            ConfigData.Runtime.LoadSuccessEventArgs _e = e as ConfigData.Runtime.LoadSuccessEventArgs;
            if(_e.UserData!=this)
            {
                return;
            }
            //[HELP]MyConfig使用
            configComponent.GetManager().data.TryGetValue("TableData", out object tmp);
            ConfigData.Runtime.TableDataDemo _tmp = tmp as ConfigData.Runtime.TableDataDemo;
            ConfigData.Runtime.TableDataDemoData data = _tmp.Get(1) as ConfigData.Runtime.TableDataDemoData;
            //[HELP]DataNode的使用 Var变量的使用
            dataNodeComponent = GameEntry.GetComponent<DataNodeComponent>();
            dataNodeComponent.SetData<VarObject>("TableData.1",new VarObject(data));
            ConfigData.Runtime.TableDataDemoData my_data  = dataNodeComponent.GetData<VarObject>("TableData.1").Value as ConfigData.Runtime.TableDataDemoData;
            Log.Info(my_data.Value1);
            myConfigIsDone = true;
        }
        private void OnLoadDictionarySuccess(object sender, GameEventArgs e)
        {
            LoadDictionarySuccessEventArgs _e = e as LoadDictionarySuccessEventArgs;
            if(_e.UserData!=this)
            {
                return;
            }
            localizationIsDone = true;
            //[HELP]多语言使用
            Log.Error(localizationComponent.GetString("Game.Name"));
        }    
        private void OnLoadConfigFailure(object sender, GameEventArgs e)
        {
            Log.Error("Config load error");
        }
    }

}
