using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDispatcher : MonoBehaviour
{
    private List<Command> _commands = new List<Command>();
    private float _timeSinceLastCommand = -1;

    public void DispatchCommand(Command command)
    {
        _commands.Add(command);
        command.Dispatch();
    }

    public IEnumerator ReplayCommands()
    {
        for(int i = 0; i < _commands.Count; i++)
        {
            if(_timeSinceLastCommand == -1)
            {
                yield return new WaitForSeconds(_commands[i].GetTime());
                _commands[i].Dispatch();
                _timeSinceLastCommand = _commands[i].GetTime();
            } else
            {
                var waitTime = _commands[i].GetTime() - _timeSinceLastCommand;
                yield return new WaitForSeconds(waitTime);
                _commands[i].Dispatch();
                _timeSinceLastCommand = _commands[i].GetTime();
            }
        }
    }
}
