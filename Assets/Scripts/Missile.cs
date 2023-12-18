using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, 10 * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        ScoreBoard.score += 1;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<Player>().TakeDamage(5f);
            Destroy(gameObject);
        }
    }
}
