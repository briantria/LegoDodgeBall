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
    public class MainMenucontroller : MonoBehaviour
    {
        [SerializeField] private IntVariable m_gameMode;

        public void PlayDodger()
        {
            m_gameMode.RuntimeValue = 0;
        }

        public void PlayThrower()
        {
            m_gameMode.RuntimeValue = 1;
        }
    }
}