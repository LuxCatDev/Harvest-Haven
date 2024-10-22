using Godot;

namespace Component.State;

public partial class StateEvent: Node
{
    [Signal]
    public delegate void EmitedEventHandler();

    public void Emit()
    {
        EmitSignal(SignalName.Emited);
    }
}