/**
 *  created by  : Brian Tria
 *  date        : 28/11/2020 04:58:42
 *  description :
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LegoDodgeBall
{
    public enum GameMode
    {
        Dodger = 0,
        Thrower = 1,
        All = int.MaxValue
    }

    public class GameModeController : MonoBehaviour
    {
        [SerializeField] private IntVariable m_gameMode;
        [SerializeField] private GameMode m_gameModeFlag;
        [SerializeField] private List<Vector3> m_gameModePosition;// = new List<Vector3>();

        protected void Awake()
        {
            // Debug.Log("game mode: " + m_gameMode.RuntimeValue);
            // Debug.Log("game mode flag: " + m_gameModeFlag);

            if (m_gameModePosition != null && m_gameModePosition.Count > 1)
            {
                this.transform.position = m_gameModePosition[m_gameMode.RuntimeValue];

                Vector3 lookAtTarget = this.transform.position;
                lookAtTarget.x = 0;
                lookAtTarget.z = 0;
                this.transform.LookAt(lookAtTarget);


                return;
            }

            this.gameObject.SetActive(m_gameMode.RuntimeValue == (int)m_gameModeFlag);
        }
    }
}