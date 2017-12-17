using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

class PauseGame : MonoBehaviour {
   bool pause;
   public Canvas CanV;
   public PlayerMovement PlayerMovement;
    public MouseLooker mouselook;
    
   public AudioSource aud;

	// Use this for initialization
	void Start () {
        pause = false;

	}
	
	// Update is called once per frame
	void Update () {


        
        if (Input.GetButton("Cancel")) {
            PlayerMovement.enabled = false;

            
            
            
            CanV.gameObject.SetActive(true);
            mouselook.enabled = false;
            Cursor.visible = true;
            Cursor.lockState=CursorLockMode.None;

        }
       
	}

    public void OnContinue() {
        if (CanV.gameObject.active) { 
            
            PlayerMovement.enabled = true;
            mouselook.enabled = true;
            CanV.gameObject.SetActive(false);
        }
    }
    public void OnOut() {

        
            SceneManager.LoadScene(0);


        
    }
}
