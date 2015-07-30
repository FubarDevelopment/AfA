using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using FubarDev.Afa.Entities;

namespace FubarDev.Afa.EntityFramework6.Mappings
{
    /// <summary>
    /// Mapping für die <see cref="Asset"/> Klasse.
    /// </summary>
    public class AssetMap : EntityTypeConfiguration<Asset>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="AssetMap"/> Klasse.
        /// </summary>
        public AssetMap()
        {
            ToTable("Asset");
            HasKey(p => p.Id);

            Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Account).IsRequired().HasMaxLength(7);
            Property(p => p.Name1).IsRequired().HasMaxLength(30);
            Property(p => p.Name2).HasMaxLength(30);
            Property(p => p.Amount).IsRequired();
            Property(p => p.CostCentre).HasMaxLength(10);
            Property(p => p.AcquisitionValue).IsRequired();
            Property(p => p.RemainingValue).IsRequired();
            Ignore(p => p.AcquisitionDate);
            Property(p => p.AcquisitionDateTime).IsRequired().HasColumnName("AcquisitionDate");
            Ignore(p => p.DispatchDate);
            Property(p => p.DispatchDateTime).HasColumnName("DispatchDate");
            Property(p => p.UserValue);

            HasMany(x => x.AssetWriteOffs).WithRequired(x => x.Asset).Map(m => m.MapKey("Asset_Id"));
            HasMany(x => x.AdditionalWriteUpOrOffs).WithRequired(x => x.Asset).Map(m => m.MapKey("Asset_Id"));
        }
    }
}
