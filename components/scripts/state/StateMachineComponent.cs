using Entities.Player;
using Godot;
using System;
using System.Collections.Generic;

namespace Components.State;

public partial class StateMachineComponent : Node
{

	[Export]
	private Player player;

	[Export]
	private State initialState;

	private List<State> states;

	private State lastState;

	private State currentState;

	public void Init()
	{
		states = new();

		foreach(Node child in GetChildren())
		{
			if (child is State state)
			{
				state.Init();
				state.player = player;
				state.stateMachine = this;
				states.Add(state);
			}
		}

		ChangeState(initialState);
	}

	public void ChangeState(State state)
	{
		if (state == null) return;

		if (currentState != null)
		{
			lastState = currentState;

			currentState.Exit();
		}

		currentState = state;

		state.Enter();
	}

    public override void _Process(double delta)
    {
		if (currentState == null) return;
		currentState.HandleProcess(delta);
    }

	public override void _PhysicsProcess(double delta)
    {
		if (currentState == null) return;
		currentState.HandlePhysicsProcess(delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
		if (currentState == null) return;
		currentState.HandleInput(@event);
    }
}
