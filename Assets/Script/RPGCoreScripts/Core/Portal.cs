

namespace RPG.Core
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.SceneManagement;

    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(SceneTransition());
            }
        }

        public IEnumerator SceneTransition()
        {
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            print("Scene Loaded");
            Destroy(gameObject);

        }
    }

}