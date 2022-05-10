using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace curdoperation.Models
{
    public class county
    {
        [Key]
        public int countryid{ get; set; }

        public string countryname{ get; set; }
    }
}
