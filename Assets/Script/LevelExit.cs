using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        // aka: come back to run the following like after the delay
        yield return new WaitForSecondsRealtime(levelLoadDelay); 

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1; 


        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        // Before loading next level, have to destroy the ScenePersist object so that
        // the new one of the new level will be there to do the work
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        
        SceneManager.LoadScene(nextSceneIndex);
    }

}