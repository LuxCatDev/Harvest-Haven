using System.Collections.Generic;
using Component.State;
using Entities.Player;
using Godot;

namespace Components.State;

public partial class State: Node
{
    public Player player;

    public StateMachineComponent stateMachine;

    private List<StateEvent> events;

    [Export]
    private Godot.Collections.Array<StateEvent> launchEvents;

    public void Init()
    {
        events = new();
        
        foreach(StateEvent stateEvent in launchEvents)
        {
            stateEvent.Emited += () => stateMachine.ChangeState(this);
        }

        foreach(Node child in GetChildren())
        {
            if (child is StateEvent stateEvent)
            {
                events.Add(stateEvent);
            }
        }
    }

    public virtual void Enter() {

    }
    public virtual void Exit() {

    }
    public virtual void HandleProcess(double delta) {

    }
    public virtual void HandlePhysicsProcess(double delta) {

    }
    public virtual void HandleInput(InputEvent @event) {

    }

    public void EmitEvent(string eventName)
    {
        foreach(StateEvent stateEvent in events)
        {
            if (stateEvent.Name == eventName)
            {
                stateEvent.Emit();
            }
        }
    }

}