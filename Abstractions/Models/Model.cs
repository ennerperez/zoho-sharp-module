using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Infrastructure.Enterprise.Abstractions.Models
{
    public abstract class Model
    {
        public List<string> Errors { get; private set; }

        public virtual bool Validate()
        {
            if (Errors != null) 
                Errors.Clear();
            else 
                Errors = new List<string>();
            
            var props = this.GetType().GetProperties()
                .Where(m => m.GetCustomAttributes<JsonRequiredAttribute>().Any())
                .ToList();
            foreach (var prop in props)
            {
                if (prop.CanRead)
                {
                    var value = prop.GetValue(this);
                    if (value == null)
                    {
                        Errors.Add(prop.Name);
                        return false;
                    }
                }
            }

            return true;
        }
    }
}