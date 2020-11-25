/**
 *  created by  : Brian Tria
 *  date        : 24/11/2020 00:44:48
 *  description :
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LEGO.Behaviours.Actions;
using Unity.LEGO.Behaviours.Triggers;

namespace LegoDodgeBall
{
    [RequireComponent(typeof(PickupTrigger))]
    public class PickupRespawnManager : MonoBehaviour
    {

        [SerializeField, Tooltip("Pickup Prefab")]
        private GameObject m_pickUpPrefab;
        private GameObject m_pickupInstance;

        private PickupTrigger m_pickUpTrigger;

        // TODO: set max pickup Goal = 9999999;
        // TODO: programatically spawn pickup and add listener
        // TODO: update progress onCollect
        // TODO: spawn / activate another pickup onCollect

        void Awake()
        {
            m_pickUpTrigger = this.GetComponent<PickupTrigger>();
        }

        void Start()
        {
            this.SpawnPickup();
        }

        private void SpawnPickup()
        {
            m_pickupInstance = GameObject.Instantiate<GameObject>(m_pickUpPrefab);
            m_pickupInstance.name = "PickUp - " + System.DateTime.Now.ToString("yyyyMMddHHmmss");
            m_pickupInstance.transform.localPosition = this.transform.position + Vector3.up * 2;
            m_pickupInstance.transform.localScale = Vector3.one;

            PickupAction pa = m_pickupInstance.GetComponentInChildren<PickupAction>();
            if (pa != null)
            {
                //Debug.Log("pickup action found!");
                pa.OnCollected += this.PickupCollected;
            }
        }

        void PickupCollected(PickupAction pickup)
        {
            pickup.OnCollected -= this.PickupCollected;
            StartCoroutine(this.Respawn(pickup, 2));
        }

        public IEnumerator Respawn(PickupAction pickup, float t)
        {
            //Debug.Log("RESPAWN in 2 seconds...");
            yield return new WaitForSeconds(t);

            if (m_pickupInstance)
            {
                Destroy(m_pickupInstance);
            }

            //Debug.Log("RESPAWN!");
            this.SpawnPickup();
        }
    }
}