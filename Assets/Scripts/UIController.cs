using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI moveLeftText;
    public GameObject GameOverScreenobj;
    public GameObject GameScreenobj;
    public GameObject WinScreenobj;
    public int moveLeft;
    public int frogCount;
    public int chapterIndex;
    private bool isFinish;
    void Start()
    {
        GameScreenobj.SetActive(true);
        WinScreenobj.SetActive(false);
        GameOverScreenobj.SetActive(false);
        chapterIndex = PlayerPrefs.GetInt("chapterIndex");
        moveLeftText.text = "Move Left: " + moveLeft.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FrogCountController(string _operation)
    {
        if (!isFinish)
        {
            if (_operation == "add")
                frogCount++;
            else if (_operation == "subtract")
                frogCount--;
            if (frogCount == 0)
                Win();
        }

    }
    public void MoveLeftController()
    {
        if (!isFinish)
        {
            moveLeft--;
            moveLeftText.text = "Move Left: " + moveLeft.ToString();
            if (moveLeft == 0)
                GameOver();
        }
    }
    public void Win()
    {
        WinScreenobj.SetActive(true);
        GameScreenobj.SetActive(false);
        GameOverScreenobj.SetActive(false);
        isFinish = true;
    }
    public void NextChapterButton()
    {
        chapterIndex++;
        if (chapterIndex > 3)
            chapterIndex = 1;

        PlayerPrefs.SetInt("chapterIndex", chapterIndex);
        SceneManager.LoadScene("chapter" + chapterIndex.ToString());
    }
    public void RetryButton()
    {
        if (chapterIndex == 0)
            chapterIndex++;
        SceneManager.LoadScene("chapter" + chapterIndex.ToString());
    }
    void GameOver()
    {
        GameScreenobj.SetActive(false);
        WinScreenobj.SetActive(false);
        GameOverScreenobj.SetActive(true);
        isFinish = true;
    }
}
