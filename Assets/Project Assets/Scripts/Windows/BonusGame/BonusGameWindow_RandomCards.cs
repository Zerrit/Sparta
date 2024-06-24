using Zparta.Services;
using Zparta.Levels.BonusLevel;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Zparta.Windows.BonusGame
{
    public class BonusGameWindow_RandomCards : AbstractWindow
    {
        [SerializeField] GridLayoutGroup m_gridLayoutGroup;
        [SerializeField] BonusLevelCard[] m_cardPrefabs;
        [SerializeField] float m_spawnCardCount = 9;

        IBonusLevelController_RandomCards_Window _controller;
        List<BonusLevelCard> _spawnedCards = new List<BonusLevelCard>();



        void SpawnCards()
        {
            m_gridLayoutGroup.enabled = true;

            for (int i = 0;  i < m_spawnCardCount; i++)
            {
                int randomCardIndex = Random.Range(0, m_cardPrefabs.Length);
                BonusLevelCard newCard = Instantiate(m_cardPrefabs[randomCardIndex], m_gridLayoutGroup.transform);
                newCard.EOn_Open += OnOpenCard;
                _spawnedCards.Add(newCard);

                newCard.Spawn();
            }
        }

        void OnOpenCard(BonusLevelCard card)
        {
            m_gridLayoutGroup.enabled = false;

            card.EOn_ReciveCardInfo += ReciveCardInfo;

            _spawnedCards.Remove(card);
            ClearSpawnedCards();
        }

        void ReciveCardInfo(BonusLevelCard card, BonusInfo bonusInfo)
        {
            _controller.ReciveBonusInfo(bonusInfo);

            Destroy(card.gameObject);
        }

        void ClearSpawnedCards()
        {
            foreach (BonusLevelCard bonusLevelAbstractCard in _spawnedCards)
            {
                bonusLevelAbstractCard.EOn_ReciveCardInfo -= ReciveCardInfo;
                bonusLevelAbstractCard.Despawn();
            }

            _spawnedCards.Clear();
        }

        public override void Show()
        {
            base.Show();
            SpawnCards();
        }


        public void SetController(IBonusLevelController_RandomCards_Window controller)
        {
            _controller = controller;
        }
    }
}