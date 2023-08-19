using IMRReader.Application.ViewModels;

namespace IMRReader.Application.Abstract
{
    public interface IMessageBusService
    {
        string CurrentMessage { get; }
        void Enqueue(MessageVM messageToEnqueueVM);
        void ForceEnqueue(MessageVM messageToEnqueueVM);
        void ClearQueue();
    }
}
