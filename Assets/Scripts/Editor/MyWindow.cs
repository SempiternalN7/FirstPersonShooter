using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;

namespace Geekbrains.Editor
{
    public class MyWindow : EditorWindow
    {
        public GameObject ObjectInstantiate;
        private string _nameObject = "Health Potions";
        private bool _groupEnabled;
        private int _countObject = 1;
        private float _minBorder = -32;
        private float _maxBorder = 32;
        [MenuItem("Geekbrains/My Health Potions")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(MyWindow));
        }
        void OnGUI()
        {
            GUILayout.Label("Базовые настройки", EditorStyles.boldLabel);
            ObjectInstantiate =
                EditorGUILayout.ObjectField("Объект который хотим вставить", ObjectInstantiate, typeof(GameObject), true)
                    as GameObject;
            _nameObject = EditorGUILayout.TextField("Имя объекта", _nameObject);
            _groupEnabled = EditorGUILayout.BeginToggleGroup("Дополнительные настройки", _groupEnabled);
            _countObject = EditorGUILayout.IntSlider("Количество объектов", _countObject, 1, 10);
            EditorGUILayout.EndToggleGroup();
            if (GUILayout.Button("Создать объекты"))
            {
                if (ObjectInstantiate)
                {
                    GameObject root = new GameObject("Potions");
                    for (int i = 0; i < _countObject; i++)
                    {
                        Vector3 pos = new Vector3(Random.Range(_minBorder, _maxBorder), 0, Random.Range(_minBorder, _maxBorder));
                        GameObject temp = Instantiate(ObjectInstantiate, pos, Quaternion.identity) as GameObject;
                        temp.name = $"{_nameObject} ({i})";
                        temp.transform.parent = root.transform;
                    }
                }
            }
        }
    }
}
