/**
 *  created by  : Brian Tria
 *  date        : 28/11/2020 23:45:16
 *  description :
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LEGO.Game;
using Unity.LEGO.Behaviours.Actions;

namespace LegoDodgeBall
{
    public class CanonController : MovementAction
    {
        [SerializeField] private IntVariable m_gameMode;
        private float m_lookSensitivity = 1;
        private Vector3 m_previousMousePosition;
        private float m_Angle;
        private float m_Offset;

        override protected void Awake()
        {
            base.Awake();
            EventManager.AddListener<LookSensitivityUpdateEvent>(OnLookSensitivityUpdate);
            EventManager.AddListener<OptionsMenuEvent>(OnOptionsMenu);
        }

        override protected void Start()
        {
            base.Start();
            m_previousMousePosition = Input.mousePosition;
            Cursor.visible = false;
        }

        void FixedUpdate()
        {
            m_CurrentTime += Time.fixedDeltaTime;

            switch (m_gameMode.RuntimeValue)
            {
                case (int)GameMode.Dodger:
                    //this.LoadPositionFromList(m_dodgerSpawnPoints);
                    break;

                default:
                    //this.LoadPositionFromList(m_throwerSpawnPoints);
                    this.MouseLookAt();
                    break;
            }
        }

        override protected void OnDestroy()
        {
            base.OnDestroy();
            Cursor.visible = true;
            EventManager.RemoveListener<OptionsMenuEvent>(OnOptionsMenu);
            EventManager.RemoveListener<LookSensitivityUpdateEvent>(OnLookSensitivityUpdate);
        }

        // TODO: consider using Camera Brain rotation

        private void MouseLookAt()
        {
            Vector3 deltaMousePosition = (Input.mousePosition - m_previousMousePosition) * m_lookSensitivity * Time.deltaTime;
            m_previousMousePosition = Input.mousePosition;

            // Rotate bricks.
            // var delta = Mathf.Clamp(m_Angle / m_Time * m_CurrentTime, Mathf.Min(-m_Angle, m_Angle), Mathf.Max(-m_Angle, m_Angle)) - m_Offset;
            var delta = deltaMousePosition.x;
            var worldPivot = transform.position + transform.TransformVector(m_BrickPivotOffset);
            m_Group.transform.RotateAround(worldPivot, transform.up, delta);
            m_Offset += delta;

            // Update model position.
            m_MovementTracker.UpdateModelPosition();
        }

        #region Broadcast Events

        void OnLookSensitivityUpdate(LookSensitivityUpdateEvent evt)
        {
            Debug.Log("look sensitivity: " + evt.Value);
            m_lookSensitivity = evt.Value;
        }

        void OnOptionsMenu(OptionsMenuEvent evt)
        {
            Cursor.visible = evt.Active;
        }

        #endregion
    }
}