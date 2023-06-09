using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : GameState
{
    [Tooltip("�������� ������")]
    [SerializeField] private EnemyManager _enemyManager;
    [Tooltip("�������� �����")]
    [SerializeField] private ExperienceManager _experienceManager;
    [Tooltip("����")]
    [SerializeField] private ActiveWindow _activeWindow;

    public override void EnterFirstTime()
    {
        base.EnterFirstTime();
        _enemyManager.StartNewWave(0);
    }

    public override void Init(GameStateManager gameStateManager)
    {
        base.Init(gameStateManager);
    }

    public override void Enter()
    {
        base.Enter();
        _activeWindow.Show();
    }

    public override void Exit()
    {
        base.Exit();
        _activeWindow.Hide();
    }
}
