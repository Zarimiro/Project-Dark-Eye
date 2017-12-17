using UnityEngine;
using UnityEngine.UI;
using System.Collections;

class CreateFlags : MonoBehaviour
{
    #region Variables
    public GameObject FlagObject;
    public Text TextWarning;
    Collider ZoneOfStuff = null;
    #endregion
    // Use this for initialization
    #region MethodsForCreatingObject
    private void OnTriggerEnter(Collider other)
    {
        ZoneOfStuff = other;
    }

    private void OnTriggerExit(Collider other)
    {
        ZoneOfStuff = null;
    }

   public void CreateFlag() {

        if (FlagsController.NumOfFlags > 0) {
            if (ZoneOfStuff == null) {
                GameObject Flag = Instantiate(FlagObject, gameObject.transform.position +transform.forward*4f, Quaternion.identity);
                FlagsController.NumOfFlags -= 1;
            }
            else
            {
                TextWarning.gameObject.SetActive(true);
                Invoke("StopMessaging", 2.5f);
            }
        }
    }

    void StopMessaging()
    {
        TextWarning.gameObject.SetActive(false);
    }
    #endregion
}
