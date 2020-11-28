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
    public class GameModeController : MonoBehaviour
    {
        [SerializeField] private IntVariable m_currentGameMode;
        [SerializeField] private GameMode m_gameMode;

        protected void Awake()
        {
            this.gameObject.SetActive(m_currentGameMode.RuntimeValue == (int)m_gameMode);
        }
    }
}