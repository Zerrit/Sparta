using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zparta.Levels.MainLevel;
using Zparta.ScoreLogic;
using Zparta.Services;

namespace Zparta.Windows.Gameplay
{
    public class GameplayWindow : AbstractWindow
    {
        [field:SerializeField] public Button HomeButton { get; private set; }
        [field:SerializeField] public GameObject HomeRequestPanel { get; private set; }
        [field:SerializeField] public Button ConfirmHome { get; private set; }
        [field:SerializeField] public Button CancelHome { get; private set; }
        
        [field:SerializeField] public Text LevelNumberText { get; private set; }
        [field:SerializeField] public Text ScoreText { get; private set; }
        [field:SerializeField] public Text ComboText { get; private set; }
        [field:SerializeField] public RectTransform StageClearedView { get; private set; }
        [field:SerializeField] public RectTransform PowerUpView { get; private set; }
        [field:SerializeField] public StageSchemeView LevelSchemeView { get; private set; }

        private Tween _scaleTween; 
        private Tween _fadeTween;

        
        /// <summary>
        /// Обновляет отображение комбо счётчика.
        /// </summary>
        /// <param name="combo"> Значение комбо. </param>
        public void UpdateComboView(int combo)
        {
            if (combo == 0)
            {
                _fadeTween?.Kill();
                _fadeTween = ComboText.DOFade(0f, .5f);
            }
            else
            {
                ComboText.text = $"combo\nX{combo.ToString()}";

                if (ComboText.color.a < 1)
                {
                    _fadeTween?.Kill();
                    _fadeTween = ComboText.DOFade(1f, .1f);
                }

                _scaleTween?.Kill();
                ComboText.rectTransform.localScale = new Vector3(1f,1f,1f);
                _scaleTween = ComboText.rectTransform.DOPunchScale(new Vector3(1.3f, 1.3f, 1f), .5f, 5);
            }

        }

        /// <summary>
        /// Выводит уведомление о зачистке уровня.
        /// </summary>
        public void ShowStageClearNotification()
        {
            StageClearedView.localScale = new Vector3(.3f, .3f, 1f);
            StageClearedView.gameObject.SetActive(true);

            StageClearedView.DOScale(Vector3.one, .2f).OnComplete(() =>
            {
                StageClearedView.DOScale(new Vector3(.3f, .3f, 1f), .2f).SetDelay(1.5f).OnComplete(() =>
                {
                    StageClearedView.gameObject.SetActive(false);
                });
            });
        }
        
        /// <summary>
        /// Выводит уведомление о подборе усиления.
        /// </summary>
        public void ShowPowerUpNotification()
        {
            PowerUpView.localScale = new Vector3(.3f, .3f, 1f);
            PowerUpView.gameObject.SetActive(true);

            PowerUpView.DOScale(Vector3.one, .2f).OnComplete(() =>
            {
                PowerUpView.DOScale(new Vector3(.3f, .3f, 1f), .2f).SetDelay(1.5f).OnComplete(() =>
                {
                    PowerUpView.gameObject.SetActive(false);
                });
            });
        }
    }
}