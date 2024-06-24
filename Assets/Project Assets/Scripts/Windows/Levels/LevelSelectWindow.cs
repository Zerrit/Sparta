using UnityEngine;

namespace Zparta.Windows.Levels
{
    public class LevelSelectWindow : AbstractWindow
    {
        [field: SerializeField] public LevelButtonView LevelButtonPrefab { get; private set; }
        [field: SerializeField] public Transform ViewPanel { get; private set; }
        [field: SerializeField] public Transform ButtonsContainer { get; private set; }
    }
}