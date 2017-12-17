using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

 class ClipplingEyes : MonoBehaviour {
    VignetteAndChromaticAberration VignetteScript;
    float ClipplingTime = 0f;
    bool IsClosed;
	// Use this for initialization
	void Start () {
        VignetteScript = GetComponent<VignetteAndChromaticAberration>();
        ClipplingTime += Time.deltaTime;
        IsClosed = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (VignetteScript != null) {
            if (ClipplingTime >= 4f && !IsClosed) {
                VignetteScript.intensity = Mathf.Lerp(VignetteScript.intensity, 1f, 1f);
                ClipplingTime = 0f;
                IsClosed = true;
                return;
            }
            if (IsClosed && ClipplingTime >= .05f)
            {
                VignetteScript.intensity = Mathf.Lerp(VignetteScript.intensity, 0.34f, 1f);
                IsClosed = false;
                return;
            }
            ClipplingTime += Time.deltaTime;
            
        }
	}
}
