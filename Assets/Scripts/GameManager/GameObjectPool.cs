using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Role.InstantiateBallSpace;

namespace GameManagerSpace
{
    public class GameObjectPool : MonoBehaviour
    {
        public GameObject prefab;
        public int initailSize = 20;

        private Queue<GameObject> m_pool = new Queue<GameObject>();

        void Awake()
        {
            for (int cnt = 0; cnt < initailSize; cnt++)
            {
                GameObject go = Instantiate(prefab) as GameObject;
                m_pool.Enqueue(go); go.SetActive(false);
            }
        }

        public void ReUse(Vector3 position, Quaternion rotation)
        {
            if (m_pool.Count > 0)
            {
                Debug.Log("Spawn");
                GameObject reuse = m_pool.Dequeue();
                reuse.GetComponent<InstantiateBall>().ReUse();
                reuse.transform.position = position;
                reuse.transform.rotation = rotation;
                reuse.SetActive(true);
            }
            else
            {
                return;
            }
        }

        public void Recovery(GameObject recovery)
        {
            m_pool.Enqueue(recovery);
            recovery.SetActive(false);
        }
    }
}