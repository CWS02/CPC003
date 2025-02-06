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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<INTRA>()
                .HasMany(intra => intra.INTRBs)
                .WithRequired()                 
                .HasForeignKey(intrb => intrb.INT999);

            modelBuilder.Entity<INTRA>()
                .HasMany(intra => intra.INTRCs)
                .WithRequired()
                .HasForeignKey(intrb => intrb.INT999);

            modelBuilder.Entity<INTRB>()
            .HasRequired(i => i.Member)  
            .WithMany()  
            .HasForeignKey(i => i.Mid);
        }

    }
}
