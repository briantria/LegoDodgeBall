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

        override protected void Awake()
        {
            base.Awake();
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
        }

        private void MouseLookAt()
        {
            m_MovementTracker.UpdateModelPosition();
        }
    }
}