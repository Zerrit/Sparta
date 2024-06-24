using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    #region Singelton
    private static ObjectPoolManager _instance;
    public static ObjectPoolManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<ObjectPoolManager>();

            return _instance;
        }
    }
    #endregion

    [SerializeField] ObjectPoolInterface[] m_objectPools;
    [SerializeField, HideInInspector] ObjectPool[] objectPools;

    [Header("Script update")]
    [SerializeField] string m_objectPoolEnumPath = "/IGM/ObjectPool/ObjectPoolEnum.cs";

    #region Instantiate
    public GameObject Instantiate(int objectIndex, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject instantiatedObject;
        objectPools[objectIndex].Instantiate(position, rotation, parent, out instantiatedObject);

        return instantiatedObject;
    }

    public GameObject Instantiate(int objectIndex, Vector3 position, Quaternion rotation)
    {
        GameObject instantiatedObject;
        objectPools[objectIndex].Instantiate(position, rotation, null, out instantiatedObject);

        return instantiatedObject;
    }

    public GameObject Instantiate(int objectIndex)
    {
        GameObject instantiatedObject;
        objectPools[objectIndex].Instantiate(Vector3.zero, Quaternion.identity, null, out instantiatedObject);

        return instantiatedObject;
    }

    public GameObject Instantiate(int objectIndex, Transform parent)
    {
        GameObject instantiatedObject;
        objectPools[objectIndex].Instantiate(Vector3.zero, Quaternion.identity, parent, out instantiatedObject);

        return instantiatedObject;
    }
    #endregion

    [ContextMenu("Init")]
    public void Init()
    {
        if (CheckObjectPoolsSameNames())
        {
            CreateObjectPools();
            InitChildrenPools();
            OnChange();
        }
    }

    void ClearObjectPools()
    {
        ObjectPool[] _objectPools = GetComponentsInChildren<ObjectPool>();

        foreach (ObjectPool OP in _objectPools)
        {
            DestroyImmediate(OP.gameObject);
        }
    }

    void CreateObjectPools()
    {
        ClearObjectPools();

        foreach(ObjectPoolInterface OPI in m_objectPools)
        {
            GameObject newGo = new GameObject(OPI.Name);
            newGo.transform.parent = transform;
            newGo.transform.localPosition = Vector3.zero;

            ObjectPool newObjectPool = newGo.AddComponent<ObjectPool>();
            newObjectPool.SetPrefab(OPI.Prefab);
            newObjectPool.SetCount(OPI.Count);
        }
    }

    bool CheckObjectPoolsSameNames()
    {
        var dict = new Dictionary<string, int>();
        bool allGood = true;

        for (int i = 0; i < m_objectPools.Length; i++)
        {
            dict.TryGetValue(m_objectPools[i].Name, out int count);
            dict[m_objectPools[i].Name] = count + 1;
        }

        foreach (var pair in dict)
        {
            if (pair.Value > 1)
            {
                Debug.LogError("Name <<" + pair.Key + ">> occurred " + pair.Value + " times.");
                allGood = false;
            }
        }

        return allGood;
    }

    void InitChildrenPools()
    {
        objectPools = GetComponentsInChildren<ObjectPool>();

        foreach (ObjectPool OP in objectPools)
        {
            OP.CreatePool();
        }
    }

    void OnChange()
    {
        if (File.Exists(Application.dataPath + m_objectPoolEnumPath))
        {
            StreamWriter sw = new StreamWriter(Application.dataPath + m_objectPoolEnumPath);
            sw.WriteLine("public enum ObjectPoolEnum");
            sw.WriteLine("{");

            foreach (ObjectPool OP in objectPools)
            {
                string _name = OP.name.Replace(' ', '_').Replace('(', '_').Replace(')', '_');
                sw.WriteLine(_name + ",");
            }

            sw.WriteLine("}");
            sw.Close();
        }
        else
            print("No File " + Application.dataPath + m_objectPoolEnumPath);
    }

    [Serializable]
    class ObjectPoolInterface
    {
        [SerializeField] string m_name;
        [SerializeField] GameObject m_prefab;
        [SerializeField] int m_count;

        public string Name => m_name;
        public GameObject Prefab => m_prefab;
        public int Count => m_count;
    }
}