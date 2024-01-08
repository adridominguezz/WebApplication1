using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class UserIgnore
    {
        [JsonIgnore]
        public int? idUser { get; set; }
        public string? Users { get; set; }
        public string? pass { get; set; }
        public string? email { get; set; }
        public int? Administrador { get; set; }
        public int? Manage { get; set; }
        public int? idNegocio { get; set; }
        public int? validated { get; set; }
    }
}
