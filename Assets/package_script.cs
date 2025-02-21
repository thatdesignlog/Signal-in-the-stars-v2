using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class package_script : MonoBehaviour
{
    Rigidbody2D rb;

    player_script ps;
    gamestate_script gs;


    public string type;


    public float speed;
    public bool grabbed;

    float initial_velocity;
    public int money_value;

    float edge_of_screen;
    void Start()
    {

        if (type == "basic")
        {
            initial_velocity = 100;
        }
        else if (type == "intermediate")
        {
            initial_velocity = 150;
        }
        gs = FindObjectOfType<gamestate_script>();
        ps = FindObjectOfType<player_script>();
        rb = GetComponent<Rigidbody2D>();


        Vector3 initial_vector = new Vector3(0,0,0);

        if (type == "intermediate")
        {
            initial_vector = new Vector3(0, Random.Range(-.1f,.1f), 0);
        }

        rb.AddForce((transform.right + initial_vector) * initial_velocity);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

    }

    void HandleMovement()
    {
        if (grabbed)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0;
            transform.position = Vector2.MoveTowards(transform.position, ps.transform.position, speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (grabbed)
            {
                gs.packages_collected += 1;
                gs.money += money_value;
                if (type == "intermediate"){
                    gs.red_packages_collected ++;
                
                    
                }
                else if (type == "basic"){
                    gs.grey_packages_collected ++;
                    
                }


                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.tag == "solar flare")
        {
            Debug.Log("hit solar flare");
            Destroy(gameObject);
            
        }
    }
}
