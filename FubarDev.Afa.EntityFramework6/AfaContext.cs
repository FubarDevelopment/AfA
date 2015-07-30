using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using FubarDev.Afa.Entities;
using FubarDev.Afa.EntityFramework6.Mappings;

namespace FubarDev.Afa.EntityFramework6
{
    /// <summary>
    /// Der Datenbank-Kontext für eine AfA-Datenbank
    /// </summary>
    public class AfaContext : DbContext
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="AfaContext"/> Klasse.
        /// </summary>
        public AfaContext()
            : base("AfaContext")
        {
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="AfaContext"/> Klasse.
        /// </summary>
        /// <param name="nameOrConnectionString">Name oder ConnectionString</param>
        public AfaContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="AfaContext"/> Klasse.
        /// </summary>
        /// <param name="existingConnection">Die Datenbank-Verbindung, die dieser Datenbank-Kontext nutzen soll</param>
        /// <param name="contextOwnsConnection"><code>true</code> wenn der Datenbank-Kontext der neue Eigentümer der Datenbank-Verbindung werden soll</param>
        public AfaContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <summary>
        /// Die Anlagen
        /// </summary>
        public DbSet<Asset> Assets { get; set; }

        /// <summary>
        /// Die Einstellungen für die Abschreibungen
        /// </summary>
        public DbSet<AssetWriteOff> AssetWriteOffs { get; set; }

        /// <summary>
        /// Die berechneten Abschreibungen
        /// </summary>
        public DbSet<CalculatedWriteOff> CalculatedWriteOffs { get; set; }

        /// <summary>
        /// Die zusätzlichen Zu- oder Abschreibungen
        /// </summary>
        public DbSet<AdditionalWriteUpOrOff> AdditionalWriteUpOrOffs { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        ///             before the model has been locked down and used to initialize the context.  The default
        ///             implementation of this method does nothing, but it can be overridden in a derived class
        ///             such that the model can be further configured before it is locked down.
        /// </summary>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        ///             is created.  The model for that context is then cached and is for all further instances of
        ///             the context in the app domain.  This caching can be disabled by setting the ModelCaching
        ///             property on the given ModelBuidler, but note that this can seriously degrade performance.
        ///             More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        ///             classes directly.
        /// </remarks>
        /// <param name="modelBuilder">The builder that defines the model for the context being created. </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new AssetMap());
            modelBuilder.Configurations.Add(new AssetWriteOffMap());
            modelBuilder.Configurations.Add(new AdditionalWriteUpOrOffMap());
            modelBuilder.Configurations.Add(new CalculatedWriteOffMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
