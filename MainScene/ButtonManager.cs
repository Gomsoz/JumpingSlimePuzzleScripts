using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Canvas mainCanvus;
    public GameObject resultPanel;

    public void QuitSystem()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Update()
    {
        if (resultPanel != null && Input.GetMouseButtonDown(0))
        {
            resultPanel.SetActive(false);
            resultPanel = null;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("PuzzleScene");
    }

    public void OnResultPanel()
    {
        resultPanel = mainCanvus.transform.Find("ResultPanel").gameObject;
        resultPanel.SetActive(true);
    }
}
