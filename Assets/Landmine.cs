using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.gameObject.GetComponent<Player>().TakeDamage(30f);
            GameObject impactGO = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(impactGO, 2f);
            Destroy(gameObject);
        }
    }
}
