using System;
using System.Collections.Generic;
using UnityEngine;
using Zparta.Configs.Missions;
using Zparta.Factories.Quests;
using Zparta.MissionLogic.Missions;
using Zparta.Services;
using Zparta.WalletLogic;
using Zparta.Windows.Quests;

namespace Zparta.MissionLogic
{
    public class MissionController : IMissionController
    {
        private DateTime _lastRefreshTime;
        private TimeSpan _timeToRefresh = TimeSpan.FromHours(12);

        private const int MissionSlotsAmount = 3;
        
        private readonly Queue<string> _availableMissionsId;
        
        private readonly AbstractMission[] _currentMissions = new AbstractMission[3];

        private readonly QuestsWindow _questsWindow;
        private readonly IQuestFactory _questFactory;
        private readonly MissionPoolConfig _missionPoolConfig;
        private readonly IRewardable _wallet;
        private readonly IUpdateService _updater;


        public MissionController(QuestsWindow questsWindow, IQuestFactory questFactory, 
            MissionPoolConfig missionPoolConfig, IRewardable wallet, IUpdateService updater)
        {
            _questsWindow = questsWindow;
            _questFactory = questFactory;
            _missionPoolConfig = missionPoolConfig;
            _wallet = wallet;
            _updater = updater;

            _availableMissionsId = new Queue<string>();
            
            //TODO Проверка на сохранения и подгрузка актуальных заданий.
            
            _lastRefreshTime = DateTime.Now;
            
            foreach (var quest in missionPoolConfig.Quests)
            {
                _availableMissionsId.Enqueue(quest.Data.MissionId);
            }

            GetMissions();

            for (int i = 0; i < 3; i++)
            {
                _questsWindow.QuestViews[i].MissionId = i;
                _questsWindow.QuestViews[i].OnClicked += TryFinishMission;
            }
        }

        
        void IMissionController.ShowMissionWindow()
        {
            if (IsRerfreshTimeUp())
            {
                Debug.Log("Время закончилось и миссии обновлены");
                GetMissions();
                _lastRefreshTime = DateTime.Now;
            }
            
            DisplayMissionViews();
            _questsWindow.Show();
            _questsWindow.HomeButton.onClick.AddListener(HideMissionWindow);

            _questsWindow.AdNewQuestsButton.onClick.AddListener(ShowAdAndGetMissions);
            _updater.OnTick += UpodateTimer;
        }

        public void HideMissionWindow()
        {
            _updater.OnTick -= UpodateTimer;
            _questsWindow.AdNewQuestsButton.onClick.RemoveListener(ShowAdAndGetMissions);
            _questsWindow.HomeButton.onClick.RemoveListener(HideMissionWindow);
            
            _questsWindow.Hide();

        }

        private void GetMissions()
        {
            for (int i = 0; i < MissionSlotsAmount; i++)
            {
                if (_currentMissions[i] == null)
                {
                    var questId = _availableMissionsId.Dequeue();
                    
                    _currentMissions[i] = _questFactory.Create(questId);
                    _currentMissions[i].SetQuestData(_missionPoolConfig.GetData(questId));
                    _currentMissions[i].StartTracking();
                }
            }
        }
        
        private void DisplayMissionViews()
        {
            ClearMissionView();
            
            for (int i = 0; i < _currentMissions.Length; i++)
            {
                if (_currentMissions[i] == null)
                {
                    _questsWindow.QuestViews[i].QuestInfo.SetActive(false);
                }
                else if (_currentMissions[i].IsCompleted)
                {
                    DisplayAsComplete(_questsWindow.QuestViews[i]);
                }
                else
                {
                    UpdateMissionView(_questsWindow.QuestViews[i], _currentMissions[i].Data);
                }
            }
        }

        private void DisplayAsComplete(QuestView missionView)
        {
            missionView.QuestInfo.SetActive(true);
            
            missionView.Background.color = missionView.CompletedColor;
            missionView.ProgressSlider.value = 1;
            missionView.CheckIcon.enabled = true;
            missionView.ProgressText.enabled = false;
        }

        private void UpdateMissionView(QuestView view, MissionData data)
        {
            view.QuestInfo.SetActive(true);

            view.DescriptionLocId.m_textName = data.DescriptionLocId;
            view.DescriptionLocId.UpdateText();
            view.CheckIcon.enabled = false;
            view.ProgressText.enabled = true;
            view.RewardValue.text = data.RewardValue.ToString();
            view.ProgressText.text = $"{data.CurrentProgress}/{data.RequirmentValue}";
            view.ProgressSlider.value = (float) data.CurrentProgress/ data.RequirmentValue;
        }

        private void ClearMissionView()
        {
            for (int i = 0; i < _currentMissions.Length; i++)
            {
                _questsWindow.QuestViews[i].Background.color = _questsWindow.QuestViews[i].InProgressColor;
                _questsWindow.QuestViews[i].QuestInfo.SetActive(false);
                _questsWindow.QuestViews[i].CompletedInfo.SetActive(false);
            }
        }

        private void TryFinishMission(int missionId)
        {
            if(_currentMissions[missionId] == null) return;
            
            if (_currentMissions[missionId].IsCompleted)
            {
                _questsWindow.QuestViews[missionId].Background.color = _questsWindow.QuestViews[missionId].InProgressColor;
                _questsWindow.QuestViews[missionId].QuestInfo.SetActive(false);
                _wallet.Reward(_currentMissions[missionId].Data.RewardValue);
                _availableMissionsId.Enqueue(_currentMissions[missionId].Data.MissionId);
                _currentMissions[missionId] = null;
            }
        }

        private void ShowAdAndGetMissions()
        {
            GetMissions();
            DisplayMissionViews();
            _lastRefreshTime = DateTime.Now;
        }
        
        private void UpodateTimer()
        {
            TimeSpan timeElapsed = DateTime.Now - _lastRefreshTime;
            TimeSpan timeLeft = _timeToRefresh - timeElapsed;

            string timerString = $"{timeLeft.Hours:D2}:{timeLeft.Minutes:D2}:{timeLeft.Seconds:D2}";

            _questsWindow.TimerText.text = timerString;
        }

        private bool IsRerfreshTimeUp()
        {
            TimeSpan timeElapsed = DateTime.Now - _lastRefreshTime;
            TimeSpan timeLeft =  _timeToRefresh - timeElapsed;

            return timeLeft <= TimeSpan.Zero;
        }
    }
}