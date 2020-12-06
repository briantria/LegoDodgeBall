/**
 *  created by  : Brian Tria
 *  date        : 04/12/2020 06:51:35
 *  description :
 **/

using UnityEngine;
using Unity.LEGO.Game;
using Unity.LEGO.Behaviours;

namespace LegoDodgeBall
{
    public enum RotationAxis
    {
        RotateX,
        RotateY,
        RotateXY
    }

    public class MouseLookAtController : MonoBehaviour
    {
        [SerializeField] private GameMode m_gameMode;
        [SerializeField] private RotationAxis m_rotationAxis;

        private bool m_isPaused = false;
        private float m_sensitivity = 1.0f;
        private float m_rotationY = 0.0f;

        #region LifeCycle

        protected void Awake()
        {
            if (!m_gameMode)
            {
                Debug.LogError("Missing game mode reference.");
                return;
            }

            EventManager.AddListener<LookSensitivityUpdateEvent>(OnLookSensitivityUpdate);
            EventManager.AddListener<OptionsMenuEvent>(OnOptionsMenu);
        }

        protected void OnDestroy()
        {
            EventManager.RemoveListener<LookSensitivityUpdateEvent>(OnLookSensitivityUpdate);
            EventManager.RemoveListener<OptionsMenuEvent>(OnOptionsMenu);
        }

        protected void Update()
        {
            if (m_isPaused || m_gameMode.CurrentGameMode != GameModeFlag.Thrower)
            {
                return;
            }

            float rotationX = this.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * m_sensitivity * 0.5f;
            m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivity * 0.25f;

            switch (m_rotationAxis)
            {
                case RotationAxis.RotateX:
                    this.transform.localEulerAngles = new Vector3(0, rotationX, 0);
                    break;

                case RotationAxis.RotateY:
                    this.transform.localEulerAngles = new Vector3(-m_rotationY, 0, 0);
                    break;

                default:
                    this.transform.localEulerAngles = new Vector3(-m_rotationY, rotationX, 0);
                    break;
            }
        }

        #endregion

        #region Broadcast Events

        void OnOptionsMenu(OptionsMenuEvent evt)
        {
            m_isPaused = evt.Active;
        }

        void OnLookSensitivityUpdate(LookSensitivityUpdateEvent evt)
        {
            m_sensitivity = evt.Value;
        }

        #endregion
    }
}