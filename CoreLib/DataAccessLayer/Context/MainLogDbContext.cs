using System;
using System.Collections.Generic;
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
    public partial class MainLogDbContext : DbContext,IConnectionInfo
    {
        public MainLogDbContext()
        {
            // db.Configuration.LazyLoadingEnabled = false;
            // db.Configuration.ProxyCreationEnabled = false;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public string ConnectionString { get; set; }
        public MainLogDbContext(DbContextOptions<MainLogDbContext> options) : base(options)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #region DbSet Entities


        public virtual DbSet<EntityHistory> EntityHistory { get; set; }
      

        #endregion

        #region DbSet Views

        public virtual DbSet<ViewContactAddressDetail> ViewContactAddressDetail { get; set; }
     
        #endregion





        // Unable to generate entity type for table 'dbo.EntityHistory'. Please see the warning messages.


        //string GetDbConnection()
        //{
        //    string file = AppDomain.CurrentDomain.BaseDirectory + "/appsettings.json";
        //    if (File.Exists(file))
        //    {
        //        dynamic conf = JsonConvert.DeserializeObject(File.ReadAllText(file));
        //        string v = conf.ConnectionStrings.MSSQL;
        //        return v;
        //    }
        //    return "";
        //}

    
   
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            bool local = false;
            bool admin = false;
            string Ip = "10.255.101.60,1433";
            string user = "FlexPro";
            string password = "rtm_123";
            string connStr = $"Server ={Ip}; Database =ErkerCoreLog; User Id ={user}; Password ={password}; Pooling = true; ";

            optionsBuilder.UseSqlServer(connStr);
            // buranın configten gelmesi lazım

        }
    }
}