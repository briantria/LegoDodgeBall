/**
 *  created by  : Brian Tria
 *  date        : 28/11/2020 22:21:27
 *  description :
 **/

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Unity.LEGO.Game;
using Unity.LEGO.Behaviours;

namespace LegoDodgeBall
{
    public class DodgeballPlayerController : MonoBehaviour
    {
        [SerializeField] private GameMode m_gameMode;
        [SerializeField] private Transform m_throwerCrosshair;

        [Space(10)]
        [SerializeField] private List<Transform> m_throwerSpawnPoints;
        [SerializeField] private List<Transform> m_dodgerSpawnPoints;

        [Space(10)]
        [SerializeField] private Vector3 m_cameraPositionOffset = Vector3.zero;

        private CinemachineFreeLook m_FreeLookCamera;
        private Camera m_mainCamera;
        private Transform m_chosenSpawnPoint;
        private float m_sensitivity = 1.0f;
        private float m_rotationY = 0.0f;

        private MinifigInputManager m_miniFigController;

        protected void Awake()
        {
            if (!m_gameMode)
            {
                Debug.LogError("Missing game mode reference.");
                return;
            }

            m_miniFigController = this.GetComponent<MinifigInputManager>();

            this.SetupPlayerPosition();
            this.SetupCrosshair();
            this.SetupCamera();
            this.SetupCameraPosition();

            this.ShowCursor(m_gameMode.CurrentGameMode == GameModeFlag.Dodger);

            EventManager.AddListener<LookSensitivityUpdateEvent>(OnLookSensitivityUpdate);
            EventManager.AddListener<OptionsMenuEvent>(OnOptionsMenu);
        }

        protected void OnDestroy()
        {
            this.ShowCursor(true);
            EventManager.RemoveListener<LookSensitivityUpdateEvent>(OnLookSensitivityUpdate);
            EventManager.RemoveListener<OptionsMenuEvent>(OnOptionsMenu);
        }

        protected void Update()
        {
            float rotationX = m_chosenSpawnPoint.localEulerAngles.y + Input.GetAxis("Mouse X") * m_sensitivity * 0.5f;
            m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivity * 0.25f;

            m_chosenSpawnPoint.localEulerAngles = new Vector3(-m_rotationY, rotationX, 0);
            //m_mainCamera.transform.localEulerAngles = new Vector3(-m_rotationY, rotationX, 0);
        }

        #region Private Functions

        private void SetupPlayerPosition()
        {
            switch (m_gameMode.CurrentGameMode)
            {
                case GameModeFlag.Dodger:
                    this.LoadPositionFromList(m_dodgerSpawnPoints);
                    break;

                default:
                    m_miniFigController.enabled = false;
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

            m_mainCamera.transform.rotation = this.transform.rotation;

            Vector3 cameraPosition = Vector3.zero;
            cameraPosition.x += m_mainCamera.transform.right.x + m_cameraPositionOffset.x;
            cameraPosition.y += m_mainCamera.transform.up.y + m_cameraPositionOffset.y;
            cameraPosition.z += m_mainCamera.transform.forward.z + m_cameraPositionOffset.z;
            m_mainCamera.transform.localPosition = cameraPosition;
        }

        private void SetupCrosshair()
        {
            if (m_gameMode.CurrentGameMode != GameModeFlag.Thrower || !m_throwerCrosshair)
            {
                return;
            }

            m_throwerCrosshair.rotation = this.transform.rotation;

            Vector3 crosshairPosition = this.transform.position;
            crosshairPosition += this.transform.forward * 50;
            crosshairPosition.y += 1;
            m_throwerCrosshair.position = crosshairPosition;
        }

        private void ShowCursor(bool visible)
        {
            Cursor.visible = visible;

            if (visible)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
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
            m_chosenSpawnPoint = spawnPoints[randomIndex];
            Vector3 randomPosition = m_chosenSpawnPoint.position;

            Vector3 targetPosition = randomPosition;
            targetPosition.x = 0;
            targetPosition.z = 0;

            this.transform.position = randomPosition;
            this.transform.LookAt(targetPosition);
        }

        #endregion

        #region Broadcast Events

        void OnOptionsMenu(OptionsMenuEvent evt)
        {
            this.ShowCursor(evt.Active);
        }

        void OnLookSensitivityUpdate(LookSensitivityUpdateEvent evt)
        {
            m_sensitivity = evt.Value;
        }

        #endregion
    }
}