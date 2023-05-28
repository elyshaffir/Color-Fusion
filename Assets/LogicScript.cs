using UnityEngine;
using UnityEngine.SceneManagement;

// todo rename
public class LogicScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public static bool isGameActive;

    private AudioSource _audioSource;

    private void Start()
    {
        isGameActive = true;
        _audioSource = GetComponent<AudioSource>();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        _audioSource.Play();
        isGameActive = false;
    }

    public static bool isOutOfScreen(float objectY)
    {
        // destroy when out of view
        float _outY = Camera.main.transform.position.y - Camera.main.orthographicSize - 1;
        return (objectY < _outY);
    }
}
