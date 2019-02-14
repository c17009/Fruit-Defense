using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruit : MonoBehaviour {

    public int score;
    public GameObject enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*if(gameObject == enemy.transform.child)*/
	}

    void OnCollisionEnter(Collision collision)
    {   
        //特定の場所に持ち去られたらポイント減少
        if (collision.gameObject.tag == "xxxx")
        {
            FindObjectOfType<GameManager>().Addpoint(score);

            Destroy(this.gameObject);
        }
    }
}
