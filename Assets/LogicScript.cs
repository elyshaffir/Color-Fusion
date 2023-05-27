using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// todo rename
public class LogicScript : MonoBehaviour
{

    public GameObject gameOverScreen;

    // Update is called once per frame
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public static bool isOutOfScreen(float objectY)
    {
        // desytoy when out of view
        float _outY = Camera.main.transform.position.y - Camera.main.orthographicSize - 1;
        return (objectY < _outY);
    }
}
