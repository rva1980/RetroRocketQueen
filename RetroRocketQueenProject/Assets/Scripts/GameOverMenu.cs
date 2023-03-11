using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverMenu : MonoBehaviour
{
    public static bool isGameOver = false;

    public GameObject gameOverMenuUi;

    public Button retryButton;
    public Button exitButton;
    public Button doneButton;

    public float secondsWait;

    public ScoreController scoreController;

    public int distanceFactor;
    public int timeFactor;
    public int mineralsFactor;
    public int rocketFactor;

    public Text distanceScore;
    public Text timeScore;
    public Text mineralsScore;
    public Text rocketScore;
    public Text totalScore;
    
    public Text newHighScore;
    public Image charSelector;
    public Transform charSelectorTransform;
    public Sprite charSelectorSprite1;
    public Sprite charSelectorSprite2;
    public Sprite charSelectorSprite3;

    public Text exitButtonText;
    public Text doneButtonText;

    private int _distance;
    private int _time;
    private int _minerals;
    private int _rocket;
    private int _total;
    
    private string _nameString;
    private int _charName1;
    private int _charName2;
    private int _charName3;
    private int _charNameSelected;
    private float _horizontalInput;
    private bool _horizontalDown;
    private float _verticalInput;
    private bool _verticalDown;
    private float _verticalDownTime;
    private float _verticalDownDeltaTime;


    private bool _distanceStep;
    private bool _timeStep;
    private bool _mineralsStep;
    private bool _rocketStep;

    public AudioSource scoreStepSound;
    public AudioSource scoreLongStepSound;
    public AudioSource selectSound;
    public AudioSource buttonPressed;

    private float _gameOverTime;
    private float _distanceTime;
    private float _timeTime;
    private float _mineralsTime;
    private float _rocketTime;

    private bool _flagScoreChecked;
    private bool _flagScoreFinished;
    private bool _skipScore;


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Cancel"))
        //{
        //    if (isGameOver)
        //    {
        //        Exit();
                
        //    }
        //}

        
        _verticalInput = Input.GetAxisRaw("Vertical");
        
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel")) && isGameOver)
        {
            _skipScore = true;
        }




    }

    void Awake()
    {
        _flagScoreChecked = false;
        _flagScoreFinished = false;

        _nameString = "abcdefghijklmnopqrstuvwxyz1234567890";
        _charName1 = 0;
        _charName2 = 0;
        _charName3 = 0;
        _charNameSelected = 1;
        _horizontalDown = false;
        _verticalDown = false;
        _verticalDownTime = Time.time;
        _verticalDownDeltaTime = 1f;
    }

    void FixedUpdate()
    {
        if (isGameOver)
        {


            if (Time.time > _rocketTime + 1f)
            {
                if (!_flagScoreChecked)
                {
                   
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

                    
                    if (_total > highScores.highScoreEntryList[5].score)
                    {
                        newHighScore.enabled = true;
                        charSelector.enabled = true;
                        doneButton.gameObject.SetActive(true);
                        doneButton.Select();
                    }
                    else
                    {
                        retryButton.gameObject.SetActive(true);
                        exitButton.gameObject.SetActive(true);
                        exitButton.Select();
                        retryButton.Select();
                    }

                    _flagScoreChecked = true;
                                        
                }
                    
                if ((60 - (int)(Time.time - (_rocketTime + 1))) == -1)
                {
                    if (newHighScore.enabled)
                    {
                        doneButton.Select();
                        Done();
                    }
                    else
                    {
                        exitButton.Select();
                        Exit();
                    }

                }
                else
                {
                    exitButtonText.text = "   exit (" + (60 - (int)(Time.time - (_rocketTime + 1))).ToString() + ")";
                    doneButtonText.text = "   done (" + (60 - (int)(Time.time - (_rocketTime + 1))).ToString() + ")";
                }

                if (_verticalInput > 0f && !_verticalDown)
                {
                    _verticalDownTime = Time.time;
                    _verticalDown = true;
                    selectSound.Play();
                    switch (_charNameSelected)
                    {
                        case 1:
                            if (_charName1 == 0)
                            {
                                _charName1 = _nameString.Length - 1;
                            }
                            else
                            {
                                _charName1--;
                            }
                            break;

                        case 2:
                            if (_charName2 == 0)
                            {
                                _charName2 = _nameString.Length - 1;
                            }
                            else
                            {
                                _charName2--;
                            }
                            break;

                        case 3:
                            if (_charName3 == 0)
                            {
                                _charName3 = _nameString.Length - 1;
                            }
                            else
                            {
                                _charName3--;
                            }
                            break;
                    }
                    
                }
                else if (_verticalInput < 0f && !_verticalDown)
                {
                    _verticalDownTime = Time.time;
                    _verticalDown = true;
                    selectSound.Play();
                    switch (_charNameSelected)
                    {
                        case 1:
                            if (_charName1 == _nameString.Length - 1)
                            {
                                _charName1 = 0;
                            }
                            else
                            {
                                _charName1++;
                            }
                            break;

                        case 2:
                            if (_charName2 == _nameString.Length - 1)
                            {
                                _charName2 = 0;
                            }
                            else
                            {
                                _charName2++;
                            }
                            break;

                        case 3:
                            if (_charName3 == _nameString.Length - 1)
                            {
                                _charName3 = 0;
                            }
                            else
                            {
                                _charName3++;
                            }
                            break;
                    }
                }
                else if (_verticalInput == 0f)
                {
                    _verticalDown = false;
                    _verticalDownDeltaTime = 1f;
                }

                if (Time.time > _verticalDownTime + _verticalDownDeltaTime && _verticalDown)
                {
                    _verticalDown = false;
                    _verticalDownDeltaTime = 0.2f;
                }

                
                if (_horizontalInput > 0f && !_horizontalDown)
                {
                    _horizontalDown = true;
                    switch (_charNameSelected)
                    {
                        case 1:
                            _charNameSelected = 2;
                            charSelectorTransform.position = new Vector3(charSelectorTransform.position.x + 16f, charSelectorTransform.position.y, charSelectorTransform.position.z);
                            charSelector.sprite = charSelectorSprite2;
                            selectSound.Play();
                            break;

                        case 2:
                            _charNameSelected = 3;
                            charSelectorTransform.position = new Vector3(charSelectorTransform.position.x + 16f, charSelectorTransform.position.y, charSelectorTransform.position.z);
                            charSelector.sprite = charSelectorSprite3;
                            selectSound.Play();
                            break;
                    }
                    
                }
                else if (_horizontalInput < 0f && !_horizontalDown)
                {
                    _horizontalDown = true;
                    switch (_charNameSelected)
                    {
                        case 2:
                            _charNameSelected = 1;
                            charSelectorTransform.position = new Vector3(charSelectorTransform.position.x - 16f, charSelectorTransform.position.y, charSelectorTransform.position.z);
                            charSelector.sprite = charSelectorSprite1;
                            selectSound.Play();
                            break;

                        case 3:
                            _charNameSelected = 2;
                            charSelectorTransform.position = new Vector3(charSelectorTransform.position.x - 16f, charSelectorTransform.position.y, charSelectorTransform.position.z);
                            charSelector.sprite = charSelectorSprite2;
                            selectSound.Play();
                            break;
                    }
                    
                }
                else if (_horizontalInput == 0f)
                {
                    _horizontalDown = false;
                }

                Debug.Log(_verticalInput.ToString() + " - " + _verticalDown.ToString());
                
         
                newHighScore.text = "high score     " + _nameString.Substring(_charName1, 1) + " " + _nameString.Substring(_charName2, 1) + " " + _nameString.Substring(_charName3, 1);

            }
            else if (((Time.time > _mineralsTime + 1f) || (Time.time > _mineralsTime && _skipScore)) && !_flagScoreFinished)
            {
                if (_skipScore)
                {
                    if (!rocketScore.enabled)
                    {
                        rocketScore.enabled = true;
                    }

                    _rocket += scoreController.rocket;
                    scoreController.rocket = 0;
                    scoreLongStepSound.Play();

                    _rocketTime = Time.time;
                    _rocketStep = false;
                    _skipScore = false;

                    _flagScoreFinished = true;
                }
                else
                {
                    if (!rocketScore.enabled)
                    {
                        rocketScore.enabled = true;
                        scoreLongStepSound.Play();
                    }

                    if ((Time.time > _mineralsTime + 2f) & (scoreController.rocket > 0))
                    {
                        _rocket += 1;
                        scoreController.rocket -= 1;
                        scoreStepSound.Play();
                    }
                    else if ((scoreController.rocket == 0) & (_rocketStep))
                    {
                        _rocketTime = Time.time;
                        _rocketStep = false;

                        _flagScoreFinished = true;
                    }
                }
               
            }
            else if (((Time.time > _timeTime + 1f) || (Time.time > _timeTime && _skipScore)) && !_flagScoreFinished)
            {
                if (_skipScore)
                {
                    if (!mineralsScore.enabled)
                    {
                        mineralsScore.enabled = true;
                    }
                    _minerals += scoreController.minerals;
                    scoreController.minerals = 0;
                    scoreLongStepSound.Play();

                    _mineralsTime = Time.time;
                    _mineralsStep = false;

                    _skipScore = false;
                }
                else
                {
                    if (!mineralsScore.enabled)
                    {
                        mineralsScore.enabled = true;
                        scoreLongStepSound.Play();
                    }

                    if ((Time.time > _timeTime + 2f) & (scoreController.minerals > 0))
                    {
                        _minerals += 1;
                        scoreController.minerals -= 1;
                        scoreStepSound.Play();
                    }
                    else if ((scoreController.minerals == 0) & (_mineralsStep))
                    {
                        _mineralsTime = Time.time;
                        _mineralsStep = false;
                    }
                }

            }
            else if (((Time.time > _distanceTime + 1f) || (Time.time > _distanceTime && _skipScore)) && !_flagScoreFinished)
            {
                if (_skipScore)
                {
                    if (!timeScore.enabled)
                    {
                        timeScore.enabled = true;
                    }

                    _time += scoreController.time;
                    scoreController.time = 0;
                    scoreLongStepSound.Play();

                    _timeTime = Time.time;
                    _timeStep = false;

                    _skipScore = false;
                }
                else
                {
                    if (!timeScore.enabled)
                    {
                        timeScore.enabled = true;
                        scoreLongStepSound.Play();
                    }

                    if ((Time.time > _distanceTime + 2f) & (scoreController.time > 0))
                    {
                        _time += 1;
                        scoreController.time -= 1;
                        scoreStepSound.Play();
                    }
                    else if ((scoreController.time == 0) & (_timeStep))
                    {
                        _timeTime = Time.time;
                        _timeStep = false;
                    }
                }
                

            }
            else if (((Time.time > _gameOverTime + 2f) || (Time.time > _gameOverTime && _skipScore)) && !_flagScoreFinished)
            {
                if (_skipScore)
                {
                    if (!distanceScore.enabled)
                    {
                        distanceScore.enabled = true;
                    }

                    _distance += scoreController.distance;
                    scoreController.distance = 0;
                    scoreLongStepSound.Play();

                    _distanceTime = Time.time;
                    _distanceStep = false;

                    _skipScore = false;
                }
                else
                {
                    if (!distanceScore.enabled)
                    {
                        distanceScore.enabled = true;
                        scoreLongStepSound.Play();
                    }

                    if ((Time.time > _gameOverTime + 3f) & (scoreController.distance > 0))
                    {
                        _distance += 1;
                        scoreController.distance -= 1;
                        scoreStepSound.Play();
                    }
                    else if ((scoreController.distance == 0) & (_distanceStep))
                    {
                        _distanceTime = Time.time;
                        _distanceStep = false;
                    }
                }
            }


            _total = (_distance * distanceFactor) + (_time * timeFactor) + (_minerals * mineralsFactor) + (_rocket * rocketFactor);
            
            distanceScore.text = "distance   x" + FormatScore(distanceFactor, 2) + " " + FormatScore(_distance * distanceFactor, 5);
            timeScore.text = "time       x" + FormatScore(timeFactor, 2) + " " + FormatScore(_time * timeFactor, 5);
            mineralsScore.text = "minerals   x" + FormatScore(mineralsFactor, 2) + " " + FormatScore(_minerals * mineralsFactor, 5);
            rocketScore.text = "rocket     x" + FormatScore(rocketFactor, 2) + " " + FormatScore(_rocket * rocketFactor, 5);
            totalScore.text = "total          " + FormatScore(_total, 5);


        }
    }

    

    public void GameOver() 
    {
        scoreController.isGameOver = true;

        _distance = 0;
        _time = 0;
        _minerals = 0;
        _rocket = 0;

        _distanceStep = true;
        _timeStep = true;
        _mineralsStep = true;
        _rocketStep = true;

        _gameOverTime = Time.time;
        _distanceTime = Time.time + 999999f;
        _timeTime = Time.time + 999999f;
        _mineralsTime = Time.time + 999999f;
        _rocketTime = Time.time + 999999f;

        gameOverMenuUi.SetActive(true);
        //Time.timeScale = 0f;
        isGameOver = true;
        


    }

    public void Retry()
    {
        buttonPressed.Play();
        Time.timeScale = 1f;
        isGameOver = false;
        retryButton.GetComponent<Animator>().SetBool("Pressed", true);
        StartCoroutine("LoadRetry");
    }
    IEnumerator LoadRetry()
    {
        yield return new WaitForSeconds(secondsWait);
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        buttonPressed.Play();
        Time.timeScale = 1f;
        isGameOver = false;
        exitButton.GetComponent<Animator>().SetBool("Pressed", true);
        StartCoroutine("LoadExit");
    }
    IEnumerator LoadExit()
    {
        yield return new WaitForSeconds(secondsWait);
        SceneManager.LoadScene("StartMenu");
    }

    public void Done()
    {
        AddHighScoreEntry(_total, _nameString.Substring(_charName1, 1) + _nameString.Substring(_charName2, 1) + _nameString.Substring(_charName3, 1));

        buttonPressed.Play();
        Time.timeScale = 1f;
        isGameOver = false;
        doneButton.GetComponent<Animator>().SetBool("Pressed", true);
        StartCoroutine("LoadDone");
    }
    IEnumerator LoadDone()
    {
        yield return new WaitForSeconds(secondsWait);
        SceneManager.LoadScene("HighScores");
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

}
