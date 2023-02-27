using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 0;
    [SerializeField] int score = 0;


    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    
    public int GetScore()
    {
        return score;
    }

    void Awake() 
    {
        // ----------------
        // Sigleton pattern
        // ----------------
        // Count how GameSession object at this poing
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        // If there is a GameSession object already
        if(numGameSessions >= 2)  //  The first one and this one make it two.
        {
            Destroy(gameObject);  // Kill this one so the first one will be the only one.
        } 
        else // This is the first one
        {
            DontDestroyOnLoad(gameObject); // Make this object survive when we load a new scene
        }
    }

    


    void Start()
    {
        livesText.text = playerLives.ToString(); // Make the UI show the initial value
        scoreText.text = score.ToString();       // Make the UI show the initial value
        
    }

    public void AddToScore(int points) // This is public. When the coin is hit, it will call this.
    {
        score += points;
        scoreText.text = score.ToString();  // if we change the value and we want UI to update
                                            // we must do that manually
    }

    public void ProcessPlayerDeath()
{
    if (playerLives > 1)
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }
    else
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();

        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}

public void SetPlayerLives(int lives)
{
    playerLives = lives;
    livesText.text = playerLives.ToString();
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    SceneManager.LoadScene(nextSceneIndex);
}




}
