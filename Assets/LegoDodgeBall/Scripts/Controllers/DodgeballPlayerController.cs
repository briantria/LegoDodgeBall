/**
 *  created by  : Brian Tria
 *  date        : 28/11/2020 22:21:27
 *  description :
 **/

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace LegoDodgeBall
{
    public class DodgeballPlayerController : MonoBehaviour
    {
        [SerializeField] private GameMode m_gameMode;
        [SerializeField] private List<Transform> m_throwerSpawnPoints;
        [SerializeField] private List<Transform> m_dodgerSpawnPoints;

        [Space(10)]
        [SerializeField] private Vector3 m_cameraOffset = Vector3.zero;

        private CinemachineFreeLook m_FreeLookCamera;
        private Camera m_mainCamera;
        private Transform m_chosenSpawnPoint;

        protected void Awake()
        {
            if (!m_gameMode)
            {
                Debug.LogError("Missing game mode reference.");
                return;
            }

            this.SetupPlayerPosition();
            this.SetupCamera();
            Debug.Log("setup camera pos...");
            this.SetupCameraPosition();
        }

        // protected void LateUpdate()
        // {
        //     this.SetupCameraPosition();
        // }

        #region Private Functions

        private void SetupPlayerPosition()
        {
            switch (m_gameMode.CurrentGameMode)
            {
                case GameModeFlag.Dodger:
                    this.LoadPositionFromList(m_dodgerSpawnPoints);
                    break;

                default:
                    this.LoadPositionFromList(m_throwerSpawnPoints);
                    break;
            }
        }

        private void SetupCamera()
        {
            m_FreeLookCamera = FindObjectOfType<CinemachineFreeLook>();
            m_mainCamera = Camera.main;

            if (!m_mainCamera || !m_FreeLookCamera)
            {
                Debug.LogError("Missing camera.");
                return;
            }

            bool isDodger = m_gameMode.CurrentGameMode == GameModeFlag.Dodger;
            m_FreeLookCamera.gameObject.SetActive(isDodger);
            CinemachineBrain cinemachineBrain = m_FreeLookCamera.GetComponent<CinemachineBrain>();

            if (cinemachineBrain)
            {
                cinemachineBrain.enabled = isDodger;
            }

            if (isDodger)
            {
                m_mainCamera.transform.SetParent(null);
            }
            else
            {
                m_mainCamera.transform.SetParent(m_chosenSpawnPoint, false);
            }
        }

        private void SetupCameraPosition()
        {
            Debug.Log("setup camera pos...");
            Vector3 cameraPosition = m_mainCamera.transform.localPosition;
            cameraPosition.x += m_mainCamera.transform.right.x + m_cameraOffset.x;
            cameraPosition.y += m_mainCamera.transform.up.y + m_cameraOffset.y;
            cameraPosition.z += m_mainCamera.transform.forward.z + m_cameraOffset.z;

            // Debug.Log("right: " + m_mainCamera.transform.right + ", up: " + m_mainCamera.transform.up + ", forward: " + m_mainCamera.transform.forward);

            m_mainCamera.transform.rotation = this.transform.rotation;
            m_mainCamera.transform.localPosition = cameraPosition;
            // m_mainCamera.transform.localPosition = (m_mainCamera.transform.right) * m_cameraOffset.x;
            // m_mainCamera.transform.localPosition += (m_mainCamera.transform.up) * m_cameraOffset.y;
            // m_mainCamera.transform.localPosition += (m_mainCamera.transform.forward) * m_cameraOffset.z;
            Debug.Log("local pos: " + m_mainCamera.transform.localPosition);
            //m_mainCamera.transform.position = cameraPosition; //this.transform.position + m_cameraOffset;
            //m_mainCamera.transform.forward = this.transform.forward;
            // playerPosition.z 
        }

        private void LoadPositionFromList(List<Transform> spawnPoints)
        {
            if (spawnPoints.Count == 0)
            {
                Debug.LogError("Missing player spawnpoints");
                return;
            }

            int randomIndex = Random.Range(0, spawnPoints.Count);
            m_chosenSpawnPoint = spawnPoints[randomIndex];
            Vector3 randomPosition = m_chosenSpawnPoint.position;

            Vector3 targetPosition = randomPosition;
            targetPosition.x = 0;
            targetPosition.z = 0;

            this.transform.position = randomPosition;
            this.transform.LookAt(targetPosition);
        }

        #endregion
    }
}