using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] ThingsToSpawn;
    public GameObject[] Spawners;

    private int rand;

    private RoomTemplates templates;

    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
    }

    // Update is called once per frame
    void Update()
    {
        if (templates.waitTime <= 0 && templates.spawnedBoss == true)
        {
            return;
        }
        else if (templates.waitTime <= 0 && templates.spawnedBoss == false)
        {
            foreach(GameObject Spawner in Spawners)
            {
                rand = Random.Range(0, ThingsToSpawn.Length);
                Instantiate(ThingsToSpawn[rand], Spawner.transform.position, Quaternion.identity);
            }
        }
    }
}
