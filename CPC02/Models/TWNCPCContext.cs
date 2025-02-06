using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using CPC02.Models;

namespace CPC02.Models
{
    public partial class TWNCPCContext : DbContext
    {
        public TWNCPCContext()
            : base("name=TWNCPC")
        {
            this.Configuration.LazyLoadingEnabled = true; 
            this.Configuration.ProxyCreationEnabled = true; 
        }
        /// <summary>
        /// ���u�򥻸��
        /// </summary>
        public virtual DbSet<CMSME> CMSME { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public virtual DbSet<CMSMV> CMSMV { get; set; }
        /// <summary>
        /// ¾��
        /// </summary>
        public virtual DbSet<CMSMJ> CMSMJ { get; set; }

    }
}
