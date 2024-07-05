using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotalScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI FinalScoreTextUi;

    [SerializeField]
    private TextMeshProUGUI FinalStepTextUi;

    [SerializeField]
    private TextMeshProUGUI FinalTimeTextUi;

    [SerializeField]
    private TextMeshProUGUI FinalStarTextUi;
    public static int stepuptls;
    public static int stepdowntls;
    public static int steplefttls;
    public static int steprighttls;
    public static int whittls;
    public static int scoreplc;
    public static int starplc,
        sttens,
        stunits;
    public static int TScore;
    public static int TStar;
    public static int TSTcore;
    public static int Timesa;
    public static int TTimes;
    private KeyCode mnscene = KeyCode.Q;

    [SerializeField]
    private List<AudioClip> scoreClips;
    private AudioSource scoreSource;
    private int mmtens,
        mtens,
        ttens,
        uunits;
    public static int SRTime;
    private int thousands,
        hundreds,
        tens,
        units;
    public static int SRScore; //คะแนนสุดท้าย
    int minute;
    int seconds;
    private bool isSounding = false;
    private bool jumpsound = false;
    string currentSceneName;
    string currentreal = "finalscore";

    private bool resetover = false;

    public void RecieveSteps(int stepup, int stepdown, int stepleft, int stepright, int whits)
    {
        stepuptls = stepup;
        stepdowntls = stepdown;
        steplefttls = stepleft;
        steprighttls = stepright;
        whittls = whits;
        TSTcore = stepup + stepdown + stepleft + stepright;
    }

    public void RecieveScore(int facscore, int starstt)
    {
        scoreplc += facscore;
        starplc += starstt;
    }

    public void RecieveTime(int timeuistt)
    {
        TTimes += timeuistt;
    }

    public void totals()
    {
        TScore = scoreplc;
        Timesa = TTimes;
        TStar = starplc;
        minute = Mathf.FloorToInt(Timesa / 60);
        seconds = Mathf.FloorToInt(Timesa % 60);
    }

    public void RecieveOver(bool wreset){
        resetover = wreset;
    }

    bool CheckToMM()
    {
        return Input.GetButtonDown("PS4L1");
    }

    private void FinGame()
    {
        SRTime = TTimes; // กำหนดค่า score ให้กับ static variable
        Debug.Log("sss = " + SRTime);
        SRScore = scoreplc; // กำหนดค่า score ให้กับ static variable
        Debug.Log("sss = " + SRScore);
        //เช็คเวลหลังเล่นจบ
        mmtens = (SRTime / 60) / 10;
        mtens = SRTime / 60;
        ttens = (SRTime % 60) / 10;
        uunits = (SRTime % 60) % 10;
        //เช็คคะแนนหลังเล่นจบ
        thousands = SRScore / 1000;
        hundreds = (SRScore % 1000) / 100;
        tens = (SRScore % 100) / 10;
        units = SRScore % 10;
        //เช็คดาว
        sttens = (starplc % 100) / 10;
        stunits = starplc % 10;
        /////////////////////////////////////////////
        StartCoroutine(WaitAndPlayRandomSoundTime());
    }

    IEnumerator WaitAndPlayRandomSoundTime()
    {
        yield return new WaitForSeconds(0.75f);
        PlaySound(20);
        yield return new WaitForSeconds(3.25f);
        PlaySound(21);
        yield return new WaitForSeconds(3.25f);
        if (SRTime == 0)
        {
            yield return new WaitForSeconds(0.5f);
            PlaySound(0);
        }
        else
        {
            StartCoroutine(PlaySoundsByDigitsTime(mmtens, mtens, ttens, uunits));
            yield return new WaitForSeconds(4.25f);
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
            yield return new WaitForSeconds(1f);
            PlaySound(0);
        }
        else
        {
            StartCoroutine(PlaySoundsByDigits(thousands, hundreds, tens, units));
            yield return new WaitForSeconds(3f);
            StartCoroutine(PlaySoundStar(sttens, stunits));
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
            yield return new WaitForSeconds(1f);
            PlaySound(thousands);
            yield return new WaitForSeconds(0.5f);
            PlaySound(10);
        }

        // เล่นเสียงตามหลักร้อย
        if (hundreds > 0)
        {
            yield return new WaitForSeconds(1f);
            PlaySound(hundreds);
            yield return new WaitForSeconds(0.5f);
            PlaySound(11);
        }

        // เล่นเสียงตามหลักสิบ
        if (tens > 0)
        {
            if (tens == 2)
            {
                yield return new WaitForSeconds(1f);
                PlaySound(14);
                yield return new WaitForSeconds(0.5f);
                PlaySound(12);
            }
            else if (tens == 1)
            {
                yield return new WaitForSeconds(0.5f);
                PlaySound(12);
            }
            else
            {
                yield return new WaitForSeconds(1f);
                PlaySound(tens);
                yield return new WaitForSeconds(0.5f);
                PlaySound(12);
            }
        }

        // เล่นเสียงตามหลักหน่วย
        if (units > 0)
        {
            yield return new WaitForSeconds(0.5f);
            PlaySound(units);
        }
    }

    IEnumerator PlaySoundsByDigitsTime(int mmtens, int mtens, int ttens, int uunits)
    {
        if (mmtens > 0)
        {
            if (mmtens == 2)
            {
                yield return new WaitForSeconds(1f);
                PlaySound(14);
                yield return new WaitForSeconds(0.5f);
                PlaySound(12);
            }
            else if (mmtens == 1)
            {
                yield return new WaitForSeconds(0.5f);
                PlaySound(12);
            }
            else
            {
                yield return new WaitForSeconds(1f);
                PlaySound(mmtens);
                yield return new WaitForSeconds(0.5f);
                PlaySound(12);
            }
        }

        if (mtens > 0)
        {
            yield return new WaitForSeconds(1f);
            PlaySound(mtens);
        }
        yield return new WaitForSeconds(0.5f);
        PlaySound(18);

        // เล่นเสียงตามหลักสิบ
        if (ttens > 0)
        {
            if (ttens == 2)
            {
                yield return new WaitForSeconds(1f);
                PlaySound(14);
                yield return new WaitForSeconds(0.5f);
                PlaySound(12);
            }
            else if (ttens == 1)
            {
                yield return new WaitForSeconds(0.5f);
                PlaySound(12);
            }
            else
            {
                yield return new WaitForSeconds(1f);
                PlaySound(ttens);
                yield return new WaitForSeconds(0.5f);
                PlaySound(12);
            }
        }
        // เล่นเสียงตามหลักหน่วย
        if (uunits > 0)
        {
            yield return new WaitForSeconds(1.0f);
            PlaySound(uunits);
        }
        yield return new WaitForSeconds(1.0f);
        PlaySound(19);
    }

    IEnumerator PlaySoundStar(int sttens, int stunits)
    {
        // เล่นเสียงตามหลักสิบ
        yield return new WaitForSeconds(2.5f);
        PlaySound(17);
        yield return new WaitForSeconds(2.5f);
        if (sttens > 0)
        {
            if (sttens == 2)
            {
                yield return new WaitForSeconds(1f);
                PlaySound(14);
                yield return new WaitForSeconds(1.0f);
                PlaySound(12);
            }
            else if (sttens == 1)
            {
                yield return new WaitForSeconds(1.0f);
                PlaySound(12);
            }
            else
            {
                yield return new WaitForSeconds(1f);
                PlaySound(sttens);
                yield return new WaitForSeconds(1.0f);
                PlaySound(12);
            }
        }

        // เล่นเสียงตามหลักหน่วย
        if (stunits > 0)
        {
            yield return new WaitForSeconds(0.5f);
            PlaySound(stunits);
        }
        yield return new WaitForSeconds(1.0f);
        PlaySound(22);
        jumpsound = true;
    }

    private void Start()
    {
        // กำหนดค่า AudioSource ให้กับ scoreSource
        scoreSource = gameObject.AddComponent<AudioSource>();
        scoreSource.volume = 0.6f;
        currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == currentreal)
        {
            FinGame();
        }
        else { }
    }

    IEnumerator PlaySound23()
    {
        yield return new WaitForSeconds(1f);
        PlaySound(23);
    }

    private void Update()
    {
        totals();
        FinalScoreTextUi.text = TScore.ToString();
        FinalStepTextUi.text = TSTcore.ToString();
        FinalTimeTextUi.text = string.Format("{0:00}:{1:00}", minute, seconds);
        FinalStarTextUi.text = TStar.ToString();

        if (currentSceneName == currentreal)
        {
            if (jumpsound == true)
            {
                if (!isSounding)
                {
                    StartCoroutine(PlaySound23());
                    isSounding = true;
                }
            }

            if (CheckToMM() || Input.GetKeyDown(mnscene))
            {
                scoreplc = 0;
                starplc = 0;
                TTimes = 0;
                jumpsound = false;
                SceneManager.LoadScene("MainMenu");
            }
        }
        else { }

        if (resetover = true)
        {
            scoreplc = 0;
            starplc = 0;
            TTimes = 0;
            jumpsound = false;
            resetover = false;
        }
    }
}
