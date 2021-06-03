﻿using System;

namespace Infrastructure.Enterprise.Abstractions.Models
{
  public class ProcessEntity<T>
  {
    public Exception Error { get; set; }
    public T Data { get; set; }
  }
}