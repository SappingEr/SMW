using System;

namespace SaveMyWord.Models.Autofac
{
    public interface ILocatorImpl
    {
        object GetService(Type type);
    }
}