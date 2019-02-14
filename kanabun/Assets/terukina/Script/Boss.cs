using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public static int  Damege = 0;
    public Transform Player;
    public GameObject PlayerPos;
    public GameObject AttackCube;
    private float AttackCubespeed = 500;
    private float Bossspeed = 0.05f;
    private int RandomPoint;
    private int BossLife = 5;
    private int Roty;
    private bool close = true;
    private float timeElapsed;
    Vector3 vecBasePos;
    private GameManager GameManager;

    private Animator anim;

    // Use this for initialization
    void Start () {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Damege = 0;
        StartCoroutine(Delay());
        vecBasePos = transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        //プレイヤーの現在地を取得
        Vector3 PPos = Player.transform.position;
        //ボスの現在地を取得
        Vector3 BPos = this.transform.position;
        //プレイヤーとボスの間の距離を取得
        float Dis = Vector3.Distance(PPos, BPos);
        
        if(Dis >= 2)
        {
            BossClose();
        }
        else
        {
            RightandLeftMove();
        }

        if (Damege >= 50)
        {
            Target();
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            anim.SetTrigger("down");
            Destroy(other.gameObject);
            Damege += 1;
        }
    }
    void RightandLeftMove()
    {
        transform.LookAt(Player);
        if (close)
        {
            transform.position += new Vector3(1f * Time.deltaTime, 0f, 0.01f);
            if(transform.position.x - vecBasePos.x >= 1)
            {
                GameManager.Addpoint(-1);
                close = false;
            }
        }
        else
        {
            transform.position -= new Vector3(1f * Time.deltaTime, 0f, 0.01f);
            if(transform.position.x - vecBasePos.x <= -1)
            {
                GameManager.Addpoint(-1);
                close = true;
            }
        }
    }

    void BossClose()
    {
        if (Damege <= 49)
        {
            Move();
            close = false;
        }
    }
    void Move()
    {
        transform.LookAt(Player);
        transform.position += transform.forward * Bossspeed;
    }
    void Target()
    {
        Bossspeed = 0.03f;
        transform.rotation = Quaternion.Euler(0,Roty,0);
        transform.position += transform.forward * Bossspeed;
        Invoke("WaitBoss", 5f);
    }
    //ふたたびプレイヤーに向かっていく関数
    void WaitBoss()
    {
        Bossspeed = 0.03f;
        Damege = 0;
    }
    //ランダムを一定時間ごとに決める関数
    IEnumerator Delay()
    {
        while (true)
        {
            SetRandomPosition();
            yield return new WaitForSeconds(10);
        }
    }
    //ランダムで、逃げる先を決める関数
    void SetRandomPosition()
    {
        int RandomRoty = Random.Range(0, 3);
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
        }
    }
}
