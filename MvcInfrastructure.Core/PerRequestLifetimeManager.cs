namespace MvcInfrastructure.Core
{
    using System;
    using System.Collections;
    using System.Threading;
    using System.Web;
    using Microsoft.Practices.Unity;

    public class PerRequestLifetimeManager : LifetimeManager, IDisposable
    {
        private static readonly string itemName = "PerRequestLifetimeManager";

        private static IDictionary Store
        {
            get
            {
                IDictionary backingStore = (HttpContext.Current != null) ? HttpContext.Current.Items : null;

                if (HttpContext.Current == null)
                {
                    backingStore = Thread.GetData(Thread.GetNamedDataSlot(itemName)) as IDictionary;

                    if (backingStore == null)
                    {
                        backingStore = new Hashtable();
                        Thread.SetData(Thread.GetNamedDataSlot(itemName), backingStore);
                    }
                }

                return backingStore;
            }
        }

        public override object GetValue()
        {
            return Store[itemName];
        }
        public override void RemoveValue()
        {
            Store.Remove(itemName);
        }

        public override void SetValue(object newValue)
        {
            Store[itemName] = newValue;
        }

        public void Dispose()
        {
            RemoveValue();
        }
    }
}
