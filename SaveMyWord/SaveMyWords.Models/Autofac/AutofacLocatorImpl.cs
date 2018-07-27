using Autofac;
using System;

namespace SaveMyWord.Models.Autofac
{
    public class AutofacLocatorImpl : ILocatorImpl
    {
        private IContainer container;

        public AutofacLocatorImpl(IContainer container)
        {
            this.container = container;
        }

        public object GetService(Type type)
        {
            object instance = null;
            container.TryResolve(type, out instance);
            return instance;
        }
    }
}