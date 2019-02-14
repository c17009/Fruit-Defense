using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject SpawnEnemy;
    private int spawncount;
    public int IntervalCount;
    public static bool BossOn;
    // Use this for initialization
    void Start()
    {
        BossOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (BossOn == true)
        {
            spawncount += 1;
            if (spawncount % IntervalCount == 0)
            {
                Instantiate(SpawnEnemy, transform.position, Quaternion.identity);
                BossOn = false;
            }
        }
    }
}
