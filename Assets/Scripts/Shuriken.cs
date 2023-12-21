using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public float damage = 30f;
    public Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * 20 * Time.deltaTime;
        
        transform.Rotate(0, 0, -360 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collider.tag == "Boss")
        {
            collider.gameObject.GetComponent<Boss>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collider.tag == "Missile")
        {
            Destroy(collider.gameObject);
        }
    }
}
