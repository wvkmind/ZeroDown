using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ConfigData.Editor
{
    public  class Model
    {
        public string FileUrl = "空";
        public string OutFileUrl = "空";
        public int SelectNumber = typeof(NormalTable).GetHashCode();
        private static readonly Dictionary<int, Type> ModelNames = new Dictionary<int, Type>();
        public static string[] names;
        public static int[] options;
        private Base CurModel;

        public void FreshModel()
        {
            var types = Assembly.GetAssembly(typeof(Base)).GetExportedTypes();
            foreach (var v in types)
            {
                if (v.Namespace == "ConfigData.Editor" && v.Name != "ConfigData.Editor" && v.Name != "ConfigData.Editor.DataType")
                {
                    RegisterConfigModel(v);
                }
            }
            names = GetTypes();
            options = GetValues();
            Select();
        }
        public void Select()
        {
            ModelNames.TryGetValue(SelectNumber, out Type type);
            if (type != null)
            {
                CurModel = (Base)Assembly.GetAssembly(typeof(Base)).CreateInstance(type.FullName);
            }
            else
            {
                UnityEngine.Debug.LogError("无法找到对应类");
            }
        }
        public void Genrate()
        {
            CurModel.GreateData(FileUrl, OutFileUrl);
        }
        private void RegisterConfigModel(Type type)
        {
            if (ModelNames.ContainsKey(type.GetHashCode()))
            {

            }
            else
            {
                ModelNames.Add(type.GetHashCode(), type);
            }
        }
        private string[] GetTypes()
        {
            ArrayList arrayList = new ArrayList();
            foreach (var type in ModelNames.Values)
            {
                arrayList.Add(type.ToString());
            }
            string[] ret = (string[])arrayList.ToArray(typeof(string));
            return ret;
        }
        private int[] GetValues()
        {
            ArrayList arrayList = new ArrayList();
            foreach (var type in ModelNames.Keys)
            {
                arrayList.Add(type);
            }
            int[] ret = (int[])arrayList.ToArray(typeof(int));
            return ret;
        }
    }
}