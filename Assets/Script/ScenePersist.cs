using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()  // We will use SINGLETON pattern
    {
        int numScenePersist = FindObjectsOfType<ScenePersist>().Length; // Notice ..Objects.. 
                                                                        // The method returns an array.
                                                                        // .Length tells number of elements
        
        if(numScenePersist >= 2) // If there is one already, and this object is the second one.
        {
            Destroy(gameObject); // Kill this one. Let the first one be THE ONE AND ONLY !!!
        }
        else // This gameObject is THE FIRST ONE !!!!!
        {
            DontDestroyOnLoad(gameObject); // Keep this one alive.. don't kill it when
                                           // we are loading a new scene
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}

// Note: since this object has children... the children will still be there even after we load it.