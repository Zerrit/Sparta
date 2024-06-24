using UnityEngine;

namespace Zparta.Levels.Traps
{
    public class BaseTrap : MonoBehaviour
    {
        [SerializeField] private TrapTrigger[] triggers;
        [SerializeField] private Animation trapAnimation;
        
        private void Start()
        {
            foreach (TrapTrigger trigger in triggers)
            {
                trigger.OnTriggered += ActivateTrap;
            }
        }

        
        protected virtual void ActivateTrap()
        {
            trapAnimation.Play();
        }

        
        private void OnDisable()
        {
            foreach (TrapTrigger trigger in triggers)
            {
                trigger.OnTriggered -= ActivateTrap;
            }
        }
    }
}