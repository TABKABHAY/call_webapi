using curdoperation.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curdoperation.ViewModel
{
    public class CrudCreateModel
    {
        public Curd curd { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> CrdState { get; set; }

    }
}
