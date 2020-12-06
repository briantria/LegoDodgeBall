/**
 *  created by  : Brian Tria
 *  date        : 07/12/2020 00:12:09
 *  description :
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LEGO.Minifig;

namespace LegoDodgeBall
{
    public class DodgeBotBehaviour : MonoBehaviour
    {
        private MinifigController m_minifigController;
        private Vector3 m_nextMoveTarget;

        void Awake()
        {
            m_minifigController = this.GetComponent<MinifigController>();

            if (!m_minifigController)
            {
                m_minifigController.SetInputEnabled(false);
            }
        }

        void Start()
        {
            // m_nextMoveTarget = Random.insideUnitSphere * 20;
            // m_nextMoveTarget.y = 0;
            ChooseAnotherPoint();
        }

        void TurnBack()
        {
            m_minifigController.TurnTo(this.transform.forward * -1, 0, ChooseAnotherPoint);
        }

        void ChooseAnotherPoint()
        {
            m_nextMoveTarget = Random.insideUnitSphere * 20;
            m_nextMoveTarget.y = 0;
            m_minifigController.TurnTo(m_nextMoveTarget, 0, GoToMoveTarget, 0);
        }

        void GoToMoveTarget()
        {
            m_minifigController.MoveTo(m_nextMoveTarget, 0, ChooseAnotherPoint, Random.Range(0, 3));
        }

        // void Update()
        // {
        //     m_minifigController.
        // }
    }
}