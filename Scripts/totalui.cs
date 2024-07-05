using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class totalui : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimerTextUi;
    [SerializeField] private TextMeshProUGUI WHitsTextUi;
    [SerializeField] private TextMeshProUGUI TotalScoreUi;
    [SerializeField] private TextMeshProUGUI TotalStarUi;
    [SerializeField] float Timefix;
    private float TimeCur;
    private int timerui;
    private int whitsui;
    private int ttalscs;
    private int minute;
    private int seconds;
    private int facscore, star = 0,startotal,SRScore;
    private int thousands, hundreds, tens, units;

    public void ReceiveHits(int whits)
    {
        whitsui = whits;
    }

    public void ScoreCurrent(int currentTime)
    {
        TimeCur = currentTime;
    }

    public void ReceiveTimes(int timer)
    {
        timerui = Mathf.FloorToInt(TimeCur) - timer;
        Timefix = timerui;
        minute = Mathf.FloorToInt(Timefix / 60);
        seconds = Mathf.FloorToInt(Timefix % 60);
    }

    public void ScoreConvert(int timertoscore)
    {
        float toscore = timertoscore;
        facscore = Mathf.FloorToInt(toscore);
    }
    void Update()
    {

        TimerTextUi.text = string.Format("{0:00}:{1:00}", minute, seconds);
        WHitsTextUi.text = whitsui.ToString();
        TotalScoreUi.text = facscore.ToString();
        if (timerui <= 30)
        {
            star = 3;
            TotalStarUi.text = star.ToString();
        }
        else if (timerui <= 60)
        {
            star = 2;
            TotalStarUi.text = star.ToString();
        }else if (timerui > 60)
        {
            star = 1;
            TotalStarUi.text = star.ToString(); 
        }


        Goal SendScore = FindObjectOfType<Goal>();
        if (SendScore != null)
        {
            SendScore.ReceiveScore(facscore,star);
        }

        Goal SendTimeui = FindObjectOfType<Goal>();
        if (SendTimeui != null)
        {
            SendTimeui.ReceiveTimeui(timerui);
        }
    }
}
