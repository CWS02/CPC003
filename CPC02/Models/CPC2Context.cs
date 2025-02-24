using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using CPC02.Models;

namespace CPC02.Models
{
    public partial class CPC2Context : DbContext
    {
        public CPC2Context()
            : base("name=ERPCPC")
        {
            this.Configuration.LazyLoadingEnabled = true; 
            this.Configuration.ProxyCreationEnabled = true; 
        }

        /// <summary>
        /// ­Ó¤H¤é»x
        /// </summary>
        public virtual DbSet<WLOGA> WLOGA { get; set; }
        public virtual DbSet<WLOGB> WLOGB { get; set; }


    }
}
