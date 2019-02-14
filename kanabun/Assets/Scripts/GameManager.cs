using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {
    private GameObject StartPanel;
    private GameObject ResultPanel;
    private GameObject PlayPanel;
    public GameObject StartObject;
    public GameObject FinishObject;
    private Text scoretext;
    private Text InfoText;//全体を通してアクティブなテキストUI
    public int score;
    private Text MainTime;
    [SerializeField]
    private float time;
    private float oldtime;
    private int oldscore;
    [SerializeField]
    private bool isPlaying = false;
    [SerializeField]private CSVReader CSVReader;
    private AudioSource[] BGM;
    public GameObject practice;
    private string gameLevelStr;

    void Start()
    {
        InfoText = GameObject.Find("InfoText").GetComponent<Text>();
        StartPanel = GameObject.Find("StartPanel");
        PlayPanel = GameObject.Find("PlayPanel");
        ResultPanel = GameObject.Find("ResultPanel");
        MainTime = GameObject.Find("timeText").GetComponent<Text>();
        scoretext = GameObject.Find("Score").GetComponent<Text>();
        Initialize();
        BGM = gameObject.GetComponents<AudioSource>();
        BGM[0].Play();
        practice.SetActive(true);
    }


    void Update()
    {
        if (isPlaying) { Gameplay(); }
    }

    void Initialize()
    {
        score = 0;
        isPlaying = false;
        time = 90;
        PlayPanel.SetActive(false);
        ResultPanel.SetActive(false);
        StartPanel.SetActive(true);
        InfoText.text = "これをたおすとスタート\n↓";
    }

    public void Addpoint(int point)
    {
        if (score <= 0 && point < 0) { return; }
        score = score + point;
    }

    private void Gameplay()
    {
            if (Mathf.CeilToInt(time) <= 0f)
            {
            BGM[0].Play();
            GoToResult();
                return;
            }

            time -= Time.deltaTime;

            if ((int)oldtime != (int)time)//整数表示でタイムが変わるときだけ
            {
                MainTime.text = (Mathf.CeilToInt(time)).ToString("00");
            }
            if (oldscore != score)//Scoreに変化があったときだけ
            {
                scoretext.text = score.ToString();
            }

            oldscore = score;
            oldtime = time;
    }

    public void WaitFinish()
    {
        Invoke("GameFinish", 3f);
    }

    private void GameFinish()
    {
        SceneManager.LoadScene("Main");
    }

    public void GoToPlay(int gameLevel)
    {
        InfoText.text = "Ready...";
        practice.SetActive(false);
        StartPanel.SetActive(false);
        PlayPanel.SetActive(true);
        
        switch (gameLevel)
        {
            case 1:
            gameLevelStr = "Level1";
            break;

            case 2:
            gameLevelStr = "Level2";
            break;

            case 3:
            gameLevelStr = "Level3";
            break;
        }
        Invoke("WaitPlay",3f);
    }

    private void GoToResult() //time0になるとResultPanelが開く,FinishTriggerを生成
    {
        InfoText.text = "Finish!!";
        CSVReader.GameEnd();
        PlayPanel.SetActive(false);
        isPlaying = false;
        Invoke("InstFinish", 3f);
    }

    private void InstFinish()
    {
        BGM[1].Stop();
        InfoText.text = score.ToString();
        ResultPanel.SetActive(true);
        Instantiate(FinishObject, new Vector3(0, 0.5f, 7.674f), Quaternion.identity);
    }

    private void WaitPlay()
    {
        CSVReader.GameStart(gameLevelStr);
        BGM[0].Stop();
        BGM[1].Play();
        isPlaying = true;
        InfoText.text = null;
    }

 }

