using System.Collections;
using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zparta.Services;
using Zparta.Windows.Victory.Controller;

namespace Zparta.Windows.Victory.Window
{
    public class VictoryWindow : AbstractWindow
    {
        [field:SerializeField] public Button ContinueButton { get; private set; }
        [field:SerializeField] public Image[] StarImages { get; private set; }
        [field:SerializeField] public TextMeshProUGUI LevelNumberText { get; private set; }
        [field:SerializeField] public TextMeshProUGUI PushedEnemiesText{ get; private set; }
        [field:SerializeField] public TextMeshProUGUI TotalScoreText { get; private set; }
        [field:SerializeField] public TextMeshProUGUI EarnedCoins { get; private set; }
    }
}