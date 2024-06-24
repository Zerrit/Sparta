using System;
using System.Collections.Generic;
using UnityEngine;

namespace IGM.Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        #region Singelton
        private static LocalizationManager _instance;
        public static LocalizationManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = GameObject.FindObjectOfType<LocalizationManager>();

                return _instance;
            }
        }
        #endregion

        [SerializeField] Language[] m_languages;
        [SerializeField, HideInInspector] string[] allTexts;

        string text;

        public Action OnChangeLanguage;
        public int CurrentLangIndex { get; private set; }


        void Start()
        {
            SetLanguage("RUS");
        }

        void GetTexts()
        {
            allTexts = text.Split("//");
            for (int i = 0; i < allTexts.Length; i++)
            {
                allTexts[i] = allTexts[i].Trim();
            }
        }

        [ContextMenu("Cheack text same names")]
        void CheackTextSameNames()
        {
            GetTexts();

            var dict = new Dictionary<string, int>();
            bool allGood = true;

            for (int i = 0; i < allTexts.Length; i++)
            {
                if ((i % 2) == 0)
                {
                    dict.TryGetValue(allTexts[i], out int count);
                    dict[allTexts[i]] = count + 1;
                }
            }

            foreach (var pair in dict)
            {
                if (pair.Value > 1)
                {
                    Debug.LogError("Name <<" + pair.Key + ">> occurred " + pair.Value + " times.");
                    allGood = false;
                }
            }

            if (allGood)
                print("No same names");
        }

        public void SetLanguage(int langId)
        {
            text = m_languages[langId].Text;

            GetTexts();

            OnChangeLanguage?.Invoke();
        }
        
        public void SetLanguage(string language)
        {
            CurrentLangIndex = GetLangIndex(language);
            //string currentLang = language;
            text = m_languages[CurrentLangIndex].Text;

            GetTexts();

            OnChangeLanguage?.Invoke();
        }

        public int GetLangIndex(string _lang)
        {
            int langIndex = 0;

            for (int i = 0; i < m_languages.Length; i++)
            {
                if (m_languages[i].Lang == _lang)
                {
                    langIndex = i;
                    break;
                }
            }

            return langIndex;
        }

        public string GetTextByName(string textName)
        {
            int index = Array.IndexOf(allTexts, textName);
            return allTexts[index + 1];
        }




        [Serializable]
        class Language
        {
            [SerializeField] string m_lang;

            [Tooltip("Format:\n" +
            "Name1//\n" +
            "Text1\n" +
            "//Name2//\n" +
            "Text2\n" +
            "//Name3//\n" +
            "Text3")]
            [SerializeField] TextAsset m_text;

            public string Lang => m_lang;
            public string Text => m_text.text;
        }
    }
}