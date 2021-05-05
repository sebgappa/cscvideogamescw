using System.Collections.Generic;
using UnityEngine;

public class CommandDispatcher : MonoBehaviour
{
    private List<Command> _commands = new List<Command>();
    private int _currentCommandIndex = -1;

    public void DispatchCommand(Command command)
    {
        _commands.Add(command);
        command.Execute();
        _currentCommandIndex = _commands.Count - 1;
    }
}
