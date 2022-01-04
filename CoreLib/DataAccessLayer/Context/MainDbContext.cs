using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ErkerCore.DataAccessLayer;
using ErkerCore.DataAccessLayer.Helper;
using ErkerCore.Entities;
using ErkerCore.Library;
using ErkerCore.Message.Model;
using ErkerCore.View;


namespace ErkerCore.DataAccessLayer
{
    public partial class MainDbContext : DbContext ,IConnectionInfo
    {
      public string ConnectionString { get; set; }
        public MainDbContext()
        {

        }
        public MainDbContext(string connectionString)
        {
            // db.Configuration.LazyLoadingEnabled = false;
            // db.Configuration.ProxyCreationEnabled = false;
            ConnectionString = connectionString;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }

        #region DbSet Entities
        public virtual DbSet<Adres> Address { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ContactAddress> ContactAddress { get; set; }
        public virtual DbSet<ContactPerson> ContactPerson { get; set; }
        public virtual DbSet<Feature> Feature { get; set; }
        public virtual DbSet<FeatureValue> FeatureValue { get; set; }
      //  [Table("FeatureValue")]
  
        public virtual DbSet<FeatureValueTranslate> FeatureValueTranslate { get; set; }
        public virtual DbSet<Subscriber> Subscriber { get; set; }
        public virtual DbSet<ContactPersonAccount> ContactPersonAccount { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<FileDocument> FileDocument { get; set; }




        #endregion

        #region DbSet Views
        public virtual DbSet<ViewContactAddressDetail> ViewContactAdresDetail { get; set; }
        public virtual DbSet<ViewContactDetail> ViewContactDetail { get; set; }
        public virtual DbSet<ViewContactPersonUser> ViewContactPersonUser { get; set; }
        public virtual DbSet<ViewContactRelationType> viewContactRelationType { get; set; }
        public virtual DbSet<ViewContactLoginUser> ViewContactLoginUser { get; set; }
        public virtual DbSet<ViewFeatureContactEmployeeCount> ViewFeatureContactEmployeeCount { get; set; }
        public virtual DbSet<ViewTaxOffice> ViewTaxOffice { get; set; }
        public virtual DbSet<ViewProduct> ViewProduct { get; set; }
        public virtual DbSet<ViewFeatureCurrencyType> ViewFeatureCurrencyType { get; set; }
        public virtual DbSet<ViewFeatureDepartment> ViewFeatureDepartment { get; set; }
        public virtual DbSet<ViewFeatureMeasureOfUnit> ViewFeatureMeasureOfUnit { get; set; }
        public virtual DbSet<ViewFeatureProductCategory> ViewFeatureProductCategory { get; set; }
        public virtual DbSet<ViewFeatureSector> ViewFeatureSector { get; set; }
        public virtual DbSet<ViewFeatureProductType> ViewFeatureProductType { get; set; }
        public virtual DbSet<ViewFeatureProductSupplier> ViewFeatureProductSupplier { get; set; }
        public virtual DbSet<ViewFeatureContactType> ViewFeatureContactType { get; set; }
        public virtual DbSet<ViewFeatureBank> ViewFeatureBank { get; set; }
        public virtual DbSet<ViewFeatureValue> ViewFeatureValue { get; set; }
        public virtual DbSet<ViewFeatureServiceStatus> ViewFeatureServiceStatus { get; set; }
        public virtual DbSet<ViewWorkOrder> ViewWorkOrder { get; set; }
        public virtual DbSet<ViewFeatureWorkOrderType> ViewFeatureWorkOrderType { get; set; }
        public virtual DbSet<ViewFeatureLinkedEquipment> ViewFeatureLinkedEquipment { get; set; }
        public virtual DbSet<ViewFeatureFailureCause> ViewFeatureFailureCause { get; set; }
        public virtual DbSet<ViewContactPerson> ViewContactPerson { get; set; }
        public virtual DbSet<ViewService> ViewService { get; set; }
        public virtual DbSet<ViewFeatureEmploymentEntry> ViewFeatureEmploymentEntry { get; set; }
        public virtual DbSet<ViewFeatureCurrencyHistory> ViewFeatureCurrencyHistory { get; set; }
        public virtual DbSet<ViewFeatureEventStatus> ViewFeatureEventStatus { get; set; }
        #endregion



        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        // Unable to generate entity type for table 'dbo.EntityHistory'. Please see the warning messages.


        string GetDbConnection()
        {
            string file = AppDomain.CurrentDomain.BaseDirectory + "appsettings.json";
            if (File.Exists(file))
            {
                dynamic conf = File.ReadAllText(file).DeSerialize();
                string v = conf.ConnectionStrings.DefaultConnection;
                return v;
            }
            return "";
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //    if (Startup)
            // string s= GetDbConnection();
            //  string s2 = ConfigurationManager.ConnectionStrings[0].ConnectionString;
       

            optionsBuilder.UseSqlServer(this.ConnectionString);

            // buranın configten gelmesi lazım

        }
    }
}