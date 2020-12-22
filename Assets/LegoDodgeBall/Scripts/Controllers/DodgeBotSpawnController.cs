/**
 *  created by  : Brian Tria
 *  date        : 15/12/2020 23:24:43
 *  description :
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LegoDodgeBall
{
    public class DodgeBotSpawnController : MonoBehaviour
    {
        [SerializeField] private GameMode m_gameMode;
        [SerializeField] private Transform m_spawnPoint;
        [SerializeField] private GameObject m_dodgeBotPrefab;

        private const int MAX_DODGEBOT_COUNT = 5;
        private int m_currentDodgeBotCount = 0;

        void OnEnable()
        {
            DodgeBotBehaviour.OnExplode += OnBotExplode;
        }

        void OnDisable()
        {
            DodgeBotBehaviour.OnExplode -= OnBotExplode;
        }

        void Start()
        {
            if (m_gameMode.CurrentGameMode == GameModeFlag.Dodger)
            {
                return;
            }

            this.SpawnBot();
        }

        #region Private Functions

        private void SpawnBot()
        {
            if (m_currentDodgeBotCount >= MAX_DODGEBOT_COUNT || !m_spawnPoint || !m_dodgeBotPrefab)
            {
                return;
            }

            int randomIndex = Random.Range(0, m_spawnPoint.childCount);
            Transform spawnPoint = m_spawnPoint.GetChild(randomIndex);

            GameObject pickupInstance = Instantiate<GameObject>(m_dodgeBotPrefab);
            pickupInstance.name = "DodgeBot - " + System.DateTime.Now.ToString("yyyyMMddHHmmss");
            pickupInstance.transform.position = spawnPoint.position;
            pickupInstance.transform.localScale = Vector3.one;

            m_currentDodgeBotCount++;

            if (m_currentDodgeBotCount < MAX_DODGEBOT_COUNT)
            {
                this.SpawnBot();
            }
        }

        private void OnBotExplode()
        {
            m_currentDodgeBotCount--;
            this.SpawnBot();
        }

        #endregion
    }
}