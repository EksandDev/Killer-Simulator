using System;
using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<Type, State> _states = new();

    public State CurrentState { get; private set; }

    public void SetState<T>() where T : State
    {
        var type = typeof(T);

        if (CurrentState != null && CurrentState.GetType() == type)
            return;

        if (_states.TryGetValue(type, out var newState))
        {
            CurrentState?.Exit();
            CurrentState = newState;
            newState.Enter();
        }
    }

    public void AddState(State state) => _states.Add(state.GetType(), state);

    public void Update() => CurrentState?.Update();
}