using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest: MonoBehaviour {
    public float speed = 1;

    void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("up"))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey("down"))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey("right"))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey("left"))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }

        //Addpointテスト

        /* if (Input.GetKeyDown("a")) {
            FindObjectOfType<GameManager>().Addpoint(10);
            Destroy(this.gameObject);
            print("OK"); 
            } */

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            FindObjectOfType<GameManager>().Addpoint(10);

            Destroy(this.gameObject);
        }
    }

   
}
