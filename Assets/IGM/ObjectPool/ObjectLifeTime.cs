using IGM.Core;
using UnityEngine;

public class ObjectLifeTime : MonoBehaviour
{
    [SerializeField] float m_lifeTime = 2;
    float lifeTime;

    private void OnEnable()
    {
        lifeTime = m_lifeTime;

        SingleUpdate.Instance.UpdateDelegate += OnUpdate;
    }

    private void OnDisable()
    {
        if (SingleUpdate.Instance != null)
            SingleUpdate.Instance.UpdateDelegate -= OnUpdate;
    }

    void OnUpdate()
    {
        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
        {
            gameObject.SetActive(false);
            lifeTime = m_lifeTime;
        }
    }
}