using UnityEngine;

public interface IEntity
{
    Transform transform { get; }
    Animator animator { get; }
}
