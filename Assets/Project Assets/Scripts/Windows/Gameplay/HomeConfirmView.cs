using UnityEngine;
using UnityEngine.UI;

namespace Zparta.Windows.Gameplay
{
    public class HomeConfirmView : MonoBehaviour
    {
        [SerializeField] private Image _fade;
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
    }
}