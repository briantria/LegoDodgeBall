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
    public class CannonLookAction : MovementAction
    {
        [SerializeField] private GameMode m_gameMode;

        private float m_sensitivity = 1.0f;

        override protected void Awake()
        {
            base.Awake();
            EventManager.AddListener<LookSensitivityUpdateEvent>(OnLookSensitivityUpdate);
        }

        override protected void Start()
        {
            base.Start();
        }

        void FixedUpdate()
        {
            m_CurrentTime += Time.fixedDeltaTime;

            switch (m_gameMode.CurrentGameMode)
            {
                case GameModeFlag.Dodger:
                    break;

                default:
                    this.MouseLookAt();
                    break;
            }
        }

        override protected void OnDestroy()
        {
            base.OnDestroy();
            EventManager.AddListener<LookSensitivityUpdateEvent>(OnLookSensitivityUpdate);
        }

        private void MouseLookAt()
        {
            m_MovementTracker.UpdateModelPosition();
        }

        #region Broadcast Events

        void OnLookSensitivityUpdate(LookSensitivityUpdateEvent evt)
        {
            m_sensitivity = evt.Value;
        }

        #endregion
    }
}