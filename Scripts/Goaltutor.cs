using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goaltutor : MonoBehaviour
{
    public AudioClip GoalSound;
    private AudioSource goalSource;

    [SerializeField] private AudioClip finish1Clip;
    [SerializeField] private AudioClip finish2Clip;
    [SerializeField] private AudioClip finish3Clip;
    private AudioSource finish1AudioSource;
    private AudioSource finish2AudioSource;
    private AudioSource finish3AudioSource;


    public static int HasPlayerGoal;
    public GameObject TotalUI;
    private KeyCode ntscene = KeyCode.Y;
    private KeyCode mnscene = KeyCode.Q;
    private KeyCode rescene = KeyCode.R;
    public static int cknodis;

    bool CheckNext()
    {
        return Input.GetButtonDown("PS4R1");
    }
    bool CheckMenu()
    {
        return Input.GetButtonDown("PS4L1");
    }
     bool CheckRep()
    {
        return Input.GetButtonDown("PS4L2");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        goalSource = gameObject.AddComponent<AudioSource>();
        goalSource.clip = GoalSound;
        goalSource.volume = 0.5f; // ตั้งค่าระดับเสียง

        // สร้าง AudioSource สำหรับ footstep และ WallHits
        finish1AudioSource = gameObject.AddComponent<AudioSource>();
        finish2AudioSource = gameObject.AddComponent<AudioSource>();
        finish3AudioSource = gameObject.AddComponent<AudioSource>();


        // โหลดเสียง footstep และ WallHits ลงใน AudioSource
        finish1AudioSource.clip = finish1Clip;
        finish2AudioSource.clip = finish2Clip;
        finish3AudioSource.clip = finish3Clip;

        // ตั้งค่าระดับเสียงตามต้องการ
        finish1AudioSource.volume = 0.6f; // เสียง 60%
        finish2AudioSource.volume = 0.6f; // เสียง 60%
        finish3AudioSource.volume = 0.6f;

        if (other.CompareTag("Player"))
        {
            HasPlayerGoal = 1;
            cknodis = 1;
            Debug.Log("Goal!");
            Debug.Log("HasPlayerGoal :" + HasPlayerGoal);
            goalSource.Play();
            Timer.StopTimer();
            TotalUI.SetActive(true);

            //==================================================//
            Dissoundtutor ReceiveCK = FindObjectOfType<Dissoundtutor>();
            if (ReceiveCK != null)
            {
                ReceiveCK.ReceiveCKSuv(cknodis);
            }
            StartCoroutine(PlaySoundsSequence());
            Time.timeScale = 0f;
        }
    }

    private IEnumerator PlaySoundsSequence()
    {
        finish1AudioSource.Play(); // เล่นเสียง finish1Clip

        while (finish1AudioSource.isPlaying) // รอให้เสียง finish1Clip เล่นจบ
        {
            yield return null;
        }

        finish2AudioSource.Play(); // เล่นเสียง finish2Clip
        while (finish2AudioSource.isPlaying) // รอให้เสียง finish1Clip เล่นจบ
        {
            yield return null;
        }
        finish3AudioSource.Play(); // เล่นเสียง finish3Clip
        while (finish3AudioSource.isPlaying) // รอให้เสียง finish1Clip เล่นจบ
        {
            yield return null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(ntscene) && HasPlayerGoal == 1 || CheckNext() && HasPlayerGoal == 1)
        {
            HasPlayerGoal = 0;
            Debug.Log("HasPlayerGoal: " + HasPlayerGoal);
            SceneManager.LoadScene("Stage1");
            Time.timeScale = 1f;
        }
        else if (Input.GetKeyDown(mnscene) && HasPlayerGoal == 1 || CheckMenu() && HasPlayerGoal == 1)
        {
            HasPlayerGoal = 0;
            Debug.Log("HasPlayerGoal: " + HasPlayerGoal);
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1f;
        }
        else if (Input.GetKeyDown(rescene) && HasPlayerGoal == 1 || CheckRep() && HasPlayerGoal == 1)
        {
            HasPlayerGoal = 0;
            Debug.Log("HasPlayerGoal: " + HasPlayerGoal);
            SceneManager.LoadScene("Tutor");
            Time.timeScale = 1f;
        }

    }
}
