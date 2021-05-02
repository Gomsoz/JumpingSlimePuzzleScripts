using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager gameMgr = null;
    public static GameManager GameMgr
    {
        get
        {
            if (gameMgr == null)
            {
                return null;
            }
            return gameMgr;
        }
    }
    public bool isOk = false;
    StageManager _stageManager;

    #region Action
    public Action<bool> SuccessBlock = null;
    public Action<int> ComboCnt = null;
    #endregion

    #region Script
    public BoardController boardCtr;
    public BlockBehavior blockBh;
    public BoardSetting boardSetting;
    #endregion

    public Slider comboSlider;
    public GameObject slime;

    public static StageInfo stageInfo;
    public static GameResult gameResult;

    #region Property
    private static ComboScript _combo;
    public static ComboScript Combo { get { return _combo; } }
    #endregion

    private static bool paused = false;
    public static bool Paused
    {
        get { return paused; }
        set
        {
            paused = value;
            Time.timeScale = value ? 0 : 1;
        }
    }

    public int stageLevel;
    private int lastLevel;
    public int goalScore;
    public int totalScore;

    public Text totalScoreText;
    public Text levelDigitText;

    public static bool isGameStart;
    public static bool isGameStop;
    public static bool isExpendBlock;
    public static bool isLastStage;

    void Awake()
    {
        if (gameMgr == null)
        {
            gameMgr = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        GameManagerInit();
    }


    void Update()
    {
        Managers.Input.InputUpdate();
        _stageManager.TimeUpdate();

        if (isGameStart == true && isGameStop == false && _stageManager.isTime == false)
        {
            Debug.Log("Load the Next Level... : Launch");
            GamePause();
            LevelChange();
            Debug.Log("Load the Next Level... : Finish");
        }

        if (isGameStop == true && isLastStage == false && Input.GetMouseButtonDown(0))
        {
            _stageManager.resultPanel.SetActive(false);
            GamePause();
            isGameStop = false;
        }
        else if (isLastStage == true && Input.GetMouseButtonDown(0))
        {

            gameResult.score = totalScore;
            gameResult.maxCombo = _combo.maxCombo;
            SceneManager.LoadScene("MainScene");
        }


    }

    public void IsBlockSelected(RaycastHit2D hit2D)
    {
        blockBh = hit2D.collider.gameObject.GetComponent<BlockBehavior>();
        if (boardCtr.AttemptBlock(blockBh.myCol, blockBh.myRow))
        {
            SuccessBlock.Invoke(true);
            totalScoreText.text = totalScore.ToString();
            _combo.StartCombo();
        }
        else
        {
            SuccessBlock.Invoke(false);
            totalScore -= (50 * stageLevel);
            totalScoreText.text = totalScore.ToString();
            _stageManager.DecreaseStageTime();
        }
    }

    public void GameManagerInit()
    {
        Paused = false;
        Debug.Log("Load the GameManager... : Launch");
        _stageManager = GetComponent<StageManager>();
        _stageManager.Init();
        boardCtr = GameObject.Find("Board").GetComponent<BoardController>();
        boardSetting = GameObject.Find("Board").GetComponent<BoardSetting>();
        gameResult = new GameResult();
        _combo = GetComponent<ComboScript>();

        Managers.Input.BlockSelect += IsBlockSelected;


        isGameStart = false;
        stageLevel = 0;
        totalScore = 0;
        lastLevel = 5;
        LevelChange();
        isGameStart = true;
        isGameStop = false;
    }

    public void GamePause()
    {
        Paused = !Paused;
        if (Paused)
        {
            Managers.Sound.Pause(Defines.SoundType.Bgm);
            return;
        }
        Debug.Log("unpause");
        Managers.Sound.UnPause(Defines.SoundType.Bgm);
    }

    public void LevelChange()
    {
        isGameStop = true;
        if (isGameStart)
        {
            Managers.Sound.Play($"Sounds/ResultSound", 1f, Defines.SoundType.ResultSound);
            _stageManager.ShowStageResult(totalScore, _combo.maxCombo);
        }

        if (isLastStage == true)
        {
            _stageManager.SaveResult();
            return;
        }

        totalScore = 0;
        totalScoreText.text = totalScore.ToString();
        stageLevel++;
        levelDigitText.text = stageLevel.ToString();
        stageInfo = StageReader.LoadStage(stageLevel);

        if (stageInfo.col == boardSetting.col && stageInfo.row == boardSetting.row)
        {
            isExpendBlock = true;
        }

        boardSetting.LevelSetting();
        goalScore = stageInfo.goalScore;
        isExpendBlock = false;
        _stageManager.OnStageTime(30);

        if (stageInfo.level == lastLevel)
        {
            isLastStage = true;
        }
    }

}
