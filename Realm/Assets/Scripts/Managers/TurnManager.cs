﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface ITurnHandler
{
    public void OnTurn();
}

public interface ITurnMutationHandler
{
    public GameStateChange HandleTurn();
    public event Action MutationChanged;
}

public interface ITurnManager
{
    // Events
    public event Action TurnEnded;
    //
    void RegisterTurnHandler(ITurnHandler turnHandler);
    void RegisterTurnHandler(ITurnMutationHandler turnMutationHandler);
    event Action TurnStarted;
}

public class TurnManager : MonoBehaviour, ITurnManager
{
    // Events
    public event Action TurnEnded;
    public event Action TurnStarted;
    //

    [Inject] private IInputManager _inputManager;
    [Inject] private IGameState _gameState;

    private readonly List<ITurnHandler> _turnHandlers = new List<ITurnHandler>();
    private readonly List<ITurnMutationHandler> _turnMutationHandlers = new List<ITurnMutationHandler>();

    private void OnEnable()
    {
        _inputManager.PlayerPressedSpaceBar += InputManagerOnPlayerPressedSpaceBar;
    }

    private void InputManagerOnPlayerPressedSpaceBar()
    {
        EndTurn();
    }

    public void RegisterTurnHandler(ITurnHandler turnHandler)
    {
        _turnHandlers.Add(turnHandler);
    }
    public void RegisterTurnHandler(ITurnMutationHandler turnMutationHandler)
    {
        _turnMutationHandlers.Add(turnMutationHandler);
        turnMutationHandler.MutationChanged += () =>
        {
            RefreshHandler(turnMutationHandler);
        };
    }

    public void EndTurn()
    {
        // End turn
        RunTurnHandlers();
        
        TurnEnded?.Invoke();
        
        // New Turn Started
        RunTurnHandlers();
        
        TurnStarted?.Invoke();
    }

    private void RunTurnHandlers()
    {
        foreach (var handler in _turnMutationHandlers)
        {
            RefreshHandler(handler);
        }
        
        foreach (var handler in _turnHandlers)
        {
            handler.OnTurn();
        }
    }

    private void RefreshHandler(ITurnMutationHandler handler)
    {
        var change = handler.HandleTurn();

        _gameState.SetChange(change);
    }
}