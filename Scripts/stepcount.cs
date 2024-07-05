using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class stepcount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stupText;
    [SerializeField] private TextMeshProUGUI stdownText;
    [SerializeField] private TextMeshProUGUI stleftText;
    [SerializeField] private TextMeshProUGUI strightText;
    [SerializeField] private TextMeshProUGUI strhitText;
    private int stepupcs;
    private int stepdowncs;
    private int stepleftcs;
    private int steprightcs;
    private int whitcs;

    public void ReceiveStepCounts(int stepup, int stepdown, int stepleft, int stepright,int whits) {
        stepupcs = stepup;
        stepdowncs = stepdown;
        stepleftcs = stepleft;
        steprightcs = stepright;
        whitcs = whits;
    }

    void Update(){
        stupText.text = stepupcs.ToString();   
        stdownText.text = stepdowncs.ToString(); 
        stleftText.text = stepleftcs.ToString();  
        strightText.text = steprightcs.ToString();  
        strhitText.text = whitcs.ToString();  
    }
}
