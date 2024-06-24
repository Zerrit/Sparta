using UnityEngine;

namespace Zparta.Levels.Stages
{
    public class StartBridge : MonoBehaviour
    {
        [SerializeField] private GameObject barrier;

        public void ShowBarrier() => barrier.SetActive(true);
        public void HideBarrier() => barrier.SetActive(false);
    }
}