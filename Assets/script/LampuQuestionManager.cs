using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class LampuQuestionManager : MonoBehaviour
{
    [Header("Component Reference")]
    [SerializeField] private LampuSpeechRecognition _lampuSpeechRecognition;
    [SerializeField] private CarMovement _carMovement;
    [Header("Container Reference")]
    [SerializeField] private GameObject _questionContainer;
    [SerializeField] private GameObject _benarContainer;
    [SerializeField] private GameObject _salahContainer;
    [Header("Variables")]
    [SerializeField] private int _currentQuestionNumber = 0;
    [SerializeField] private string _question1Answer;
    [SerializeField] private string _question2Answer;
    [SerializeField] private string _question3Answer;
    [Header("Score UI")]
    private int _score = 0;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject winPanel; // drag dari Inspector
    [SerializeField] private int maxScore = 3; // ganti 3 dengan jumlah soal sebenarnya
    [Header("Lose UI per Lampu")]
    [SerializeField] private GameObject scoreLose1;
    [SerializeField] private GameObject scoreLose2;
    [SerializeField] private GameObject scoreLose3;
    [Header("Lampu Hijau References")]
    [SerializeField] private GameObject _lampuHijau1;
    [SerializeField] private GameObject _lampuHijau2;
    [SerializeField] private GameObject _lampuMerah1;
    [SerializeField] private GameObject _lampuMerah2;
    [SerializeField] private GameObject _lampuMerah3;

    public bool _isAnswering;

    private void Start()
    {
        _currentQuestionNumber = 0;

    }

    public void InitializeQuestion()
    {
        _currentQuestionNumber++;
        switch (_currentQuestionNumber)
        {
            case 1:
                _lampuSpeechRecognition.SetCorrectAnswer(_question1Answer);
                break;
            case 2:
                _lampuSpeechRecognition.SetCorrectAnswer(_question2Answer);
                break;
            case 3:
                _lampuSpeechRecognition.SetCorrectAnswer(_question3Answer);
                break;
            default:
                break;
        }

        StartCoroutine(OnDelayTurnOnSpeechRecog());
    }

    IEnumerator OnDelayTurnOnSpeechRecog()
    {
        yield return new WaitForSeconds(0.5F);
        _questionContainer.SetActive(true);
        _lampuSpeechRecognition.ButtonClick();

        _isAnswering = true;

    }

    public void StartSpeechRecog()
    {

        _lampuSpeechRecognition.ButtonClick();
    }

    public void SetResult(bool isCorrect)
    {
        DeactiveAllUi();
        _isAnswering = false;
        if (isCorrect)
        {
            switch(_score)
            {
                case 0:
                    _lampuHijau1.gameObject.SetActive(true);
                    break;
                case 1:
                    _lampuHijau2.gameObject.SetActive(true);
                    break;
            }
            _score++;
            UpdateScoreUI(); // ← tampilkan score
            _carMovement.ResumeMovementOption();
            _benarContainer.SetActive(true);
        }

        else
        {
            _salahContainer.SetActive(true);
            TampilkanUILose(_currentQuestionNumber);
        }

        if (_score >= maxScore) // misal skor penuh
        {
            Debug.Log("You won the game");
            winPanel.SetActive(true); // munculkan tampilan menang
        }

        StartCoroutine(DelayResumeGame(isCorrect));
    }
    private void TampilkanUILose(int lampuKe)
    {
        switch (lampuKe)
        {
            case 1:
                scoreLose1.SetActive(true);
                _lampuMerah1.SetActive(true);
                break;
            case 2:
                scoreLose2.SetActive(true);
                _lampuMerah2.SetActive(true);
                break;
            case 3:
                scoreLose3.SetActive(true);
                _lampuMerah3.SetActive(true);
                break;
        }
    }
    private void UpdateScoreUI()
    {
        _scoreText.text = "Score: " + _score.ToString();
    }
    private void DeactiveAllUi()
    {
        _benarContainer.SetActive(false);
        _salahContainer.SetActive(false);
        _questionContainer.SetActive(false);
    }

    IEnumerator DelayResumeGame(bool isCorrect)
    {
        yield return new WaitForSeconds(2);
        if(isCorrect)
        {
            _benarContainer.SetActive(false);

            if (_currentQuestionNumber == 3)
            {
                Debug.Log("You won the game");
            }
        }
        else
        {
            _salahContainer.SetActive(false);
            _questionContainer.SetActive(true);
        }

    }
}
