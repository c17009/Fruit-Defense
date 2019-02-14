using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pshot : MonoBehaviour {

    private float BulletPower = 500;
    public GameObject bullet;
    private int life = 5;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update() { 
        print(life);
        
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Fire1"))
        {
            Shot();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 1, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -1, 0);
        }
        if(life == 0)
        {
            Destroy(this.gameObject);
        }

	}

    void Shot()
    {
        var BulletIns = GameObject.Instantiate(bullet,this.transform.position, transform.rotation) as GameObject;
        BulletIns.GetComponent<Rigidbody>().AddForce(BulletIns.transform.forward * BulletPower);
        Destroy(BulletIns, 10f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Attack")
        {
            life -= 1;
        }
    }
}
