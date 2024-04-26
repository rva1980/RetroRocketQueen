using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HighScoresMenu : MonoBehaviour
{
    public Canvas uiCanvas;

    public Button backButton;

    public Text line1;
    public Text line2;
    public Text line3;
    public Text line4;
    public Text line5;
    public Text line6;

    public float secondsWait;

    public AudioSource buttonPressed;
    //public AudioSource menuMusic;

    private GameObject _selectedObject;


    void Start()
    {
        //int height = Screen.height;
        int _scaleFactor = GetScaleFactor();
        uiCanvas.scaleFactor = _scaleFactor;

        backButton.Select();

        _selectedObject = new GameObject();
    }

    void Update()
    {
        //int height = Screen.height;
        int _scaleFactor = GetScaleFactor();
        uiCanvas.scaleFactor = _scaleFactor;

        if (Input.GetButtonDown("Cancel"))
        {
            Back();
        }


        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(_selectedObject);
        }
        else
        {
            _selectedObject = EventSystem.current.currentSelectedGameObject;
        }
    }

    void Awake()
    {
        //menuMusic.time = PlayerPrefs.GetFloat("MenuMusicTime", 0f);

        string jsonString = PlayerPrefs.GetString("HighScoresTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        if (highScores == null)
        {
            // There's no stored table, initialize
            AddHighScoreEntry(10000, "rva");
            AddHighScoreEntry(8000, "clm");
            AddHighScoreEntry(6000, "osc");
            AddHighScoreEntry(4000, "raq");
            AddHighScoreEntry(2000, "pau");
            AddHighScoreEntry(1000, "mar");

            jsonString = PlayerPrefs.GetString("HighScoresTable");
            highScores = JsonUtility.FromJson<HighScores>(jsonString);
        }

        // Sort entry list by Score
        for (int i = 0; i < highScores.highScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highScores.highScoreEntryList.Count; j++)
            {
                if (highScores.highScoreEntryList[j].score > highScores.highScoreEntryList[i].score)
                {
                    // Swap
                    HighScoreEntry tmp = highScores.highScoreEntryList[i];
                    highScores.highScoreEntryList[i] = highScores.highScoreEntryList[j];
                    highScores.highScoreEntryList[j] = tmp;
                }
            }
        }

        for (int i = 0; i < 6; i++)
        {
            int rank = i + 1;
            switch (rank)
            {
                case 1: line1.text = " " + rank + "st  " + highScores.highScoreEntryList[i].name + "  " + FormatScore(highScores.highScoreEntryList[i].score, 5); break;
                case 2: line2.text = " " + rank + "nd  " + highScores.highScoreEntryList[i].name + "  " + FormatScore(highScores.highScoreEntryList[i].score, 5); break;
                case 3: line3.text = " " + rank + "rd  " + highScores.highScoreEntryList[i].name + "  " + FormatScore(highScores.highScoreEntryList[i].score, 5); break;
                case 4: line4.text = " " + rank + "th  " + highScores.highScoreEntryList[i].name + "  " + FormatScore(highScores.highScoreEntryList[i].score, 5); break;
                case 5: line5.text = " " + rank + "th  " + highScores.highScoreEntryList[i].name + "  " + FormatScore(highScores.highScoreEntryList[i].score, 5); break;
                case 6: line6.text = " " + rank + "th  " + highScores.highScoreEntryList[i].name + "  " + FormatScore(highScores.highScoreEntryList[i].score, 5); break;
            }
        }

    }

    private string FormatScore(int score, int digits)
    {
        string scoreText = score.ToString();

        while (scoreText.Length < digits)
        {
            scoreText = "0" + scoreText;
        }
        if (scoreText.Length > digits)
        {
            scoreText = "";
            while (scoreText.Length < digits)
            {
                scoreText = "9" + scoreText;
            }
        }

        return scoreText;
    }

    public void Back()
    {
        buttonPressed.Play();
        backButton.GetComponent<Animator>().SetBool("Pressed", true);
        StartCoroutine("LoadBack");
    }
    IEnumerator LoadBack()
    {
        yield return new WaitForSeconds(secondsWait);
        //PlayerPrefs.SetFloat("MenuMusicTime", menuMusic.time);
        SceneManager.LoadScene("StartMenu");
    }

    private void AddHighScoreEntry(int score, string name)
    {
        // Create HighscoreEntry
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("HighScoresTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        if (highScores == null)
        {
            // There's no stored table, initialize
            highScores = new HighScores()
            {
                highScoreEntryList = new List<HighScoreEntry>()
            };
        }

        // Add new entry to Highscores
        highScores.highScoreEntryList.Add(highScoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("HighScoresTable", json);
        PlayerPrefs.Save();
    }

    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }

    private int GetScaleFactor()
    {
        int ret;
        if (((decimal)Screen.height / (decimal)Screen.width) > ((decimal)180 / (decimal)320))
        {
            ret = Screen.width / 320;
        }
        else
        {
            ret = Screen.height / 180;
        }
        return ret;
    }
}
