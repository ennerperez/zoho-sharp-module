using System;

namespace Zoho.Interfaces
{
    
    // ReSharper disable PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CheckNamespace
    public interface IResponseEntity<T>
    {
        public Exception Error { get; set; }
        public T Data { get; set; }
    }
}
