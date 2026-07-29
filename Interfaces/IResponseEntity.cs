using System;

namespace Zoho.Interfaces
{
    // ReSharper disable PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CheckNamespace
    public interface IResponseEntity<T>
    {
        Exception Error { get; set; }
        T Data { get; set; }
    }
}
