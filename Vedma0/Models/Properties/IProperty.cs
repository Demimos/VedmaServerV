using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vedma0.Models.Properties
{
    interface IProperty
    {
        string GetName { get; }
        string GetValue { get; }

        string GetPropertyType { get; }
    }
}
