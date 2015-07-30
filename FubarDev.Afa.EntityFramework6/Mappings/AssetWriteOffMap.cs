using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using FubarDev.Afa.Entities;

namespace FubarDev.Afa.EntityFramework6.Mappings
{
    /// <summary>
    /// Mapping für die <see cref="AssetWriteOff"/> Klasse.
    /// </summary>
    public class AssetWriteOffMap : EntityTypeConfiguration<AssetWriteOff>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="AssetWriteOffMap"/> Klasse.
        /// </summary>
        public AssetWriteOffMap()
        {
            ToTable("AssetWriteOff");
            HasKey(p => p.Id);

            Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.YearsSinceAcquisition).IsRequired();
            Property(p => p.CurrentValue).IsRequired();
            Property(p => p.Lifetime).IsRequired();
            Property(p => p.Rounding).IsRequired();
            Property(p => p.Precision).IsRequired();
            Property(p => p.CalculationMethod).IsRequired();
            Property(p => p.Percent);
            Property(p => p.ChangeToLinear).IsRequired();

            HasMany(x => x.CalculatedWriteOffs).WithRequired(x => x.AssetWriteOff).Map(m => m.MapKey("AssetWriteOff_Id"));
        }
    }
}
