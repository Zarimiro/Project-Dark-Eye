using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

class GameStatsControl : MonoBehaviour {
    #region FireSlider variables
    public Slider FireSlider;
    public GameObject FireInHand;
    float Timer=0;
   public static bool IsFired=true;
    public ParticleSystem FireLight;
    public static float FireValue = 0.1f;
    #endregion

    #region Running Variables
    public PlayerMovement SpeedRun;
    public Slider AccelerationSlider;
  public static  bool IsTired = false;
    float TimerForRun = 0f;
    Image MyRunStripe;
    MouseLooker PlayerLooker;
    #endregion

    #region Brain Variables
    public Image BrainIcon;
    public Sprite[] BrainIconStages=  new Sprite[4];
    float TimerForBrain;
    public enum BrainStages {
        Warning1, Warning2, Warning3
    }
    public Image ScreenImage;
    public string currentSatge;
    #endregion

    public Text FlagScoreText;

    public Text TimeText;
    float TimerForGame;
    public static bool OnFired = false;
    #region AudioVariables
    public AudioMixerSnapshot Calm;
    public AudioMixerSnapshot Dang1;
    public AudioMixerSnapshot Dang2;
    public AudioMixerSnapshot Dang3;
    public AudioMixerSnapshot Death;

    float BMP = 128;
    float TransitionIn;
    float TransitionOut;
    #endregion
    
    public Text ScaryTextFeel;
    public Canvas NextLevelCanvas;
    

    void Start () {
        
        TimerForBrain = 0f;
        TimerForGame = 800f;
        StartCoroutine(CalmState());
        PlayerLooker = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLooker>();
    }
	
	void Update () {
       // if (!IsFired) { if (OnFired) { IsFired = true; } }
       
        FireSliderControl(FireValue);
        RunSliderControl();
        FlagController();
        TimeTextChanges();
        }

    void FireSliderControl(float FireValue)
    {
        if (IsFired)
        {
            if (Timer >= 1.5f)
            {
                FireSlider.value -= FireValue;
                Timer = 0f;
            }
        }
        else
        {
            if (FireSlider.value >= 95f)
            {
                Animator anim = FireInHand.GetComponent<Animator>();
               int p= Animator.StringToHash("FireOn");
                anim.SetTrigger("FireOn");
                IsFired = true;
                Debug.Log(IsFired.ToString()+" IsFired"+p.ToString());
                Invoke("FireHandActivity", 2f);

            }
        }
        if (FireSlider.value <= 0f)
        {
                Animator anim = FireInHand.GetComponent<Animator>();
            int p = Animator.StringToHash("FireOff");
            anim.Play(p);
            Invoke("FireHandActivity", 2f);
            IsFired = false;
            Debug.Log(IsFired.ToString() + " IsFired"+ p.ToString());
            
        }
        Timer += Time.deltaTime;
    }
    void RunSliderControl() {
        TimerForRun += Time.deltaTime;
        if (SpeedRun.currentSpeed >= 7f && !IsTired) {
            AccelerationSlider.value -= 4f;
            TimerForRun = 0f;
        }
        if (AccelerationSlider.value <= 0f)
        {
            IsTired = true;
            Debug.Log(SpeedRun.currentSpeed.ToString());
            MyRunStripe = AccelerationSlider.GetComponentInChildren<Image>();
            MyRunStripe.color = new Color(1f, 0f, 0f, 255f);
        }
        if (TimerForRun > 2f) {
            if (IsTired)
            { MyRunStripe.color = Color.clear;
                IsTired = false;
            }
            AccelerationSlider.value += .8f;
        }
    }
    void BrainControl() {
        if (TimerForBrain >= 4f && currentSatge == BrainStages.Warning1.ToString()) {
            BrainIcon.sprite = BrainIconStages[0];
            ScreenImage.color = Color.Lerp(ScreenImage.color, new Color(1f, 0f, 0f, 0.3f), 4.5f * Time.deltaTime);
            currentSatge = BrainStages.Warning2.ToString();
            Invoke("ColorReset", 0.3f);
            TimerForBrain = 0f;
        }
        if (TimerForBrain > 8f && currentSatge == BrainStages.Warning2.ToString()) {
            currentSatge = BrainStages.Warning3.ToString();
            BrainIcon.sprite = BrainIconStages[1];
            TimerForBrain = 0f;
        }
        if (TimerForBrain > 12f && currentSatge == BrainStages.Warning3.ToString())
        {
           
            BrainIcon.sprite = BrainIconStages[2];
            TimerForBrain = 0f;
        }
    }
    Color ColorReset() {
        return ScreenImage.color = Color.Lerp(ScreenImage.color,  Color.clear, 1f);
        
    }


    void FlagController() {
      //  FlagScoreText.text = FlagsController.NumOfFlags.ToString();
    }


    void TimeTextChanges() {
        TimerForGame -= Time.deltaTime;
        if (TimerForGame <= 720f) {
            TimeText.text = "1:AM";
        }
        if (TimerForGame <= 630f)
        {
            TimeText.text = "2:AM";
        }
        if (TimerForGame <= 540f)
        {
            TimeText.text = "3:AM";
        }
        if (TimerForGame <= 450f)
        {
            TimeText.text = "4:AM";
        }
        if (TimerForGame <= 360f)
        {
            TimeText.text = "5:AM";
            TimeText.color = new Color(163f, 174f, 53f, 1f);
        }
        if (TimerForGame <= 270f)
        {
            TimeText.text = "6:AM";
        }
        if (TimerForGame <= 180f)
        {
            TimeText.text = "7:AM";
        }
        if (TimerForGame <= 90f)
        {
            TimeText.text = "8:AM";
            TimeText.color = new Color(197f, 59f, 28f, 1f);
        }

        if (TimerForGame <= 0f) {
            BlockActivity();
            
        }
    }



    public IEnumerator CalmState() {
        Calm.TransitionTo(60 / BMP);
        Debug.Log("CalmStage");
        bool spec=true;
        while (spec)
        {
            if (IsFired)
                yield return null;
            else {
                TimerForBrain += Time.deltaTime;
                if (TimerForBrain >= 7f) { spec = false; }
                yield return null;
            }
        }
        if (TimerForBrain >= 7f && !IsFired)
        {
            TimerForBrain = 0f;
            BrainIcon.sprite = BrainIconStages[0];
            ScreenImage.color = Color.Lerp(ScreenImage.color, new Color(1f, 0f, 0f, 0.3f), 4.5f * Time.deltaTime);
            Invoke("ColorReset", 0.3f);
            Debug.Log("DangerousStage");
            StartCoroutine(StageDangerous1());
            yield break;
        }
        
        
    }

    public IEnumerator StageDangerous1() {
        Dang1.TransitionTo(60 / BMP);
        ScaryTextFeel.gameObject.SetActive(true);
        Invoke("FadeText", 2f);
        float TimeForCalming=0f;
        bool spec = true;
        while (spec)
        {
            if (IsFired)
            {
                TimerForBrain = 0f;
                if (TimerForBrain == 0f) { TimeForCalming += Time.deltaTime;
                    if (TimeForCalming >= 5f) { StartCoroutine(CalmState());
                        yield break; }
                }
                yield return null;
            }
                
            else
            {
                TimerForBrain += Time.deltaTime;
                if (TimerForBrain >= 7f) { spec = false; }
                yield return null;
            }
        }
        
        TimerForBrain = 0f;
        BrainIcon.sprite = BrainIconStages[1];
        ScreenImage.color = Color.Lerp(ScreenImage.color, new Color(1f, 0f, 0f, 0.3f), 4.5f * Time.deltaTime);
        Invoke("ColorReset", 0.3f);
        StartCoroutine(StageDangerous2());
        yield break;
    }

    public IEnumerator StageDangerous2() {
        Dang2.TransitionTo(60 / BMP);
        ScaryTextFeel.gameObject.SetActive(true);
        Invoke("FadeText", 2f);
        bool spec = true;
        float TimeForCalming = 0f;
        while (spec)
        {
            if (IsFired)
            {
                TimerForBrain = 0f;
                if (TimerForBrain == 0f)
                {
                    TimeForCalming += Time.deltaTime;
                    if (TimeForCalming >= 5f)
                    {
                        StartCoroutine(StageDangerous1());
                        yield break;
                    }
                }
                yield return null;
            }
            else
            {
                TimerForBrain += Time.deltaTime;
                if (TimerForBrain >= 8f) { spec = false; }
                yield return null;
            }
        }  
        TimerForBrain = 0f;
        BrainIcon.sprite = BrainIconStages[2];
        ScreenImage.color = Color.Lerp(ScreenImage.color, new Color(1f, 0f, 0f, 0.3f), 4.5f * Time.deltaTime);
        Invoke("ColorReset", 0.3f);
        StartCoroutine(StageDangerousCritical());
    }

    public IEnumerator StageDangerousCritical() {
        Dang3.TransitionTo(60 / BMP);
        ScaryTextFeel.gameObject.SetActive(true);
        Invoke("FadeText", 2f);
        bool spec = true;
        float TimeForCalming = 0f;
        while (spec)
        {
            if (IsFired)
            {
                TimerForBrain = 0f;
                if (TimerForBrain == 0f)
                {
                    TimeForCalming += Time.deltaTime;
                    if (TimeForCalming >= 5f)
                    {
                        StartCoroutine(StageDangerous2());
                        yield break;
                    }
                }
                yield return null;
            }
            else
            {
                TimerForBrain += Time.deltaTime;
                if (TimerForBrain >= 7f) { spec = false; }
                yield return null;
            }
        }
       
        TimerForBrain = 0f;
        BrainIcon.sprite = BrainIconStages[3];
        ScreenImage.color = Color.Lerp(ScreenImage.color, new Color(1f, 0f, 0f, 0.3f), 4.5f * Time.deltaTime);
        Invoke("ColorReset", 0.5f);
        StartCoroutine(StageMadnessDeath());
    }

    public IEnumerator StageMadnessDeath() {
        Death.TransitionTo(60 / BMP);
        TimerForBrain += Time.deltaTime;
        bool spec = true;
        float TimeForCalming = 0f;
        while (spec)
        {
            if (IsFired)
            {
                TimerForBrain = 0f;
                if (TimerForBrain == 0f)
                {
                    TimeForCalming += Time.deltaTime;
                    if (TimeForCalming >= 5f)
                    {
                        StartCoroutine(StageDangerousCritical());
                        yield break;
                    }
                }
                yield return null;
            }
            else
            {
                TimerForBrain += Time.deltaTime;
                if (TimerForBrain >= 7f) { spec = false; }
                yield return null;
            }
        }
        ScreenImage.color = Color.Lerp(ScreenImage.color, new Color(1f, 0f, 0f, 0.3f), 4.5f * Time.deltaTime);
    }


    void FadeText() {
        ScaryTextFeel.gameObject.SetActive(false);
    }


    public void ChangeLevel() {
        SceneManager.LoadScene(3);
    }


    public void BlockActivity() {
        NextLevelCanvas.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SpeedRun.enabled = false;
        PlayerLooker.enabled = false;
        gameObject.SetActive(false);
        
    }

    public void FireHandActivity() {
        if (!IsFired)
        {
            FireLight.Stop(true);
            FireLight.gameObject.SetActive(false);
        }
        else {
            FireLight.Play(true);
        }

    }
}
