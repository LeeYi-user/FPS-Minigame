using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLandmine : MonoBehaviour
{
    public GameObject landmine;
    float timer;

    public bool gameover;

    // Start is called before the first frame update
    void Start()
    {
        timer = 7;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Instantiate(landmine, new Vector3(Random.Range(-25f, 25f), 15, Random.Range(-25f, 25f)), Quaternion.Euler(-89.98f, 0f, 0f));
            timer = 10;
        }
    }
}
