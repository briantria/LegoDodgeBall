using System.Runtime.CompilerServices;
/**
 *  created by  : Brian Tria
 *  date        : 27/11/2020 05:23:13
 *  description :
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LegoDodgeBall
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameMode m_gameMode;

        public void PlayDodger()
        {
            m_gameMode.CurrentGameMode = GameModeFlag.Dodger;
            // TODO pickup goal and timelimit
        }

        public void PlayThrower()
        {
            m_gameMode.CurrentGameMode = GameModeFlag.Thrower;
            // TODO pickup goal and timelimit
        }
    }
}