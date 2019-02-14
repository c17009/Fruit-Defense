using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Avatar;

public class InputTest : MonoBehaviour {
    public AudioClip se;
    public OVRInput.Controller controller;
    private AudioSource audiosource;
    public GameObject ball;
	// Use this for initialization
	void Start () {
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = se;
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Debug.Log("Aボタンを押した");
            OnSound();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            Debug.Log("Bボタンを押した");
            OnSound();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.X))
        {
            Debug.Log("Xボタンを押した");
            OnSound();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            Debug.Log("Yボタンを押した");
            OnSound();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.Start))
        {
            Debug.Log("メニューボタン（左アナログスティックの下にある）を押した");
            OnSound();
        }

        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            Debug.Log("右人差し指トリガーを押した");
            OnSound();
            //OnShot();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            Debug.Log("右中指トリガーを押した");
            OnSound();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            Debug.Log("左人差し指トリガーを押した");
            OnSound();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        {
            Debug.Log("左中指トリガーを押した");
            OnSound();
        }
    }

    private void OnSound()
    {
        audiosource.PlayOneShot(audiosource.clip);
    }

    private void OnShot()
    {
        Transform right_position = GameObject.Find("hand_right").GetComponent<Transform>();
        GameObject bullet = Instantiate(ball, right_position.position, ball.transform.rotation);
        Rigidbody bullet_rb = bullet.GetComponent<Rigidbody>();
        bullet_rb.AddRelativeForce(0, 0, 100);
    }
}
