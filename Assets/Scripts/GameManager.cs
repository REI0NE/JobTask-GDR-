using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _coinsForWin = 10;
    [SerializeField] private TextMeshProUGUI _coinsText, _resultText;
    [SerializeField] private CanvasGroup _gameOverScreen;
    private int _coins;

    private void OnEnable()
    {
        _player.Death += OnPlayerDeath;
        _player.PickupCoin += OnPlayerPickupCoin;
    }

    private void OnDestroy() => OnDisable();

    private void OnDisable()
    {
        _player.Death -= OnPlayerDeath;
        _player.PickupCoin -= OnPlayerPickupCoin;
    }

    private void OnPlayerPickupCoin()
    {
        _coins++;
        _coinsText.text = $"Coins\n<size=+40>{_coins}</size>";

        if (_coins >= _coinsForWin) GameOver(true);
    }

    private void OnPlayerDeath() => GameOver(false);


    private void GameOver(bool win)
    {
        Time.timeScale = 0;
        _resultText.text = win ? "You Win!" : "You Lose!";
        _gameOverScreen.gameObject.SetActive(true);
        _gameOverScreen.DOFade(1, 1);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
