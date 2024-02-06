using ClassLibrary1.PostgreDataStruct;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Libro
    {
        [Key]
        public int? id { get; set; }
        public string? titulo { get; set; }
        public string? autor { get; set; }
        public int? year { get; set; }
    }
    }
