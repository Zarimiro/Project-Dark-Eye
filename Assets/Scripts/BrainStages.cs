using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class BrainStages : MonoBehaviour {
    private Image BrainStageIcon;
    public Image CanX;
    public Sprite[] BrainStage=new Sprite[5];
    float Timer;
  public  string CurrentStage;
    public enum Stages {
        Stage1, 
        Stage2,
        Stage3
    }
	// Use this for initialization
	void Start () {
        BrainStageIcon = GetComponent<Image>();
     //   CanX = gameObject.GetComponentInParent<Image>();
            Timer = 0f;
        CurrentStage = Stages.Stage1.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime;
        if (Timer >= 3f && CurrentStage== Stages.Stage1.ToString()) {
            CanX.color = Color.Lerp(CanX.color, new Color(1f, 0f, 0f, .7f), 4.5f*Time.deltaTime);
            BrainStageIcon.sprite = BrainStage[0];
            CurrentStage = Stages.Stage2.ToString();
            Invoke("ColorReset", 0.2f);
            Timer = 0f;
        }
        if (Timer >= 7f && CurrentStage == Stages.Stage2.ToString()) {
            BrainStageIcon.sprite = BrainStage[1];
            CurrentStage = Stages.Stage3.ToString();
           
            Timer = 0f;

        }
        if (Timer >= 12f && CurrentStage == Stages.Stage3.ToString())
        {
            BrainStageIcon.sprite = BrainStage[1];
          
            Timer = 0f;

        }
    }

    Color ColorReset() {
        return CanX.color = Color.Lerp(CanX.color, Color.clear, 1f);
    }
}
