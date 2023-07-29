using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRReader.Application.ViewModels
{
    public class Target
    {
        public int Id { private set; get; }

        public string Name { private set; get; }

        public List<Measurement>? Measurements { get; set; }

        public Target(int id, string name)
        {
            Id = id;
            Name = name;           
        }
    }
}
