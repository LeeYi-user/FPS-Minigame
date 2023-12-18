using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public MainSceneManager MainSceneManager;
    public GameObject bossPrefab;
    public GameObject weaponHolder;

    GameObject boss;
    bool bossSummoned;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0 && !bossSummoned)
        {
            weaponHolder.GetComponent<WeaponSwitch>().SelectWeapon(1);

            boss = Instantiate(bossPrefab, new Vector3(5, 15, 0), Quaternion.identity);
            bossSummoned = true;
        }

        if (!boss && bossSummoned)
        {
            MainSceneManager.GameOver("YOU WIN");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
