using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class NewGameScript : MonoBehaviour
    {
        public int SceneId = 2;


        public void StartNewGame()
        {
            var timeForBegin = 0f;

            while (timeForBegin <= 3f)
            {
                timeForBegin += Time.deltaTime;
            }
            
            SceneManager.LoadSceneAsync(SceneId);     
        }

        public void OutFromGame()
        {
            var times = 0f;

            while (times <= 1.5f)
            {
                times += Time.deltaTime;
            }

            Application.Quit();
        }
    }
}
