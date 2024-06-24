using UnityEngine;

namespace Zparta.Levels.BonusLevel
{
    public class BonusPlayer : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int Preparing = Animator.StringToHash("Preparing");
        private static readonly int Punch1 = Animator.StringToHash("Punch");


        public void StartPreparing()
        {
            _animator.SetTrigger(Preparing);
        }

        public void Punch()
        {
            _animator.SetTrigger(Punch1);
        }
    }
}