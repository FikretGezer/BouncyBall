using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIUpdate : MonoBehaviour
{
    [Header("Canvas In Game")]
    [SerializeField] GameObject _CanvasInGame;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] MaxScore _maxScore;

    [Header("Canvas End Game")]
    [SerializeField] GameObject _CanvasEndGame;
    [SerializeField] TMP_Text _endGame_Score;
    [SerializeField] TMP_Text _endGame_MaxScore;
    [SerializeField] TMP_Text _endGame_ScoreText;

    int _score = 0;
    public static bool isEnded;
    private void OnEnable()
    {
        BallController.onTouchedPlatform += UpdateScore;
    }
    private void OnDisable()
    {
        BallController.onTouchedPlatform -= UpdateScore;
    }
    private void UpdateScore()
    {
        _score += 1;
        if(_score>_maxScore.score)
        {
            _maxScore.score = _score;
            _endGame_ScoreText.text = "NEW RECORD";
        }
        else
            _endGame_ScoreText.text = "SCORE";
        _scoreText.text = _score.ToString();
    }
    private void Update()
    {
        if(isEnded)
        {
            _endGame_Score.text = _scoreText.text;
            if(_endGame_MaxScore!=null)
             _endGame_MaxScore.text = _maxScore.score.ToString();

            _CanvasInGame.SetActive(false);
            _CanvasEndGame.SetActive(true);
        }
    }
    public void PlayAgain()
    {
        isEnded = false;
        _CanvasInGame.SetActive(true);
        _CanvasEndGame.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
