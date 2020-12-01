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
        [SerializeField] private GameMode m_gameMode;
        // [SerializeField] private GameModeFlag m_gameMode;

        [Space(5)]
        [SerializeField] private List<GameObject> m_dodgerGameModeRules = new List<GameObject>();

        [Space(5)]
        [SerializeField] private List<GameObject> m_throwerGameModeRules = new List<GameObject>();

        protected void Awake()
        {
            //this.gameObject.SetActive(m_currentGameMode.InitValue == (int)m_gameMode);

            foreach (GameObject obj in m_dodgerGameModeRules)
            {
                obj.SetActive(m_gameMode.CurrentGameMode == GameModeFlag.Dodger);
            }

            foreach (GameObject obj in m_throwerGameModeRules)
            {
                obj.SetActive(m_gameMode.CurrentGameMode == GameModeFlag.Thrower);
            }
        }
    }
}