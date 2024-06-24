using UnityEngine;

namespace Zparta.EnemiesLogic
{
    public class RandomHat : MonoBehaviour
    {
        [SerializeField] GameObject[] m_hats;

        void OnEnable()
        {
            foreach (GameObject hat in m_hats)
            {
                hat.SetActive(false);
            }

            int haveCustomHat = Random.Range(0, 3);

            if (haveCustomHat > 0)
            {
                int randomIndex = Random.Range(0, m_hats.Length);
                m_hats[randomIndex].SetActive(true);
            }
        }
    }
}