using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace gstore.Models
{
    public class gstoredb : DbContext
    {
        public gstoredb()
        {

        }

        public virtual DbSet<login>  Login { get; set; }

    }
}