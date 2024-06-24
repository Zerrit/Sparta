#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectPoolManager))]
public class ObjectPoolManagerEditor : Editor
{
    SerializedProperty m_objectPools;
    SerializedProperty m_objectPoolEnumPath;

    void OnEnable()
    {
        m_objectPools = serializedObject.FindProperty("m_objectPools");
        m_objectPoolEnumPath = serializedObject.FindProperty("m_objectPoolEnumPath");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ObjectPoolManager myScript = (ObjectPoolManager)target;

        EditorGUILayout.PropertyField(m_objectPools);
        EditorGUILayout.PropertyField(m_objectPoolEnumPath);

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Init"))
        {
            myScript.Init();
            EditorUtility.RequestScriptReload();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif