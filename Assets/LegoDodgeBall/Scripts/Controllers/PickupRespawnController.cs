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
    public class PickupRespawnController : MonoBehaviour
    {
        [SerializeField] private Transform m_pickUpSpawnPoint;
        [SerializeField] private GameObject m_pickUpPrefab;

        // private List<GameObject> m_pickupInstanceList = new List<GameObject>();
        private List<Transform> m_activePickUpSpawnPoints = new List<Transform>();
        private List<Transform> m_inactivePickUpSpawnPoints = new List<Transform>();
        private PickupTrigger m_pickUpTrigger;

        void Start()
        {
            m_pickUpTrigger = this.GetComponentInChildren<PickupTrigger>();
            this.LoadPickUpSpawnPoints();
        }

        #region Private Functions

        private void SpawnPickup(Vector3 spawnPoint)
        {
            GameObject pickupInstance = Instantiate<GameObject>(m_pickUpPrefab);
            pickupInstance.name = "PickUp - " + System.DateTime.Now.ToString("yyyyMMddHHmmss");
            pickupInstance.transform.position = spawnPoint;
            pickupInstance.transform.localScale = Vector3.one;

            // m_pickupInstanceList.Add(pickupInstance);
            PickupAction pickupAction = pickupInstance.GetComponentInChildren<PickupAction>();

            if (pickupAction != null)
            {
                //Debug.Log("pickup action found!");

                pickupAction.OnCollected += this.PickupCollected;
            }
        }

        private bool GetRandomSpawnPoint(ref Vector3 spawnPoint)
        {
            if (m_inactivePickUpSpawnPoints != null || m_inactivePickUpSpawnPoints.Count == 0)
            {
                // no available spawnpoint
                return false;
            }

            return true;
        }

        private void LoadPickUpSpawnPoints()
        {
            if (!m_pickUpSpawnPoint)
            {
                Debug.LogError("Missing spawnpoints...");
                return;
            }

            // m_activePickUpSpawnPoints = new List<Transform>();
            // m_inactivePickUpSpawnPoints = new List<Transform>();

            foreach (Transform spawnPoint in m_pickUpSpawnPoint)
            {
                if (spawnPoint.tag == "PickUpSpawnPoint")
                {
                    // m_inactivePickUpSpawnPoints.Add(spawnPoint);
                    // Debug.Log("spawn...");
                    this.SpawnPickup(spawnPoint.position);
                }
            }
        }

        #endregion

        void PickupCollected(PickupAction pickup)
        {
            if (m_pickUpTrigger)
            {
                m_pickUpTrigger.Progress++;
            }

            pickup.OnCollected -= this.PickupCollected;
            StartCoroutine(this.Respawn(pickup, 2));
        }

        public IEnumerator Respawn(PickupAction pickup, float t)
        {
            GameObject pickupGameObject = pickup.transform.parent.gameObject;
            Vector3 spawnPoint = pickupGameObject.transform.position;
            //Destroy(pickupGameObject);

            yield return new WaitForSeconds(t);
            this.SpawnPickup(spawnPoint);
        }
    }
}