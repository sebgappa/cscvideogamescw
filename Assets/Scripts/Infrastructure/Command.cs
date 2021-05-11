public abstract class Command
{
    protected IEntity _entity;
    protected float _time;

    public Command(IEntity entity, float time)
    {
        _entity = entity;
        _time = time;
    }
    public abstract void Dispatch();

    public float GetTime()
    {
        return _time;
    }
}
