using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Data.Models
{
    public class PatchModel
    {
        public PatchOperations Operation { get; set; }
        public string Path { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($"Operation={Operation}");
            sb.AppendLine($"Path={Path}");
            sb.AppendLine($"Value={Value}");

            return sb.ToString();
        }
    }
}
