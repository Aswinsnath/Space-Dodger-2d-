using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public float respawntime = 3.0f;
    public float respawnDelay = 3.0f;
    public int lives = 3;
    public ParticleSystem Explotion;
    public int score = 0;

    public Text livesText;
    public Text scoreText;
    public Text highScoreText; // Add a UI Text component for displaying the high score
    public GameObject gameOverCanvas; // Reference to the Game Over UI canvas

    public int highScore = 0; // Variable to store the high score

    private void Start()
    {
        // Set the initial values of UI text components on awake
        livesText.text = lives.ToString();
        scoreText.text = score.ToString();
        highScore = PlayerPrefs.GetInt("HighScore", 0); // Load the high score from PlayerPrefs
        highScoreText.text = "High Score: " + highScore.ToString(); // Display the high score on the UI

        // Disable the Game Over canvas initially
        gameOverCanvas.SetActive(false);
    }

    public void AstroidDistroied(Asteroid asteroid)
    {
        Explotion.transform.position = asteroid.transform.position;
        Explotion.Play();

        if (asteroid.size < 0.75f)
        {
            score += 100;
        }
        else if (asteroid.size < 1.0f)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }

       
        livesText.text = lives.ToString();
        scoreText.text = score.ToString();

        CheckGameOver(); 

        
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore); 
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    public void playedDied()
    {
        Explotion.transform.position = player.transform.position;
        Explotion.Play();

        lives--;

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), respawntime);
        }

        
        livesText.text = lives.ToString();
        scoreText.text = score.ToString();

        CheckGameOver(); 
    }

    private void CheckGameOver()
    {
        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("ignoreCollision");
        player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollision), respawnDelay);
    }

    private void TurnOnCollision()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        // Show the Game Over UI
        gameOverCanvas.SetActive(true);

       
    }
}
