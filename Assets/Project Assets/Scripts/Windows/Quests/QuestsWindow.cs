using UnityEngine;
using UnityEngine.UI;
using Zparta.Services;

namespace Zparta.Windows.Quests
{
    public class QuestsWindow : AbstractWindow
    {
        [field: SerializeField] public Button HomeButton { get; private set; }
        [field: SerializeField] public Text TimerText { get; private set; }
        [field: SerializeField] public Transform QuestsParent { get; private set; }
        [field: SerializeField] public QuestView[] QuestViews { get; private set; } = new QuestView[3];
        [field: SerializeField] public Button AdNewQuestsButton { get; private set; }
    }
}