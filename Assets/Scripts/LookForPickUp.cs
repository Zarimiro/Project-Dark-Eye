using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class LookForPickUp : MonoBehaviour {
    public Text PickUpText;
    public Text FoundedItemText;
    public Slider FireSlider;
    Collider InterestingPlace;
    bool IsVisited;
    // Use this for initialization
    private void Start()
    {
        InterestingPlace = null;
        IsVisited = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUps")
        {
            Debug.Log(other.name+" and "+ other.tag);
            Debug.Log("Yes, I have done it!");
            
                PickUpText.gameObject.SetActive(true);
                InterestingPlace = other;

            
        }

        if (other.tag == "FireStick") {
            Debug.Log("You founded avaliable fire stick");
            GameStatsControl.OnFired = true;
            Debug.Log(GameStatsControl.OnFired.ToString());
            if (PickUpText != null)
            {
                PickUpText.gameObject.SetActive(true);
                InterestingPlace = other;

            }

        }


        
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickUps" || other.tag=="FireStick")
        {
            Debug.Log(other.name + " and " + other.tag);
            Debug.Log("Yes, I move out");
            InterestingPlace = null;
            GameStatsControl.OnFired = false;
            Debug.Log(GameStatsControl.OnFired.ToString());
            if (PickUpText != null)
            {
                PickUpText.gameObject.SetActive(false);
                IsVisited = false;
            }
        }
    }


    public void OnPickUp() {


        if (PickUpText.IsActive() && !IsVisited)
        {

            if (InterestingPlace.gameObject.tag == "PickUps")
            {
                FlagsController.NumOfFlags += 1;
                Debug.Log("You looked for something..");
            }

            if (InterestingPlace.gameObject.tag == "FireStick")
            {
                FireSlider.value = 100f;
            }

            InterestingPlace.gameObject.tag = "Untagged";
          ParticleSystem WallFire =  InterestingPlace.GetComponentInChildren<ParticleSystem>();
            Light WallFireLight = InterestingPlace.GetComponentInChildren<Light>();
            WallFire.Stop();
            GameObject.Destroy(WallFire);
            GameObject.Destroy(WallFireLight);
                PickUpText.gameObject.SetActive(false);
                FoundedItemText.gameObject.SetActive(true);
                Invoke("VoidText", 2.5f);
            }
    }

    void VoidText() {

        FoundedItemText.gameObject.SetActive(false);
    }
}
