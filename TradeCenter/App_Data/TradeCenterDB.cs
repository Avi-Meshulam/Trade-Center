namespace TradeCenter
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq;

    public class TradeCenterDB : DbContext
    {
        // Your context has been configured to use a 'TradeCenter' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TradeCenter.TradeCenter' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'TradeCenter' 
        // connection string in the application configuration file.
        public TradeCenterDB()
            : base("name=TradeCenter")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new TradeCenterDBInitializer());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkID=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 0);
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }
    }
}