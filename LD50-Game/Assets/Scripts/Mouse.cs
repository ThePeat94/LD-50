using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir
{
    public class Mouse : MonoBehaviour
    {
        private static Mouse s_instance;

        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            SceneManager.sceneLoaded += this.SceneLoaded;
        }

        private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Cursor.visible = arg0.buildIndex == 0;
        }
    }
}