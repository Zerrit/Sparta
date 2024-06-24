using UnityEngine;
using UnityEngine.UI;

namespace Zparta.Windows.Gameplay
{
    public class StageSegmentView : MonoBehaviour
    {
        [field:SerializeField] public RectTransform SegmentTransform { get; private set; }
        [field:SerializeField] public Image SegmentImage { get; private set; }
        [field:SerializeField] public Image SelectionMark { get; private set; }
        
        private void Awake()
        {
            if (!SegmentTransform)
                SegmentTransform = GetComponent<RectTransform>();
        }
    }
}