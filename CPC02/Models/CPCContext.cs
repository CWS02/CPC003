using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using CPC02.Models;

namespace CPC02.Models
{
    public partial class CPCContext : DbContext
    {
        public CPCContext()
            : base("name=CPC2")
        {
            this.Configuration.LazyLoadingEnabled = true; 
            this.Configuration.ProxyCreationEnabled = true; 
        }

        /// <summary>
        /// 客戶訪談記錄
        /// </summary>
        public virtual DbSet<INTRA> INTRA { get; set; }
        /// <summary>
        /// 訪談記錄
        /// </summary>
        public virtual DbSet<INTRB> INTRB { get; set; }
        /// <summary>
        /// 報價
        /// </summary>
        public virtual DbSet<INTRC> INTRC { get; set; }
        /// <summary>
        /// 客戶狀態/來源
        /// </summary>
        public virtual DbSet<INTRD> INTRD { get; set; }
        /// <summary>
        /// 展覽訪談追蹤記錄
        /// </summary>
        public virtual DbSet<INTRE> INTRE { get; set; }
        /// <summary>
        /// 業務會員
        /// </summary>
        public virtual DbSet<Member> Member { get; set; }        
        /// <summary>
        /// 多筆上傳
        /// </summary>
        public virtual DbSet<Files> Files { get; set; }


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
