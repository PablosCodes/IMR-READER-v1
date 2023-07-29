using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRReader.Application.ViewModels
{
    public record Measurement
    {
        public required int Id { get; set; }
        public required DateTime Date { get; set; }
        public required string Method { get; set; }
        public required string Results { get; set; }
        public string? Comment { get; set; }

        public Measurement() { }
    }
}
