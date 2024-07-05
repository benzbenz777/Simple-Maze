using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timerText;

    [SerializeField]
    float remainingTime;
    private int currentTime;
    private static bool timerStopped = false;
    public Goal goalScript;
    public GameObject GameoverUI;
    public int hasPlayerGoal = Goal.HasPlayerGoal;
    private static int TimeEqual = 0;
    private KeyCode mnscene = KeyCode.Q;
    private KeyCode rescene = KeyCode.R;
    private bool isticking = false;
    private bool isSounding = false;

    [SerializeField]
    private AudioClip clockticking;
    private AudioSource clocktickingAudioSource;

    [SerializeField]
    private AudioClip final1;
    private AudioSource final1AudioSource;

    [SerializeField]
    private AudioClip final2;
    private AudioSource final2AudioSource;

    [SerializeField]
    private AudioClip begin;
    private AudioSource beginAudioSource;

    private bool wreset = false;

    bool tomenu()
    {
        return Input.GetButtonDown("PS4L1");
    }

    bool toreplay()
    {
        return Input.GetButtonDown("PS4L2");
    }

    private IEnumerator PlaySoundsSequence()
    {
        clocktickingAudioSource = gameObject.AddComponent<AudioSource>();
        clocktickingAudioSource.clip = clockticking; // ชื่อตัวแปรถูกต้องแล้ว
        clocktickingAudioSource.volume = 0.1f; // เสียง 10%
        clocktickingAudioSource.Play(); // เล่นเสียง clockticking
        while (clocktickingAudioSource.isPlaying) // รอให้เสียง clockticking เล่นจบ
        {
            yield return null;
        }
        isticking = false;
    }

    private IEnumerator PlaySoundsSequenceEnd1()
    {
        final1AudioSource = gameObject.AddComponent<AudioSource>();
        final1AudioSource.clip = final1; // ชื่อตัวแปรถูกต้องแล้ว
        final1AudioSource.volume = 0.50f; // เสียง 10%
        final1AudioSource.Play(); // เล่นเสียง clockticking
        while (final1AudioSource.isPlaying) // รอให้เสียง clockticking เล่นจบ
        {
            yield return null;
        }
        isticking = false;
    }

    private IEnumerator PlaySoundsSequenceEnd2()
    {
        final2AudioSource = gameObject.AddComponent<AudioSource>();
        final2AudioSource.clip = final2; // ชื่อตัวแปรถูกต้องแล้ว
        final2AudioSource.volume = 0.50f; // เสียง 10%
        yield return new WaitForSeconds(3.1f);
        final2AudioSource.Play(); // เล่นเสียง clockticking
        while (final2AudioSource.isPlaying) // รอให้เสียง clockticking เล่นจบ
        {
            yield return null;
        }
        isticking = false;
    }

    private IEnumerator PlaySoundsBegin()
    {
        beginAudioSource = gameObject.AddComponent<AudioSource>();
        beginAudioSource.clip = begin; // ชื่อตัวแปรถูกต้องแล้ว
        beginAudioSource.volume = 0.50f; // เสียง 10%
        yield return new WaitForSeconds(0.25f);
        beginAudioSource.Play(); // เล่นเสียง clockticking
        while (beginAudioSource.isPlaying) // รอให้เสียง clockticking เล่นจบ
        {
            yield return null;
        }
        isticking = false;
    }

    void Start()
    {
        currentTime = Mathf.FloorToInt(remainingTime);
        StartCoroutine(PlaySoundsBegin());
    }

    void Update()
    {
        Debug.Log(hasPlayerGoal);
        if (hasPlayerGoal == 1)
        {
            Debug.Log("HasPlayerGoal value: " + hasPlayerGoal);
            StopTimer();
        }

        if (!timerStopped)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                if (remainingTime <= 20)
                {
                    if (isticking == false)
                    {
                        StartCoroutine(PlaySoundsSequence());
                        isticking = true;
                    }
                }
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
            }

            int minute = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minute, seconds);
            TimeEqual = Mathf.FloorToInt(remainingTime);
        }

        if (remainingTime == 0)
        {
            Gameover();
        }

        totalui totalsendtime = FindObjectOfType<totalui>();
        totalui sendtoscore = FindObjectOfType<totalui>();
        totalui sendcrtime = FindObjectOfType<totalui>();

        if (totalsendtime != null)
        {
            totalsendtime.ReceiveTimes(TimeEqual);
        }
        if (sendtoscore != null)
        {
            sendtoscore.ScoreConvert(TimeEqual);
        }
        if (sendcrtime != null)
        {
            sendcrtime.ScoreCurrent(currentTime);
        }
    }

    public static void StopTimer()
    {
        timerStopped = true;
        Debug.Log(TimeEqual);
    }

    public void resetzero(int reset)
    {
        TimeEqual = reset;
        timerStopped = false;
    }

    public void Gameover()
    {
        GameoverUI.SetActive(true);
        if (isSounding == false)
        {
            StartCoroutine(PlaySoundsSequenceEnd1());
            StartCoroutine(PlaySoundsSequenceEnd2());
            isSounding = true;
        }

        if (GameoverUI.activeSelf == true)
        {
            if (Input.GetKeyDown(mnscene) || tomenu())
            {
                GameoverUI.SetActive(false);
                SceneManager.LoadScene("MainMenu");
                isSounding = false;
                wreset = true;
                TotalScore SendOver = FindObjectOfType<TotalScore>();
                if (SendOver != null)
                {
                    SendOver.RecieveOver(wreset);
                }
                wreset = false;
            }
            else if (Input.GetKeyDown(rescene) || toreplay())
            {
                GameoverUI.SetActive(false);
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
                isSounding = false;
                wreset = true;
                TotalScore SendOver = FindObjectOfType<TotalScore>();
                if (SendOver != null)
                {
                    SendOver.RecieveOver(wreset);
                }
                wreset = false;
            }
        }
    }
}
