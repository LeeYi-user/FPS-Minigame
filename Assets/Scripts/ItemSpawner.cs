using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private float timer;

    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time + cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlaySceneManager.gameOver)
        {
            timer = Time.time + cooldown;
            return;
        }

        if (timer <= Time.time)
        {
            timer = Time.time + cooldown;
            Instantiate(prefab, new Vector3(Random.Range(-15f, 15f), 15f, Random.Range(-15f, 15f)), prefab.transform.rotation);
        }
    }
}
