using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{
    public Text scoreText;
    public Text maxComboText;

    GameResult gameResult;

    void OnEnable()
    {
        gameResult = StageReader.LoadResult();
        scoreText.text = gameResult.score.ToString();
        maxComboText.text = gameResult.maxCombo.ToString();
    }
}
