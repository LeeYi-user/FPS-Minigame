using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float cooldown;
    [SerializeField] private int limit;

    private float timer;

    public static int counter;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time + cooldown;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlaySceneManager.gameOver)
        {
            timer = Time.time + cooldown;
            counter = 0;
            return;
        }

        if (counter < limit && EnemySpawner.waves <= 8)
        {
            if (timer <= Time.time)
            {
                Instantiate(prefab, new Vector3(Random.Range(-15f, 15f), 15f, Random.Range(-15f, 15f)), prefab.transform.rotation);
                timer = Time.time + cooldown;
                counter++;
            }
        }
        else
        {
            timer = Time.time + cooldown;
        }
    }
}
