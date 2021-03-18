using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //These arrays must be the same size as they will be converted to a Dictionary
    //Having seperate arrays helps with Unity's visualization in Editor
    public double[] timeGoals = { 60,90,140};
    public float[] scoreValues= { 200, 150, 50};

    private Transform startLine;
    private Transform finishLine;

    private GameObject player;
    private Score playerScore;

    private bool started = false;
    private bool ended = false;

    public static bool gameIsPaused = true;

    public GameObject countText;

    public SoundAudioClip[] audioClips;
    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(timeGoals.Length != scoreValues.Length)
        {
            Debug.LogError("Invalid time and score arrays. They need to be the same size");
        }

        PauseGame(true);
        player = GameObject.FindGameObjectWithTag("Player");
        playerScore = player.GetComponent<Score>();

        startLine = gameObject.transform.Find("Start Line");
        finishLine = gameObject.transform.Find("Finish Line");

        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        Debug.Log("Starting Countdown");

        string[] numbers = { "","3", "2", "1" };

        for (int i = 0; i < numbers.Length; i++) {
            TextPopup _t = Instantiate(countText, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane)), Quaternion.identity).GetComponent<TextPopup>();
            Vector3 _temp = _t.gameObject.GetComponent<RectTransform>().position;
            _temp.z = 0;
            _t.gameObject.GetComponent<RectTransform>().position = _temp;
            _t.Setup(numbers[i]);
            yield return new WaitForSecondsRealtime(1);
         }

        TextPopup t = Instantiate(countText, Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane)), Quaternion.identity).GetComponent<TextPopup>();
        Vector3 temp = t.gameObject.GetComponent<RectTransform>().position;
        temp.z = 0;
        t.gameObject.GetComponent<RectTransform>().position = temp;
        t.Setup("Go");
        StartLevel();
    }

    public Dictionary<double, float> GetDict()
    {
        Dictionary<double, float> dict = new Dictionary<double, float>();

        for(int i = 0; i < timeGoals.Length; i++)
        {
            dict.Add(timeGoals[i], scoreValues[i]);
        }

        return dict;
    }


    public void StartLevel()
    {
        player.transform.position = startLine.position;
        PauseGame(false);
        playerScore.StartRun();
        started = true;
        ended = false;
        SoundManager.Instance.PlaySound(SoundManager.Sound.StartRun);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!gameIsPaused);
        }

        //Make it mandatory to eliminate all enemies?
        if(player.transform.position.x > finishLine.position.x && !ended)
        {
            ended = true;
            playerScore.EndRun();
            PauseGame();
            SoundManager.Instance.PlaySound(SoundManager.Sound.EndRun);

        }
    }

    public static void PauseGame(bool paused = true)
    {
        if (paused)
        {
            Time.timeScale = 0;
            gameIsPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            gameIsPaused = false;
        }
    }


    private void OnGUI()
    {

        if (gameIsPaused && started && !ended)
        {
            GUI.Box(new Rect(Screen.width / 2, (Screen.height / 2), 100, 50), "Paused");
        }

       
        if (ended)
        {
            GUI.Box(new Rect(Screen.width / 2, (Screen.height / 2), 150, 50), "Your score is: " + playerScore.GetScore());
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 - 40, 100, 20), "Restart?"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);            
            }
        }

    }

   

}
