using UnityEngine;

namespace Zparta.Levels.BonusLevel
{
    public class BonusLevelFinalExposionConstuction : MonoBehaviour
    {
        [SerializeField] BonusLevelExplosionObject[] m_explosionObjects;

        void OnEnable()
        {
            foreach(var obj in m_explosionObjects)
            {
                obj.ResetObject();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Enemy")
            {
                foreach (var obj in m_explosionObjects)
                {
                    obj.Explode();
                }

                other.GetComponentInParent<BonusEnemy>().Explode();
            }
        }
    }
}