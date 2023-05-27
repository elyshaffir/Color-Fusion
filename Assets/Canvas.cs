using TMPro;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    [SerializeField] private GameObject _mainGuy;
    [SerializeField] private TextMeshProUGUI scoreText;
    private float lastMainGuyPositionY;

    private int _score = 0;
    void Start()
    {
        lastMainGuyPositionY = _mainGuy.transform.position.y;
        scoreText.text = _score.ToString();
    }

    void Update()
    {
        if(!LogicScript.isGameActive || _mainGuy.transform.position.y <= lastMainGuyPositionY)
        {
            return;
        }

        lastMainGuyPositionY = _mainGuy.transform.position.y;
        _score = (int)(lastMainGuyPositionY * 10);
        scoreText.text = _score.ToString();
    }
}
