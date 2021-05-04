using UnityEngine;
public class DieCommand : ICommand
{
    private IPlayer _entity;
    public DieCommand(IPlayer entity) 
    {
        _entity = entity;
    }
    public void Execute()
    {
        _entity.BoxCollider2D.enabled = false;
        _entity.PlayerInput.enabled = false;
        _entity.Rigidbody2D.velocity = new Vector2(0, 0);
        _entity.Animator.SetBool("isDead", true);
        _entity.Score.IncreaseScore();
    }
}
