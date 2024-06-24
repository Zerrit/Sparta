using UnityEngine;

namespace Zparta.EnemiesLogic
{
    public class AppearingVfxController : MonoBehaviour
    {
        [SerializeField] GameObject m_vfx;

        void OnEnable()
        {
            int random = Random.Range(0, 2);

            if(random == 0)
            {
                m_vfx.SetActive(false);
            }
            else
            {
                m_vfx.SetActive(true);
            }
            
        }
    }
}