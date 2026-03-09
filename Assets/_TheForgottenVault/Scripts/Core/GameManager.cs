using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }
    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        ChangeState(GameState.Playing);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        OnGameStateChanged?.Invoke(newState);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    public void WinGame()
    {
        ChangeState(GameState.Win);
    }

    public void PauseGame()
    {
        ChangeState(GameState.Paused);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        ChangeState(GameState.Playing);
        Time.timeScale = 1f;
    }
}