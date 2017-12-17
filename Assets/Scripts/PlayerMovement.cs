using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using UnityStandardAssets.CrossPlatformInput;
class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;
    float SpeedForRunning;
    private Vector3 movement;
   // private Rigidbody rigbod;
    public float currentSpeed;
    CharacterController charT;
    public Slider SpeedSlider;

    // Use this for initialization
    void Awake()
    {
        charT = GetComponent<CharacterController>();
       // rigbod = GetComponent<Rigidbody>();
        currentSpeed = Speed;
        SpeedForRunning = 10f;

    }

    // Update is called once per frame
    void Update()
    {
        
        RunControl(GameStatsControl.IsTired);
        float InputUP = Input.GetAxisRaw("Vertical");
        
        float InputLeft = Input.GetAxisRaw("Horizontal");
        Move(InputUP, InputLeft);
    }

    void Move(float up, float left)
    {

        Vector3 MovementX = up * currentSpeed * Vector3.forward * Time.deltaTime;
        Vector3 MovementY = left * currentSpeed * Vector3.right * Time.deltaTime;
      //  Vector3 NewPosition = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0f, CrossPlatformInputManager.GetAxis("Vertical"));
      //  movement = transform.TransformDirection(NewPosition);
      //  movement = movement.normalized * currentSpeed * Time.deltaTime;
        movement = transform.TransformDirection(MovementX + MovementY);
       movement = movement.normalized * currentSpeed * Time.deltaTime;
       //rigbod.MovePosition(transform.position + movement);
        charT.Move(movement);
        
        
    }
    public void RunControl(bool IsTired)
    {
        if (SpeedSlider.value > 0f)
        {
            if (IsTired) currentSpeed = 5f; 
            if (Input.GetButton("Fire3"))
                currentSpeed = 10f;
            else currentSpeed = 5f;
        }
    }

}


