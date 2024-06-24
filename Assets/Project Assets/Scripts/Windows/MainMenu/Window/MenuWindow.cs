using UnityEngine;
using UnityEngine.UI;
using Zparta.Services;

namespace Zparta.Windows.MainMenu.Window
{
    public class MenuWindow : AbstractWindow
    {
        [field:SerializeField] public Button SettingsButton { get; private set; }
        [field:SerializeField] public Button BackgroundButton { get; private set; }
        
        [field:SerializeField] public UpgradeButton SizeUpgrade { get; private set; }
        [field:SerializeField] public UpgradeButton PowerUpgrade { get; private set; }
        
        [field:SerializeField] public Button LevelsButton { get; private set; }
        [field:SerializeField] public Button CustomizationButton { get; private set; }
        [field:SerializeField] public Button CelebrationButton { get; private set; }
        [field:SerializeField] public Button QuestsButton { get; private set; }
    }
}