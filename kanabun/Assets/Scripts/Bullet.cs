using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private GameManager GameManager;//Script宣言

	// Use this for initialization
	void Start () {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Destroy(this.gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update ()
    {
        if(transform.position.y <= -50)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameManager.Addpoint(10);
            Destroy(other.gameObject, 1f);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Boss")
        {
            Boss.Damege += 1;
        }

    }

}
