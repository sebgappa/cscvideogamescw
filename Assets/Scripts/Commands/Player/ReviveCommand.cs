using UnityEngine;
public class ReviveCommand : ICommand
{
    private IPlayer _entity;
    public ReviveCommand(IPlayer entity)
    {
        _entity = entity;
    }
    public void Execute()
    {
        _entity.Animator.SetBool("isDead", false);
        _entity.Animator.SetTrigger("Revive");
        _entity.CurrentHealth = _entity.MaxHealth;
        _entity.BoxCollider2D.enabled = true;
        _entity.PlayerInput.enabled = true;
    }
}
