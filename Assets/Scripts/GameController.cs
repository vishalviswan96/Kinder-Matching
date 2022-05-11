using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameController : MonoBehaviour
{
    public List<Button> btns = new List<Button>();
    [SerializeField]
    private Sprite backgroundImage;

    public float startTimer;
    public Text timerText;
    public float time;
    private int finalTime;
    private bool isCounting;
    public Text countdownText;

    public List<Image> img = new List<Image>();
    //public int imageCount = 0;

    public List<Sprite> gamePuzzles = new List<Sprite>();
    public Sprite[] puzzle;

    public bool iswaiting;
    private bool firstGuess, secondGuess;
    private int CountGuesses;
    public int countCorrectGuesses;
    public int gameGuess;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessPuzzle, secondGuessPuzzle;

    public GameObject timerImage;
    public GameObject endingPanel;
    //public GameObject thankyouPanel;
    public GameObject barrierPanel;

   
    public Text scoreText;
    public int score;
    int Imgindex;
    public GameObject winPanel;
    public GameObject loosePanel;

    /*readonly string savedata = "https://www.wearnpaymemorygame.com/wearnpay/sqlconnect/updateTime.php";*/

    readonly string savedata = "http://kindercreamy.dashboardhome.in/kindercreamy/kinder/sqlconnect/infosave.php";

    public List<Animator> anim = new List<Animator>();

    private void Awake()
    {
        puzzle = Resources.LoadAll<Sprite>("Wearables");
    }
    private void Start()
    {
        score = 0;
        //startTimer = 90;
        time = 90;
        GetButton();
        AddListners();
        AddGamePuzzle();
        Shuffle(gamePuzzles);
        SetAnimator();


        /*StartCoroutine("Countdown");
        StartCoroutine("TrialShowImage");*/
        gameGuess = gamePuzzles.Count / 2;
    }

    private void Update()
    {
        if (isCounting)
        {

            time -= Time.deltaTime;
            string minutes = ((int)time / 60).ToString();
            string seconds = (time % 60).ToString("F0");
            timerText.text = minutes + ":" + seconds;
            if(time < 0)
            {
                isCounting = false;
                loosePanel.SetActive(true);
            }
        }
        
    }

    public void InstuctioButton()
    {
        StartCoroutine("Countdown");
        StartCoroutine("TrialShowImage");
    }

    public void GetButton()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for(int i = 0; i < obj.Length; i++)
        {
            btns.Add(obj[i].GetComponent<Button>());
            btns[i].image.sprite = backgroundImage;
        }
    }

    void AddGamePuzzle()
    {
        int looper = btns.Count;
        int index = 0;

        for(int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzle[index]);
            index++;
        }
    }

    IEnumerator TrialShowImage()
    {
        for (int i = 0; i < btns.Count; i++)
        {
            //anim[i].Play("Flip");   
            //yield return new WaitForSeconds(0.5f);
            btns[i].image.sprite = gamePuzzles[i];
            btns[i].interactable = false;
        }

        yield return new WaitForSeconds(21f);

        for (int i = 0; i < btns.Count; i++)
        {
            //anim[i].Play("Flip");
            btns[i].image.sprite = backgroundImage;
            btns[i].interactable = true;
        }
    }

    void AddListners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => CallPickPuzzle());
        }
    }

    void CallPickPuzzle()
    {
        StartCoroutine("PickAPuzzle");
    }

    IEnumerator PickAPuzzle()
    {
        //string name = EventSystem.current.currentSelectedGameObject.name;
        if (!iswaiting)
        {
            if (!firstGuess)
            {
                AudioManager.instance.Flip();

                iswaiting = true;
                firstGuess = true;
                firstGuessIndex = int.Parse(EventSystem.current.currentSelectedGameObject.name);
                firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
                anim[firstGuessIndex].Play("Flip");

                yield return new WaitForSeconds(0.5f);
                iswaiting = false;
                btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
                btns[firstGuessIndex].interactable = false;
            }
            else if (!secondGuess)
            {
                AudioManager.instance.Flip();

                secondGuess = true;
                secondGuessIndex = int.Parse(EventSystem.current.currentSelectedGameObject.name);
                secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
                anim[secondGuessIndex].Play("Flip");

                yield return new WaitForSeconds(0.5f);

                btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
                btns[secondGuessIndex].interactable = false;

                CountGuesses++;
                StartCoroutine("CheckThePuzzleMatch");
            }
        }
        

    }

    IEnumerator CheckThePuzzleMatch()
    {
        

        yield return new WaitForSeconds(0.5f);

        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            score += 50;
            scoreText.text = "Score : " + score.ToString();
            if (firstGuessPuzzle == "1")
            {
                SetImage();
            }
            else if (firstGuessPuzzle == "2")
            {
                SetImage();
            }
            else if (firstGuessPuzzle == "3")
            {
                SetImage();
            }
            else if (firstGuessPuzzle == "4")
            {
                SetImage();
            }
            else if (firstGuessPuzzle == "5")
            {
                SetImage();
            }
            else if (firstGuessPuzzle == "6")
            {
                SetImage();
            }
            AudioManager.instance.Correct();

            yield return new WaitForSeconds(0.1f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;
            
            //btns[firstGuessIndex].image.color = new Color(0f, 0f, 0f, 0f);
            //btns[secondGuessIndex].image.color = new Color(0f, 0f, 0f, 0f);

            //img[imageCount].enabled = true;
            barrierPanel.SetActive(true);
            yield return new WaitForSeconds(4f);
            barrierPanel.SetActive(false);
            img[Imgindex - 1].enabled = false;
            //imageCount++;
            GameFinishCall();
        }
        else
        {
            AudioManager.instance.Error();

            yield return new WaitForSeconds(0.5f);

            anim[firstGuessIndex].Play("Fliprev");
            anim[secondGuessIndex].Play("Fliprev");
            yield return new WaitForSeconds(0.5f);

            btns[firstGuessIndex].interactable = true;
            btns[secondGuessIndex].interactable = true;
            btns[firstGuessIndex].image.sprite = backgroundImage;
            btns[secondGuessIndex].image.sprite = backgroundImage;
        }

        // return new WaitForSeconds(0.5f);
        firstGuess = secondGuess = false;

    }

    private void SetImage()
    {
        //Image sr = btns[firstGuessIndex].GetComponent<Image>();
        Imgindex = int.Parse(firstGuessPuzzle);
        Debug.Log(Imgindex);
        img[Imgindex - 1].enabled = true;
    }

    void GameFinishCall()
    {
        //img[imageCount].enabled = false;
        //imageCount++;
        CheckIfTheGameIsFinished();
    }

    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;
        if(countCorrectGuesses == gameGuess)
        {
            Debug.Log("It took " + CountGuesses + " to Finish");
            isCounting = false;
            GameFinished();
            
            
        }
    }

    void GameFinished()
    {
        if(time < 90)
        {
            finalTime = 90 - (int)time;
            //AudioManager.instance.WinFx();
            StartCoroutine("SaveData");
            winPanel.SetActive(true);
        }
        /*else
        {
            loosePanel.SetActive(true);
        }*/
        
        timerImage.SetActive(false);
        //endingPanel.SetActive(true);
    }

    /*public void NextButtonPress()
    {
        endingPanel.SetActive(false);
        thankyouPanel.SetActive(true);
    }*/

    void Shuffle(List<Sprite> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    IEnumerator SaveData()
    {
        Debug.Log(MainMenu.namestring);
        Debug.Log(MainMenu.emailstr);
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("username", MainMenu.namestring));
        wwwForm.Add(new MultipartFormDataSection("password", MainMenu.emailstr));
        wwwForm.Add(new MultipartFormDataSection("city", MainMenu.citystr));
        wwwForm.Add(new MultipartFormDataSection("score", finalTime.ToString()));

        UnityWebRequest www = UnityWebRequest.Post(savedata, wwwForm);
        Debug.Log(time);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);

        }
        else
        {
            Debug.Log(www.downloadHandler.text);

            string str = www.downloadHandler.text;
            //string mytext = dataentry.text.ToString();

            if (str == "0")
            {
                Debug.Log("Time Saved");
            }
            else
            {

                Debug.Log("Save Failed ");
            }

        }
    }

    void SetAnimator()
    {
        for(int i = 0; i < btns.Count; i++)
        {
            anim.Add(btns[i].GetComponent<Animator>());
        }
    }

    /*public void CallLeadersBoard()
    {
        Application.OpenURL("https://www.wearnpaymemorygame.com/leaderboard/");
        //StartCoroutine("LeadersBoard");
    }*/

    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator Countdown()
    {
        for(int i = 20; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);
            countdownText.text = "Time to memorize: "+ i.ToString();
            if(i == 0)
            {
                countdownText.text = "GO";
                yield return new WaitForSeconds(1f);
                countdownText.enabled = false;
                startTimer = Time.time;
                isCounting = true;

            }
        }
    }

    /*192.167.10.133*/
}
