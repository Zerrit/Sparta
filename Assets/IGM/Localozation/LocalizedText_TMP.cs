using TMPro;
using UnityEngine;

namespace IGM.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText_TMP : LocalizedText
    {
        public string m_textName;
        [SerializeField, HideInInspector] TextMeshProUGUI m_text;

        void Awake()
        {
            m_text = GetComponent<TextMeshProUGUI>();
        }

        void OnEnable()
        {
            base.OnEnable();
        }

        void OnDisable()
        {
            base.OnDisable();
        }

        public override void UpdateText()
        {
            m_text = GetComponent<TextMeshProUGUI>();
            m_text.text = LocalizationManager.Instance.GetTextByName(m_textName);
        }
    }
}