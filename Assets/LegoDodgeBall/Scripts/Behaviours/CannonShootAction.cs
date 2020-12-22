/**
 *  created by  : Brian Tria
 *  date        : 04/12/2020 16:55:31
 *  description :
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LEGO.Behaviours;
using Unity.LEGO.Behaviours.Actions;

namespace LegoDodgeBall
{
    public class CannonShootAction : RepeatableAction
    {
        [SerializeField] private GameObject m_Projectile;
        [SerializeField] private Transform m_ShootPoint;

        [SerializeField, Range(1, 100), Tooltip("The velocity of the projectiles.")]
        private float m_Velocity = 25f;

        [SerializeField, Range(0, 100), Tooltip("The accuracy in percent.")]
        private int m_Accuracy = 90;

        [SerializeField, Tooltip("The time in seconds before projectiles disappears.")]
        private float m_Lifetime = 2f;

        [SerializeField, Tooltip("Projectiles are affected by gravity.")]
        private bool m_UseGravity = true;

        override protected void Awake()
        {
            base.Awake();
        }

        protected void Update()
        {
            if (CheckInput())
            {
                // ConditionMet();
                Fire();
            }
        }

        void Fire()
        {
            if (!m_Projectile || !m_ShootPoint)
            {
                return;
            }

            GameObject projectileObj = GameObject.Instantiate<GameObject>(m_Projectile);
            projectileObj.transform.rotation = m_ShootPoint.rotation;
            projectileObj.transform.position = m_ShootPoint.position;
            //projectileObj.SetActive(true);

            Projectile projectile = projectileObj.GetComponent<Projectile>();
            if (projectile)
            {
                projectile.Init(m_ScopedBricks, m_Velocity, m_UseGravity, m_Lifetime);
            }

            // PlayAudio();
        }

        bool CheckInput()
        {
            bool didFire = Input.GetButtonDown("Fire1");
            return didFire;
        }
    }
}