using UnityEngine;
using UnityEditor;

namespace ConfigData.Editor
{
    public class Window : EditorWindow
    {
        private readonly Model model = new Model();

        void OnEnable()
        {
            Debug.Log("OnEnable");
            model.FreshModel();
        }
        void OnGUI()
        {
            if (model != null)
            {
                EditorGUILayout.BeginVertical(GUILayout.Width(position.width), GUILayout.Height(position.height));
                {
                    GUILayout.Space(15f);
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Space(15f);
                        EditorGUILayout.LabelField(model.FileUrl, GUILayout.Width(475f));
                        GUILayout.Space(15f);
                        if (GUILayout.Button("打开文件", GUILayout.Width(80f)))
                        {
                            BrowseAssetsDirectory();
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    GUILayout.Space(15f);
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Space(15f);
                        int before = model.SelectNumber;
                        model.SelectNumber = EditorGUILayout.IntPopup("选择模型", model.SelectNumber, Model.names, Model.options);
                        if (before != model.SelectNumber) model.Select();
                    }
                    EditorGUILayout.EndHorizontal();

                    GUILayout.Space(15f);
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Space(15f);
                        EditorGUILayout.LabelField(model.OutFileUrl, GUILayout.Width(475f));
                        GUILayout.Space(15f);
                        if (GUILayout.Button("输出到", GUILayout.Width(80f)))
                        {
                            BrowseOutputDirectory();
                        }

                    }
                    EditorGUILayout.EndHorizontal();

                    GUILayout.Space(15f);
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Space(475f);
                        if (GUILayout.Button("生成数据"))
                        {
                            model.Genrate();
                        }

                    }
                    EditorGUILayout.EndHorizontal();

                }
                EditorGUILayout.EndVertical();
            }

        }

        private void BrowseOutputDirectory()
        {
            string directory = EditorUtility.OpenFolderPanel("选择输出文件夹", System.Environment.CurrentDirectory+"/Assets/PublicScript/ConfigData/Asset", "");
            if (!string.IsNullOrEmpty(directory))
            {
                model.OutFileUrl = directory;
            }
        }

        private void BrowseAssetsDirectory()
        {
            string directory = EditorUtility.OpenFilePanel("选择文件", System.Environment.CurrentDirectory+"/Assets/PublicScript/ConfigData/Source", "xls,xlsx");
            if (!string.IsNullOrEmpty(directory))
            {
                model.FileUrl = directory;
            }
        }
    }

}
