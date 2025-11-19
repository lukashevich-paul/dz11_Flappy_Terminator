using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private GameObject _panelWrapper;
    [SerializeField] private TextMeshProUGUI _panelScore;
    [SerializeField] private TextMeshProUGUI _panelRecord;

    [SerializeField] private Button _resetButton;
    [SerializeField] private InputHandler _inputHandler;

    private void OnEnable()
    {
        _scoreCounter.ScoreChanged += ChangeCurrentScore;
        _resetButton.onClick.AddListener(_inputHandler.InvokeResetAction);
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChanged -= ChangeCurrentScore;
        _resetButton.onClick.RemoveListener(_inputHandler.InvokeResetAction);
    }

    public void Show()
    {
        _panelWrapper.gameObject.SetActive(true);
        _currentScore.gameObject.SetActive(false);

        _panelScore.text = _scoreCounter.Score.ToString();
        _panelRecord.text = "Record: " + _scoreCounter.Record.ToString();
    }

    public void Hide()
    {
        _panelWrapper.gameObject.SetActive(false);
        _currentScore.gameObject.SetActive(true);
    }

    private void ChangeCurrentScore(int score)
    {
        _currentScore.text = score.ToString();
    }
}
