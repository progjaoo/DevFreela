namespace DevFreela.Coree.Interfaces
{
    public interface IMessageBusService
    {
        void Publish(string queue, byte[] message);
    }
}
