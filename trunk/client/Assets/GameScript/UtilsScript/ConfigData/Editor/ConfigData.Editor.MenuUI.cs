using UnityEngine;
using UnityEditor;

namespace ConfigData.Editor
{
    public static class MenuUI
    {
        [MenuItem("Game Framework/ConfigAndData")]
        public static void ConfigAndData()
        {
            Window window = EditorWindow.GetWindow<Window>(true);
            window.minSize = window.maxSize = new Vector2(600f, 500f);
        }
    }
}

