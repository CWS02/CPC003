using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using CPC02.Models;

namespace CPC02.Models
{
    public partial class ESGContext : DbContext
    {
        public ESGContext()
            : base("name=ESG")
        {
            this.Configuration.LazyLoadingEnabled = true; 
            this.Configuration.ProxyCreationEnabled = true; 
        }

        public virtual DbSet<WD40A> WD40A { get; set; }
        public virtual DbSet<SGS_Parameter> SGS_Parameter { get; set; }
        public virtual DbSet<SGS_ParameterSetting> SGS_ParameterSetting { get; set; }

        public virtual DbSet<WATER_BILL> WATER_BILL { get; set; }
        public virtual DbSet<WASTES> WASTES { get; set; }
        public virtual DbSet<ColdCoal> ColdCoal { get; set; }
        public virtual DbSet<BRM_MST_EMISSION_FACTOR> BRM_MST_EMISSION_FACTOR { get; set; }
        public virtual DbSet<FireExtin> FireExtin { get; set; }
        public virtual DbSet<ELECTRICITY_BILL> ELECTRICITY_BILL { get; set; }
        public virtual DbSet<GHG_MST_COMMUTE> GHG_MST_COMMUTE { get; set; }
        public virtual DbSet<CAT_THREE_EMPLOYEE_COMMUTING> CAT_THREE_EMPLOYEE_COMMUTING { get; set; }
        public virtual DbSet<Diesel> Diesel { get; set; }
        public virtual DbSet<Gas_Parameter> Gas_Parameter { get; set; }
        

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
