using UnityEngine;
using Zparta.Levels.MainLevel;
using Zparta.Levels.Model;

namespace Zparta.Windows.Levels
{
    public interface ILevelSelectController
    {
        void Show();
        void Hide();
    }

    public class LevelSelectController : ILevelSelectController
    {
        private readonly LevelSelectWindow _window;
        private readonly IReadOnlyLevelModel _levelModel;
        private readonly IMainLevelHandler _mainLevelHandler;

        public LevelSelectController(LevelSelectWindow window, IReadOnlyLevelModel levelModel, IMainLevelHandler mainLevelHandler)
        {
            _window = window;
            _levelModel = levelModel;
            _mainLevelHandler = mainLevelHandler;
        }


        public void Show()
        {
            _window.Show();
            //viewPanel.localPosition = new Vector3(Screen.width, 0f, 0f);
            //viewPanel.DOLocalMove(Vector3.zero, .4f).SetEase(Ease.OutBack);
            
            for (int i = 0; i < _levelModel.LevelCount; i++)
            {
                var button = Object.Instantiate(_window.LevelButtonPrefab, _window.ButtonsContainer);
                button.Initialize(i);
                button.OnClick += HandleClick;
            }
        }

        public void Hide()
        {
            _window.Hide();
        }
        
        private void HandleClick(int levelId)
        {
            _mainLevelHandler.InitializeLevel(levelId);
            //_levelsWindowController.ChangeCurrentLevel(levelId);
            Hide();
        }
    }
}