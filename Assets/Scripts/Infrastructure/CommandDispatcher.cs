using System.Collections.Generic;
using UnityEngine;

public class CommandDispatcher : MonoBehaviour
{
    private List<ICommand> _commands = new List<ICommand>();
    private int _currentCommandIndex = -1;

    public void DispatchCommand(ICommand command)
    {
        _commands.Add(command);
        command.Execute();
        _currentCommandIndex = _commands.Count - 1;
    }
}
