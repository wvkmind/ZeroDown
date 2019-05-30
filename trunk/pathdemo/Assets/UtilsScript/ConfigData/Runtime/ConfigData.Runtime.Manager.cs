using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using GameFramework.Resource;
using System;
using System.Diagnostics;
using UnityGameFramework.Runtime;

namespace ConfigData.Runtime    
{
    public class Manager
    {
        private struct PreAsset
        {
            public string str;
            public int pri;
            public LoadAssetCallbacks cal;
            public UnityGameFramework.Runtime.LoadConfigInfo inf;
        }
        public Dictionary<string, object>  data = new Dictionary<string, object>();
        public Dictionary<string, string> book = new Dictionary<string, string>();
        private IResourceManager mResourceManager;
        private EventComponent eventComponent;
        private LoadAssetCallbacks mLoadAssetCallbacks;
        public int configLoadedCount = 0;
        public bool isBatch = false;
        public object userData = null;
        public ArrayList preList = new ArrayList();
        public void Init()
        {
            mResourceManager = GameFrameworkEntry.GetModule<IResourceManager>();
            eventComponent  = GameEntry.GetComponent<EventComponent>();
            mLoadAssetCallbacks = new LoadAssetCallbacks(LoadDataTableSuccessCallback, LoadDataTableFailureCallback);
        }
        public void LoadAsset<T>(string sheet_name,object user_data) where T : Data, new()
        {
            userData = user_data;
            LoadAsset<T>(sheet_name);
        }        
        public void BatchBegin(object batch_user_data)
        {
            if(BatchStatus())
            {
                isBatch = true;
                userData = batch_user_data;
            }
            else
                throw new Exception("配置加载器还在工作");
        }
        public void BatchEnd()
        {
            foreach(var _pre_asset in preList)
            {
                AddProcess((PreAsset)_pre_asset);
            }
            isBatch = false;
        }
        public bool BatchStatus()
        {
            return configLoadedCount == 0;
        }
        private void AddProcess(PreAsset pre_asset)
        {
            mResourceManager.LoadAsset(
                pre_asset.str, 
                pre_asset.pri, 
                pre_asset.cal, 
                pre_asset.inf
            );
        }
        public void  LoadAsset<T>(string sheet_name) where T : Data, new()
        {
            if (mResourceManager == null) Init();
            T _var = new T();
            string assstr = "Assets/UtilsScript/ConfigData/Asset/" + _var.Type() + "_" + sheet_name + ".txt";
            book.Remove(assstr);
            book.Add(assstr,sheet_name);
            configLoadedCount++;
            PreAsset _pre_asset;
            _pre_asset.str = assstr;
            _pre_asset.pri = 0;
            _pre_asset.cal = mLoadAssetCallbacks;
            _pre_asset.inf = new UnityGameFramework.Runtime.LoadConfigInfo("Text", _var.GetType());
            if(isBatch)
            {
                preList.Add(_pre_asset);
            }
            else
            {
                AddProcess(_pre_asset);
            }
        }
        public  void LoadDataTableSuccessCallback(string data_table_asset_name, object data_table_asset, float duration, object user_data)
        {
            UnityGameFramework.Runtime.LoadConfigInfo _userData = user_data as UnityGameFramework.Runtime.LoadConfigInfo;
            Type _type = _userData.UserData as Type;
            ConfigData.Runtime.Data _obj = Activator.CreateInstance(_type) as ConfigData.Runtime.Data;

            book.TryGetValue(data_table_asset_name, out string sheet_name);

            _obj.Parse(sheet_name, data_table_asset);
            data.Remove(sheet_name);
            data.Add(sheet_name, _obj);
            configLoadedCount--;
            if(configLoadedCount == 0)
            {
                eventComponent.Fire(this, new ConfigData.Runtime.LoadSuccessEventArgs(userData));
            }
        }
        public void LoadDataTableFailureCallback(string data_table_asset_name, LoadResourceStatus status, string error_message, object user_data)
        {
            configLoadedCount = 0;
            UnityEngine.Debug.LogError("Load Assets" + data_table_asset_name + " " + status + " " + error_message);
            throw new Exception("配置加载器都蹦了，别干活了");
        } 
    }

}