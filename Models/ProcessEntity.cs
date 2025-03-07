using System;
using Zoho.Interfaces;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable once CheckNamespace
namespace Zoho.Models
{
    public class ProcessEntity<T> : IResponseEntity<T>
    {
        public Exception Error { get; set; }
        public T Data { get; set; }
    }
}
