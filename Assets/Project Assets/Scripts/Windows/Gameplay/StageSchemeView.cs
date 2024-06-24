using UnityEngine;
using UnityEngine.UI;

namespace Zparta.Windows.Gameplay
{
    public class StageSchemeView : MonoBehaviour
    {
        [field:SerializeField] public float DefaultHeight { get; private set; } = 50f;
        [field:SerializeField] public float SelectedHeight { get; private set; } = 100f;
        [field:SerializeField] public Color DefaultColor { get; private set; }
        [field:SerializeField] public Color CompletedColor { get; private set; }
        [field:SerializeField] public Color SelectedColor { get; private set; }
        
        [field:SerializeField] public StageSegmentView[] Segments { get; private set; } = new StageSegmentView[4];
        [field:SerializeField] public StageSegmentView FinalSegments { get; private set; }

        
        public void UnselectAll()
        {
            foreach (var segment in Segments)
            {
                var size = segment.SegmentTransform.sizeDelta;
                segment.SegmentTransform.sizeDelta = new Vector2(size.x, DefaultHeight);
                segment.SegmentImage.color = DefaultColor;
                segment.SelectionMark.gameObject.SetActive(false);
                segment.gameObject.SetActive(false);
            }
            FinalSegments.SegmentImage.color = DefaultColor;
            FinalSegments.SelectionMark.gameObject.SetActive(false);
        }

        public void Select(StageSegmentView segment)
        {
            var size = segment.SegmentTransform.sizeDelta;
            segment.SegmentTransform.sizeDelta = new Vector2(size.x, SelectedHeight);
            segment.SegmentImage.color = SelectedColor;
            segment.SelectionMark.gameObject.SetActive(true);
        }

        public void Complete(StageSegmentView segment)
        {
            var size = segment.SegmentTransform.sizeDelta;
            segment.SegmentTransform.sizeDelta = new Vector2(size.x, DefaultHeight);
            segment.SegmentImage.color = CompletedColor;
            segment.SelectionMark.gameObject.SetActive(false);
        }

        public void SelectFinal()
        {
            FinalSegments.SegmentImage.color = SelectedColor;
            FinalSegments.SelectionMark.gameObject.SetActive(true);
        }
    }
}