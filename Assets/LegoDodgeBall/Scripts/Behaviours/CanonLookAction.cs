using System;
/**
 *  created by  : Brian Tria
 *  date        : 28/11/2020 23:45:16
 *  description :
 **/

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Unity.LEGO.Game;
using Unity.LEGO.Behaviours.Actions;

namespace LegoDodgeBall
{
    public class CanonLookAction : MovementAction
    {
        [SerializeField] private IntVariable m_gameMode;

        private CinemachineFreeLook m_FreeLookCamera;
        private Vector3 m_previousLookDirection;

        override protected void Awake()
        {
            base.Awake();
            m_FreeLookCamera = FindObjectOfType<CinemachineFreeLook>();
            EventManager.AddListener<OptionsMenuEvent>(OnOptionsMenu);
        }

        override protected void Start()
        {
            base.Start();
            m_previousLookDirection = this.transform.position - m_FreeLookCamera.transform.position;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void FixedUpdate()
        {
            m_CurrentTime += Time.fixedDeltaTime;

            switch (m_gameMode.InitValue)
            {
                case (int)GameMode.Dodger:
                    break;

                default:
                    this.MouseLookAt();
                    break;
            }
        }

        override protected void OnDestroy()
        {
            base.OnDestroy();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            EventManager.RemoveListener<OptionsMenuEvent>(OnOptionsMenu);
        }

        private void MouseLookAt()
        {
            if (!m_FreeLookCamera)
            {
                return;
            }

            // Math.Atan2(b.Y - a.Y,b.X - a.X);
            Vector3 currentLookDirection = this.transform.position - m_FreeLookCamera.transform.position;
            double deltaAxisAngleY = Math.Atan2(currentLookDirection.z - m_previousLookDirection.z, currentLookDirection.x - m_previousLookDirection.x);
            m_previousLookDirection = currentLookDirection;
            Debug.Log("delta: " + deltaAxisAngleY);

            // Vector3 lookAtPoint = (this.transform.position - m_FreeLookCamera.transform.position) * 20;
            // lookAtPoint.y = this.transform.position.y;
            // m_Group.transform.LookAt(lookAtPoint); //, m_FreeLookCamera.transform.up);


            // Vector3 deltaMousePosition = (Input.mousePosition - m_previousMousePosition) * m_lookSensitivity * Time.deltaTime;
            // m_previousMousePosition = Input.mousePosition;

            // // Rotate bricks.
            // // var delta = Mathf.Clamp(m_Angle / m_Time * m_CurrentTime, Mathf.Min(-m_Angle, m_Angle), Mathf.Max(-m_Angle, m_Angle)) - m_Offset;
            float delta = (float)deltaAxisAngleY;
            // var delta = deltaMousePosition.x;
            var worldPivot = transform.position + transform.TransformVector(m_BrickPivotOffset);
            m_Group.transform.RotateAround(worldPivot, transform.up, delta);
            //m_Offset += delta;

            // Update model position.
            m_MovementTracker.UpdateModelPosition();
        }

        #region Broadcast Events

        void OnOptionsMenu(OptionsMenuEvent evt)
        {
            Cursor.visible = evt.Active;

            if (evt.Active)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        #endregion
    }
}