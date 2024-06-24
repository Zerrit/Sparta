using UnityEngine;

namespace Zparta.Levels.BonusLevel
{
    public class BonusRoad : MonoBehaviour
    {
        [SerializeField] private BonusSegment segmentPrefab;

        [SerializeField] private Color[] segmentsColors = new Color[5];


        [ContextMenu("Build")]
        public void BuildRoad()
        {
            int segmentNumber = 0;
            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    BonusSegment segment = Instantiate(segmentPrefab, new Vector3(0, 0,segmentNumber * 3), Quaternion.identity, transform);
                    
                    float t = (float)j / 10;
                    Color newColor = Color.Lerp(segmentsColors[i], segmentsColors[i+1], t);
                    segment.SetColor(newColor);
                    
                    segment.SetText(1f + (segmentNumber * .1f));
                    segmentNumber++;
                }
            }
        }
    }
}