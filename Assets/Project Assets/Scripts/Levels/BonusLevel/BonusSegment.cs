using TMPro;
using UnityEngine;

namespace Zparta.Levels.BonusLevel
{
    public class BonusSegment : MonoBehaviour
    {
        [SerializeField] private Renderer segmentRenderer;
        [SerializeField] private TMP_Text multiplyerText;

        public void SetText(float value)
        {
            multiplyerText.text = $"X{value}";
        }

        public void SetColor(Color color)
        {
            segmentRenderer.material.color = color;
        }
    }
}