using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Collections;

namespace OzekiDemoSoftphoneWPF.Model.Data
{
    public class ObservableList<T> : ObservableCollection<T>
    {
        public ObservableList()
        {
            
        }

        public ObservableList(IEnumerable<T> enumerable)
        {
            foreach (var item in enumerable)
            {
                Add(item);
            }    
        }

        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            using (BlockReentrancy())
            {
                NotifyCollectionChangedEventHandler eventHandler = CollectionChanged;
                if (eventHandler == null)
                    return;

                Delegate[] delegates = eventHandler.GetInvocationList();

                foreach (NotifyCollectionChangedEventHandler handler in delegates)
                {
                    DispatcherObject dispatcherObject = handler.Target as DispatcherObject;
                    if (dispatcherObject != null && dispatcherObject.CheckAccess() == false)
                    {
                        dispatcherObject.Dispatcher.Invoke(DispatcherPriority.DataBind, handler, this, e);
                    }
                    else
                    {
                        handler(this, e);
                    }
                }
            }
        }
    }
}
