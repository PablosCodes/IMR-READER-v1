using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRReader.Domain.Models
{
    public record Target
    {
        public required int Id { get; set; }

        public required string Name { get; set; }
    }
}
