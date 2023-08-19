using IMRReader.Application.Abstract;
using IMRReader.Application.ViewModels;
using ReactiveUI;

namespace IMRReader.Application.Services
{
    public class MessageBusService : ReactiveObject, IMessageBusService
    {
        private readonly Queue<MessageVM> _messagesQueue;
        private DateTime? _messageOvertimeStarted;
        private MessageVM? _currentlyDisplayMessage;

        private string _currentMessage;
        public string CurrentMessage
        {
            get { return _currentMessage; }
            private set { this.RaiseAndSetIfChanged(ref _currentMessage, value); }
        }

        public MessageBusService()
        {
            _messagesQueue = new Queue<MessageVM>();
            _currentMessage = string.Empty;

            Task.Run(Loop);
        }

        public void Enqueue(MessageVM messageToAddVM)
        {
            if (messageToAddVM is null)
                throw new ArgumentNullException(nameof(messageToAddVM));

            _messagesQueue.Enqueue(messageToAddVM);
        }

        public void ForceEnqueue(MessageVM messageToAddVM)
        {
            if (messageToAddVM is null)
                throw new ArgumentNullException(nameof(messageToAddVM));

            _messagesQueue.Clear();
            Enqueue(messageToAddVM);
        }

        public void ClearQueue()
        {
            _messagesQueue.Clear();
            DisplayBlank();
        }

        private async Task Loop()
        {
            while (true)
            {
                if (_currentlyDisplayMessage is not null)
                {
                    if (_messageOvertimeStarted is not null)
                    {
                        TimeSpan interval = DateTime.Now - _messageOvertimeStarted.Value;
                        if (ShouldMoveNext(_currentlyDisplayMessage, _messagesQueue, interval))
                        {
                            await Next();
                        }
                    }
                    else
                    {
                        _messageOvertimeStarted = DateTime.Now;
                    }
                }
                else
                {
                    await Next();
                }
            }
        }

        private async Task Next()
        {
            MessageVM? messageToBeShown;
            if (_messagesQueue.TryDequeue(out messageToBeShown))
            {
                CurrentMessage = messageToBeShown.Content;
                _currentlyDisplayMessage = messageToBeShown;

                await Task.Delay(_currentlyDisplayMessage.MinimumDisplayTime);
                _messageOvertimeStarted = DateTime.Now;
            }
            else
            {
                DisplayBlank();
            }
        }

        private void DisplayBlank()
        {
            _currentlyDisplayMessage = default;
            CurrentMessage = string.Empty;
        }

        private bool ShouldMoveNext(MessageVM currentMessage, Queue<MessageVM> queue, TimeSpan timeLasted)
        {
            if (queue.Count > 0)
                return true;

            if (currentMessage.MaximumDisplayTime > timeLasted.TotalMilliseconds)
                return false;

            return !currentMessage.ShouldStay;
        }

    }
}
