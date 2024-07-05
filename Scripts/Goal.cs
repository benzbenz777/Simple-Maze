using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public AudioClip GoalSound;
    private AudioSource audioSource;
    [SerializeField] private AudioClip finish1Clip;
    [SerializeField] private AudioClip finish2Clip;

    private AudioSource finish1AudioSource;
    private AudioSource finish2AudioSource;

    //[SerializeField] private AudioClip finish3Clip;
    //private AudioSource finish3AudioSource;

    [SerializeField] private List<AudioClip> scoreClips;
    private AudioSource scoreSource;

    private int mtens, ttens, uunits;
    public static int SRTime;
    private int thousands, hundreds, tens, units;
    public static int SRScore; //คะแนนสุดท้าย
    public static int HasPlayerGoal;
    public GameObject TotalUI;
    //private string[] myScene = new string[10];
    private int SceneChecker;
    private KeyCode ntscene = KeyCode.Y;
    private KeyCode mnscene = KeyCode.Q;
    private KeyCode rescene = KeyCode.R;
    private int reset = 0;
    private int restep = 0;
    private bool bcheck = true;
    public static int stepupstt = 0;
    public static int stepdownstt = 0;
    public static int stepleftstt = 0;
    public static int steprightstt = 0;
    public static int whitstt = 0;
    public static int scorestt;
    public static int starstt;
    public static int timeuistt;
    public static int cknodis = 0;
    void Start()
    {
        SceneChecker = SceneManager.GetActiveScene().buildIndex + 1;
        // insertstagename();
    }

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

    public void ReceiveAllstep(int stepup, int stepdown, int stepleft, int stepright, int whits)
    {
        stepupstt = stepup;
        stepdownstt = stepdown;
        stepleftstt = stepleft;
        steprightstt = stepright;
        whitstt = whits;
    }

    public void ReceiveScore(int facscore, int totalstar)
    {
        scorestt = facscore;
        starstt = totalstar;
    }

    public void ReceiveTimeui(int timerui)
    {
        timeuistt = timerui;
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

        // finish3AudioSource.Play(); // เล่นเสียง finish3Clip
        // while (finish3AudioSource.isPlaying) // รอให้เสียง finish1Clip เล่นจบ
        // {
        //     yield return null;
        // }
    }

    private void FinGame()
    {
        TotalScore SendTime = FindObjectOfType<TotalScore>();
        if (SendTime != null)
        {
            SendTime.RecieveTime(timeuistt);
        }

        SRTime = timeuistt; // กำหนดค่า score ให้กับ static variable
        Debug.Log("sss = " + SRTime);
        SRScore = scorestt; // กำหนดค่า score ให้กับ static variable
        Debug.Log("sss = " + SRScore);
        //เช็คเวลหลังเล่นจบ
        mtens = SRTime / 60;
        ttens = (SRTime % 60) / 10;
        uunits = (SRTime % 60) % 10;
        //เช็คคะแนนหลังเล่นจบ
        thousands = SRScore / 1000;
        hundreds = (SRScore % 1000) / 100;
        tens = (SRScore % 100) / 10;
        units = SRScore % 10;
        /////////////////////////////////////////////
        bcheck = false;
        StartCoroutine(WaitAndPlayRandomSoundTime());
        TotalUI.SetActive(true);

        GridMovement Checkbtn = FindObjectOfType<GridMovement>();
        if (Checkbtn != null)
        {
            Checkbtn.Checkbtn(bcheck);
        }
    }
    IEnumerator WaitAndPlayRandomSoundTime()
    {

        yield return new WaitForSeconds(2.0f);
        PlaySound(20);
        yield return new WaitForSeconds(2.0f);
        if (SRTime == 0)
        {
            yield return new WaitForSeconds(1.50f);
            PlaySound(0);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(PlaySoundsByDigitsTime(mtens, ttens, uunits));
            yield return new WaitForSeconds(3.25f);
            StartCoroutine(WaitAndPlayRandomSound());
        }
    }
    IEnumerator WaitAndPlayRandomSound()
    {

        yield return new WaitForSeconds(2.0f);
        PlaySound(13);
        yield return new WaitForSeconds(0.75f);
        if (SRScore == 0)
        {
            yield return new WaitForSeconds(1.50f);
            PlaySound(0);
        }
        else
        {
            StartCoroutine(PlaySoundsByDigits(thousands, hundreds, tens, units));
            yield return new WaitForSeconds(3.5f);
            StartCoroutine(PlaySoundStar(starstt));
            yield return new WaitForSeconds(6.75f);
            StartCoroutine(PlaySoundsSequence());
        }
    }
    public void PlaySound(int soundIndex)
    {
        Debug.Log("AudioClip is null at index " + soundIndex);
        if (soundIndex >= 0 && soundIndex < scoreClips.Count)
        {
            if (scoreClips[soundIndex] != null)
            {
                scoreSource.PlayOneShot(scoreClips[soundIndex]);
            }
            else
            {
                Debug.LogError("AudioClip is null at index " + soundIndex);
            }
        }
        else
        {
            Debug.LogError("Invalid sound index: " + soundIndex);
        }
    }
    IEnumerator PlaySoundsByDigits(int thousands, int hundreds, int tens, int units)
    {
        // เล่นเสียงตามหลักพัน
        if (thousands > 0)
        {
            yield return new WaitForSeconds(1.50f);
            PlaySound(thousands);
            yield return new WaitForSeconds(0.75f);
            PlaySound(10);
        }

        // เล่นเสียงตามหลักร้อย
        if (hundreds > 0)
        {
            yield return new WaitForSeconds(1.50f);
            PlaySound(hundreds);
            yield return new WaitForSeconds(0.75f);
            PlaySound(11);
        }

        // เล่นเสียงตามหลักสิบ
        if (tens > 0)
        {
            if (tens == 2)
            {
                yield return new WaitForSeconds(1.50f);
                PlaySound(14);
                yield return new WaitForSeconds(0.75f);
                PlaySound(12);
            }
            else if (tens == 1)
            {
                yield return new WaitForSeconds(0.75f);
                PlaySound(12);
            }
            else
            {
                yield return new WaitForSeconds(1.50f);
                PlaySound(tens);
                yield return new WaitForSeconds(0.75f);
                PlaySound(12);
            }
        }

        // เล่นเสียงตามหลักหน่วย
        if (units > 0)
        {
            yield return new WaitForSeconds(0.75f);
            PlaySound(units);
        }
    }

    IEnumerator PlaySoundsByDigitsTime(int mtens, int ttens, int uunits)
    {
        if (mtens > 0)
        {
            yield return new WaitForSeconds(1.25f);
            PlaySound(mtens);
            yield return new WaitForSeconds(0.75f);
            PlaySound(18);
            // }
        }
        // เล่นเสียงตามหลักสิบ
        if (ttens > 0)
        {
            if (ttens == 2)
            {
                yield return new WaitForSeconds(1.25f);
                PlaySound(14);
                yield return new WaitForSeconds(0.75f);
                PlaySound(12);
            }
            else if (ttens == 1)
            {
                yield return new WaitForSeconds(0.75f);
                PlaySound(12);
            }
            else
            {
                yield return new WaitForSeconds(1.25f);
                PlaySound(ttens);
                yield return new WaitForSeconds(0.75f);
                PlaySound(12);
            }
        }

        // เล่นเสียงตามหลักหน่วย
        if (uunits > 0)
        {
            yield return new WaitForSeconds(0.75f);
            PlaySound(uunits);
        }
        yield return new WaitForSeconds(0.75f);
        PlaySound(19);


    }

    IEnumerator PlaySoundStar(int starstt)
    {
        yield return new WaitForSeconds(2.0f);
        PlaySound(15);
        yield return new WaitForSeconds(2.0f);
        if (starstt == 1)
        {
            PlaySound(starstt);
            yield return new WaitForSeconds(1.0f);
        }
        else if (starstt == 2)
        {
            PlaySound(starstt);
            yield return new WaitForSeconds(1.0f);
        }
        else if (starstt == 3)
        {
            PlaySound(starstt);
            yield return new WaitForSeconds(1.0f);
        }
        PlaySound(17);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = GoalSound;
        audioSource.volume = 0.6f; // ตั้งค่าระดับเสียง

        finish1AudioSource = gameObject.AddComponent<AudioSource>();
        finish2AudioSource = gameObject.AddComponent<AudioSource>();

        scoreSource = gameObject.AddComponent<AudioSource>();

        //finish3AudioSource = gameObject.AddComponent<AudioSource>();

        finish1AudioSource.clip = finish1Clip;
        finish2AudioSource.clip = finish2Clip;
        //finish3AudioSource.clip = finish3Clip;

        // ตั้งค่าระดับเสียงตามต้องการ
        finish1AudioSource.volume = 0.6f; // เสียง 60%
        finish2AudioSource.volume = 0.6f; // เสียง 60%
        scoreSource.volume = 0.6f;
        //finish3AudioSource.volume = 0.6f;


        if (other.CompareTag("Player"))
        {
            HasPlayerGoal = 1;
            Debug.Log("Goal!");
            Debug.Log("HasPlayerGoal :" + HasPlayerGoal);
            audioSource.Play();
            Timer.StopTimer();
            FinGame();
            cknodis = 1;
            //StartCoroutine(PlaySoundsSequence());
            DistanceButtonSound ReceiveCK = FindObjectOfType<DistanceButtonSound>();
            if (ReceiveCK != null)
            {
                ReceiveCK.ReceiveCKSuv(cknodis);
            }

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(ntscene) && HasPlayerGoal == 1 || CheckNext() && HasPlayerGoal == 1)
        {
            HasPlayerGoal = 0;
            Debug.Log("HasPlayerGoal: " + HasPlayerGoal);
            Debug.Log("ntscene pressed");

            // ให้ SceneChecker เป็นตัวชี้ที่ใช้ในการอ้างอิงถึงฉากในอาร์เรย์ myScene
            SceneManager.LoadScene(SceneChecker);
            Debug.Log("SceneChecker: " + SceneChecker);

            TotalScore RecieveScore = FindObjectOfType<TotalScore>();
            if (RecieveScore != null)
            {
                RecieveScore.RecieveScore(scorestt, starstt);
            }
            // รีเซ็ต Step
            GridMovement resetstep = FindObjectOfType<GridMovement>();
            if (resetstep != null)
            {
                resetstep.resetstep(restep);
            }
            // รีเซ็ตเวลาเมื่อโหลดซีนใหม่
            Timer resetzero = FindObjectOfType<Timer>();
            if (resetzero != null)
            {
                resetzero.resetzero(reset);
            }
        }
        else if (Input.GetKeyDown(mnscene) && HasPlayerGoal == 1 || CheckMenu() && HasPlayerGoal == 1)
        {
            HasPlayerGoal = 0;
            Debug.Log("HasPlayerGoal: " + HasPlayerGoal);

            // ให้ SceneChecker เป็นตัวชี้ที่ใช้ในการอ้างอิงถึงฉากในอาร์เรย์ myScene
            SceneManager.LoadScene("MainMenu");

            // รีเซ็ต Step
            GridMovement resetstep = FindObjectOfType<GridMovement>();
            if (resetstep != null)
            {
                resetstep.resetstep(restep);
            }
            // รีเซ็ตเวลาเมื่อโหลดซีนใหม่
            Timer resetzero = FindObjectOfType<Timer>();
            if (resetzero != null)
            {
                resetzero.resetzero(reset);
            }
        }
        else if (Input.GetKeyDown(rescene) && HasPlayerGoal == 1 || CheckRep() && HasPlayerGoal == 1)
        {
            HasPlayerGoal = 0;
            Debug.Log("HasPlayerGoal: " + HasPlayerGoal);

            // ให้ SceneChecker เป็นตัวชี้ที่ใช้ในการอ้างอิงถึงฉากในอาร์เรย์ myScene
            SceneManager.LoadScene(SceneChecker - 1);

            // รีเซ็ต Step
            GridMovement resetstep = FindObjectOfType<GridMovement>();
            if (resetstep != null)
            {
                resetstep.resetstep(restep);
            }
            // รีเซ็ตเวลาเมื่อโหลดซีนใหม่
            Timer resetzero = FindObjectOfType<Timer>();
            if (resetzero != null)
            {
                resetzero.resetzero(reset);
            }
        }
    }
}
