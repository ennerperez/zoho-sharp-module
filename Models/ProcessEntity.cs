using System;
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable once CheckNamespace
namespace Zoho.Models
{
    public class ProcessEntity<T>
    {
        public Exception Error { get; set; }
        public T Data { get; set; }

    }
}
