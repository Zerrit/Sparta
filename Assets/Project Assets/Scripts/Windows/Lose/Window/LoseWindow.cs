using UnityEngine;
using UnityEngine.UI;
using Zparta.Services;

namespace Zparta.Windows.Lose.Window
{
    public class LoseWindow : AbstractWindow
    {
        [field:SerializeField] public AdRenewView RenewView { get; private set; }
        [field:SerializeField] public Button RestartButton { get; private set; }
    }
}
