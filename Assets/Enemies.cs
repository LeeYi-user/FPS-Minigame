using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public GameOver GameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            GameOver.Setup("YOU WIN");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
