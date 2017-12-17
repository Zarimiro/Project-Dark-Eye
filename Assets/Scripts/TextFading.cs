using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class TextFading : MonoBehaviour {
    Animator anim;
    public GameObject NextText;
    float Timer;
	// Use this for initialization
	void Start () {
        Timer = 0f;
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update () {
        MakeText();
	}

    void MakeText() {
        anim.SetTrigger("TimeToChangeText");
        if (Timer >= 6f)
        {
            DestroyImmediate(gameObject);

            if (NextText != null)
            {
                NextText.SetActive(true);

            }

        }
        Timer += Time.deltaTime;

      
    }
}
