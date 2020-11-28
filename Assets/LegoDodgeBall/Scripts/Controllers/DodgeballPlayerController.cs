/**
 *  created by  : Brian Tria
 *  date        : 28/11/2020 22:21:27
 *  description :
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LegoDodgeBall
{
    public class DodgeballPlayerController : MonoBehaviour
    {
        [SerializeField] private IntVariable m_gameMode;
        [SerializeField] private List<Transform> m_throwerSpawnPoints;
        [SerializeField] private List<Transform> m_dodgerSpawnPoints;

        protected void Awake()
        {
            if (!m_gameMode)
            {
                Debug.LogError("Missing game mode reference.");
                return;
            }

            switch (m_gameMode.RuntimeValue)
            {
                case (int)GameMode.Dodger:
                    this.LoadPositionFromList(m_dodgerSpawnPoints);
                    break;

                default:
                    this.LoadPositionFromList(m_throwerSpawnPoints);
                    break;
            }
        }

        private void LoadPositionFromList(List<Transform> spawnPoints)
        {
            if (spawnPoints.Count == 0)
            {
                Debug.LogError("Missing player spawnpoints");
                return;
            }

            int randomIndex = Random.Range(0, spawnPoints.Count);
            Transform randomTransform = spawnPoints[randomIndex];
            Vector3 randomPosition = randomTransform.position;

            Vector3 targetPosition = randomPosition;
            targetPosition.x = 0;
            targetPosition.z = 0;

            this.transform.position = randomPosition;
            this.transform.LookAt(targetPosition);
        }
    }
}