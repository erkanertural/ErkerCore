using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ErkerCore.DataAccessLayer;
using ErkerCore.DataAccessLayer.Helper;
using ErkerCore.Entities;

using ErkerCore.View;


namespace ErkerCore.DataAccessLayer
{
    public partial class MainFileDbContext : DbContext, IConnectionInfo
    {
        public MainFileDbContext()
        {
            // db.Configuration.LazyLoadingEnabled = false;
            // db.Configuration.ProxyCreationEnabled = false;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public MainFileDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public string ConnectionString { get; set; }
        #region DbSet Entities
        public virtual DbSet<FileDocument> FileDocument { get; set; }

        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            bool local = false;
            bool admin = false;
            string Ip = "10.255.101.60,1433";
            string user = "FlexPro";
            string password = "rtm_123";
            string connStr = $"Server ={Ip}; Database =ErkerCoreFileDocument; User Id ={user}; Password ={password}; Pooling = true; ";

            optionsBuilder.UseSqlServer(connStr);

            // buranın configten gelmesi lazım

        }
    }
}