using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject prefab;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, Random.Range(2, 5));
    }

    void Spawn()
    {
        Enemy ActualEnemy = Instantiate(prefab,transform.position,Quaternion.identity).GetComponent<Enemy>();
        ActualEnemy.target = player;
    }
}
