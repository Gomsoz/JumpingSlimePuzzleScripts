using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public enum Rank
    {
        S,
        A,
        B,
        C,
        D,
        F
    };

    public int col;
    public int maxCol;//행
    public int row;
    public int maxRow;//렬

    public GameObject resultCanvas;
    public GameObject resultPanel;
    public Slider timeSlider;

    public Text rankText;
    public Text scoreText;
    public Text maxComboText;

    public Rank curRank;

    private float timeSpeed;
    private int maxCombo;
    private int preScore;
    private int curScore;
    private int totalScore;

    private List<int> rankInfo;

    public float stageTime;
    public bool isTime;


    public void Init()
    {
        timeSpeed = 1;
        maxCombo = 0;
    }

    public void TimeUpdate()
    {
        if (isTime)
        {
            StageTime();
        }
    }

    

    public void StageTime()
    {
        stageTime = stageTime - (timeSpeed * Time.deltaTime);
        timeSlider.value = stageTime;

        if(stageTime <= 0)
        {
            isTime = false;
        }
    }

    public void OnStageTime(int maxCombo, int stageTime)
    {
        this.stageTime = stageTime;
        timeSlider.maxValue = stageTime;
        if(maxCombo > this.maxCombo)
        {
            this.maxCombo = maxCombo;
        }
        isTime = true;
    }

    public void OnStageTime(int stageTime)
    {
        this.stageTime = stageTime;
        timeSlider.maxValue = stageTime;
        isTime = true;
    }

    public void DecreaseStageTime()
    {
        stageTime -= 5;
    }

    public void ShowStageResult(int score, int maxCombo)
    {
        resultPanel = resultCanvas.transform.Find("LevelChangePanel").gameObject;
        rankInfo = GameManager.stageInfo.rankList;
        curScore = score;
        this.maxCombo = maxCombo;
        totalScore = preScore + curScore;
        curRank = 0;
        foreach(int item in rankInfo)
        {
            if(curScore >= item)
                break;
            curRank++;
        }
        
        resultPanel.SetActive(true);
        maxComboText.text = maxCombo.ToString();
        scoreText.text = curScore.ToString();
        rankText.text = curRank.ToString();
        preScore = totalScore;

        //OnStageTime(5);
    }

    public void SaveResult()
    {
        StageReader.SaveResult(GameManager.gameResult);
    }
}
