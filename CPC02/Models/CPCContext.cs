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
        /// �Ȥ�X�ͰO��
        /// </summary>
        public virtual DbSet<INTRA> INTRA { get; set; }
        /// <summary>
        /// �X�ͰO��
        /// </summary>
        public virtual DbSet<INTRB> INTRB { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public virtual DbSet<INTRC> INTRC { get; set; }
        /// <summary>
        /// �Ȥ᪬�A/�ӷ�
        /// </summary>
        public virtual DbSet<INTRD> INTRD { get; set; }
        /// <summary>
        /// �i���X�Ͱl�ܰO��
        /// </summary>
        public virtual DbSet<INTRE> INTRE { get; set; }
        /// <summary>
        /// �~�ȷ|��
        /// </summary>
        public virtual DbSet<Member> Member { get; set; }        
        /// <summary>
        /// �h���W��
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
