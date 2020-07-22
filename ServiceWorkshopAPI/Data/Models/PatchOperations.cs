using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Data.Models
{
    public enum PatchOperations
    {
        Unknown = 0,
        Test = 1,
        Remove = 2,
        Add = 3,
        Replace = 4,
        Move = 5,
        Copy = 6
    }
}
