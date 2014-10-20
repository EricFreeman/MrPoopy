namespace Assets.Scripts.EventHandler
{
    public interface IListener<T>
    {
        void Handle(T message);
    }
}