using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSubscribeLab
{
    class Lab
    {
        private Subscriber _subscriber;
        private Publisher _publisher;

        private WeakReference<Subscriber> _subscriberWeakReference;
        private WeakReference<Publisher> _publisherWeakReference;

        public Lab()
        {
            _subscriber = new Subscriber();
            _publisher = new Publisher();

            _subscriber.Subscribe(_publisher);
            _subscriberWeakReference = new WeakReference<Subscriber>(_subscriber);
            _publisherWeakReference = new WeakReference<Publisher>(_publisher);
        }

        public void ReleaseSubscriber()
        {
            _subscriber = null;
            GC.Collect();
        }

        public void ReleasePublisher()
        {
            _publisher = null;
            GC.Collect();
        }

        public bool TestExistSubscriber()
        {
            Subscriber outSubscriber;
            return _subscriberWeakReference != null && _subscriberWeakReference.TryGetTarget(out outSubscriber);
        }

        public bool TestExistPublisher()
        {
            Publisher outPublisher;
            return _publisherWeakReference != null && _publisherWeakReference.TryGetTarget(out outPublisher);
        }
    }

    /// <summary>
    /// 事件订阅者
    /// </summary>
    class Subscriber
    {
        public void Subscribe(Publisher publisher)
        {
            publisher.TestEvent += PublisherOnTestEvent;   
        }

        private void PublisherOnTestEvent(object sender, EventArgs eventArgs)
        {
        }
    }

    class Publisher
    {
        public event EventHandler TestEvent;

        protected virtual void OnTestEvent()
        {
            TestEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
