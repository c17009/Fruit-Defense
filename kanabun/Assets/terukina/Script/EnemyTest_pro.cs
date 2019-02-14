using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest_pro : MonoBehaviour
{
    public Transform Target;
    public float EnemySpeed;
    public Transform escape;
    public GameObject E_sub;

    private Animator anim;
    private GameManager GameManager;
    private int Roty;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {


        if (gameObject.transform.childCount >= 3)
        {
            
            Invoke("Escape", 2f);
           // print("ok");
           
        } else {

            Move();
            //print("non");
        }
                
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            EnemySpeed = 0;
            Instantiate(E_sub, gameObject.transform.position, Quaternion.identity);
            anim.SetTrigger("death");
            Destroy(E_sub, 5f);
        }

        if(other.gameObject.tag == "Stump" )
        {
            transform.GetChild(2).gameObject.transform.position = transform.forward;
            transform.GetChild(2). gameObject.transform.localPosition = new Vector3(0, 5, 5);
        }

        /*if(other.gameObject.tag == "Escape")
        {
            PointLoss();
        }*/
    }

    void Escape()
    {
        anim.SetBool("run", false);
        anim.SetTrigger("have");

        transform.rotation = Quaternion.Euler(0, Roty, 0);
        transform.position += transform.forward * EnemySpeed;
        if(this.transform.position.z >= 15)
        {
            PointLoss();
        }
        //transform.LookAt(escape);
        //transform.position += transform.forward * EnemySpeed;
        //print("escape");
    }

    void Move()
    {
        anim.SetBool("run", true);
        transform.LookAt(Target);
        transform.position += transform.forward * EnemySpeed;
    }

    void PointLoss()
    {
            Destroy(gameObject, 0.15f);
            GameManager.Addpoint(-1);
    }
    IEnumerator Delay()
    {
        SetRandomPosition();
        yield break;
            
    }
    void SetRandomPosition()
    {
        int RandomRoty = Random.Range(0, 5);
        switch (RandomRoty)
        {
            case 0:
                Roty = -60;
                break;
            case 1:
                Roty = 0;
                break;
            case 2:
                Roty = 30;
                break;
            case 3:
                Roty = -30;
                break;
            case 4:
                Roty = 60;
                break;
        }
    }

}
