using UnityEngine;
using UnityEngine.UI;

namespace Zparta.Windows.Lose.Window
{
    public class AdRenewView : MonoBehaviour
    {
        [field:SerializeField] public GameObject Timer { get; private set; }
        [field:SerializeField] public Slider Slider { get; private set; }
        [field:SerializeField] public Button AdButton { get; private set; }
        [field:SerializeField] public Button Restart { get; private set; }
    }
}