using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Game : MonoBehaviour
{
    public const float MaximumVolume = 20f;
    public const float MinimumVolume = -80f;

    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private Player _player;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private float _delay = 1f;
    [SerializeField] private ScorePanel _scorePanel;

    private Coroutine _coroutine;
    private float _previousVolume;

    private void Start()
    {
        _coroutine = StartCoroutine(Timer());

        if (_audioMixerGroup.audioMixer.GetFloat(_audioMixerGroup.name, out float previousVolume))
        {
            _previousVolume = previousVolume;
        }

        StopTime();
    }

    private void OnEnable()
    {
        _playerMover.Restart += StartTime;
        _player.GameOver += StopTime;
    }

    private void OnDisable()
    {
        _playerMover.Restart -= StartTime;
        _player.GameOver -= StopTime;
    }

    private void StartTime()
    {
        _scorePanel.Hide(); ;
        Time.timeScale = 1.0f;
        _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name, _previousVolume);

        if (_coroutine != null)
        {
            _coroutine = StartCoroutine(Timer());
        }
    }

    private void StopTime()
    {
        _scorePanel.Show();

        Time.timeScale = 0.0f;
        StopCoroutine(_coroutine);
        _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name, MinimumVolume);
    }

    private IEnumerator Timer()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            _scoreCounter.Add();
            yield return wait;
        }

        yield return null;
    }
}
