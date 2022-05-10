using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace curdoperation.Models
{
    public class crdstate
    {
        [Key]
        public int curdstateid { get; set; }

        [ForeignKey("country")]
        public int countryid { get; set; }
        public string curdstatename { get; set; }

        //public virtual county County { get; set; }

    }
}
