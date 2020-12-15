/**
 *  created by  : Brian Tria
 *  date        : 07/12/2020 00:12:09
 *  description :
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LEGO.Minifig;
using Unity.LEGO.Behaviours;

namespace LegoDodgeBall
{
    public class DodgeBotBehaviour : MonoBehaviour
    {
        private MinifigController m_minifigController;
        private CharacterController m_controller;
        private Animator m_animator;

        private Vector3 m_nextMoveTarget;
        private Vector3 m_moveDelta = Vector3.zero;
        private bool m_airborne = false;
        private bool m_didJump = false;
        private bool m_didDoubleJump = false;
        private float m_gravityEffectDelay = 0.0f;

        int m_jumpHash = Animator.StringToHash("Jump");
        int m_groundedHash = Animator.StringToHash("Grounded");

        #region Life Cycle

        void Awake()
        {
            m_minifigController = this.GetComponent<MinifigController>();
            m_animator = this.GetComponent<Animator>();
            m_controller = this.GetComponent<CharacterController>();

            if (m_minifigController)
            {
                m_minifigController.SetInputEnabled(false);
            }
        }

        void Start()
        {
            ChooseAnotherPoint();
        }

        void Update()
        {
            this.Jump();
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.collider.CompareTag("DodgeBallProjectile"))
            {
                if (m_minifigController)
                {
                    m_minifigController.Explode();
                }
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("DodgeBallProjectile"))
            {
                if (m_minifigController)
                {
                    m_minifigController.Explode();
                }
            }
        }

        #endregion

        #region Private Functions

        void ChooseAnotherPoint()
        {
            if (!m_minifigController)
            {
                return;
            }

            m_nextMoveTarget = Random.insideUnitSphere * 20;
            m_nextMoveTarget.y = 0;
            m_minifigController.TurnTo(m_nextMoveTarget, 0, GoToMoveTarget, 0);
        }

        void GoToMoveTarget()
        {
            if (!m_minifigController)
            {
                return;
            }

            m_didJump = true;
            m_minifigController.MoveTo(m_nextMoveTarget, 0, ChooseAnotherPoint, Random.Range(0, 3));
        }

        void Jump()
        {
            if (!m_controller.isGrounded && m_gravityEffectDelay <= 0.01f)
            {
                if (!m_didDoubleJump)
                {
                    m_didDoubleJump = true;
                    m_gravityEffectDelay = 0.5f;
                    m_moveDelta.y += m_minifigController.jumpSpeed;
                    m_animator.SetTrigger(m_jumpHash);
                }
                else
                {
                    m_moveDelta.y -= m_minifigController.gravity * Time.deltaTime;
                }
            }

            if (m_controller.isGrounded && m_didJump)
            {
                m_didDoubleJump = false;
                m_airborne = true;
                m_didJump = false;
                m_gravityEffectDelay = 0.5f;
                m_moveDelta.y = m_minifigController.jumpSpeed;
                m_animator.SetTrigger(m_jumpHash);
            }

            if (this.gameObject.activeInHierarchy)
            {
                m_gravityEffectDelay -= Time.deltaTime;
                m_controller.Move(m_moveDelta * Time.deltaTime);

                if (m_controller.isGrounded)
                {
                    m_animator.SetTrigger(m_groundedHash);
                    m_airborne = false;
                    m_moveDelta.y = 0;
                }
            }
        }

        #endregion
    }
}