using curdoperation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curdoperation.Data
{
    public class Selectlistcontex : DbContext
    {

        public Selectlistcontex(DbContextOptions<Selectlistcontex> options) : base(options)
        { }
        public virtual DbSet<Curd> Curds { get; set; }
        public virtual DbSet<county> Countries { get; set; }
        public virtual DbSet<crdstate> Crdstates { get; set; }





    }


}
