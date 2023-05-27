using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// todo rename
public class LogicScript : MonoBehaviour
{
    public GameObject gameOverScreen;
    public static bool isGameActive;

    private void Start()
    {
        isGameActive = true;
    }

    // Update is called once per frame
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        isGameActive = false;
    }

    public static bool isOutOfScreen(float objectY)
    {
        // desytoy when out of view
        float _outY = Camera.main.transform.position.y - Camera.main.orthographicSize - 1;
        return (objectY < _outY);
    }

    public static Color returnColor(int _colorNum)
    {
        switch (_colorNum)
        {
            case 0:
                return new Color(1, 0, 0, 1);
            case 1:
                return new Color(1, 0.7f, 0, 1);
            case 2:
                return new Color(1, 1, 0, 1);
            case 3:
                return new Color(0, 1, 0, 1);
            case 4:
                return new Color(0, 0, 1, 1);
            case 5:
                return new Color(1, 0, 1, 1);

            default:
                return new Color(1, 1, 1, 1);
        }
    }
}
