using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    [Tooltip("��������� - �������� ����")]
    [SerializeField] private GameState _startMenuState;
    [Tooltip("��������� - �������� ��������� ����")]
    [SerializeField] private GameState _actionState;    
    [Tooltip("��������� - �����")]
    [SerializeField] private GameState _pauseState;    
    [Tooltip("��������� - ������")]
    [SerializeField] private GameState _winState;       
    [Tooltip("��������� - ���������")]
    [SerializeField] private GameState _loseState;

    private GameState _currentGameState; // ������� ������� ���������
    

    public void Init()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        _startMenuState?.Init(this);
        _actionState?.Init(this);
        _pauseState?.Init(this);
        _winState?.Init(this);
        _loseState?.Init(this);

        SetGameState(_startMenuState);
    }

    private void SetGameState(GameState gameState)
    {
        if (_currentGameState)
        {
            _currentGameState.Exit(); //������� �� �������� ���������
        }
        _currentGameState = gameState; // �������� ������� ���������
        gameState.Enter();  //������ � ����� ���������
    }

    public void SetMenu()
    {
        SetGameState(_startMenuState);
    }

    public void SetAction()
    {
        SetGameState(_actionState);
    }

    public void SetPause()
    {
        SetGameState(_pauseState);
    }

    public void SetWin()
    {
        SetGameState(_winState);
    }

    public void SetLose()
    {
        SetGameState(_loseState);
    }
}
