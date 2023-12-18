using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public static int selectedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        selectedWeapon = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectWeapon(int index)
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == index)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
        }

        selectedWeapon = index;
    }
}
