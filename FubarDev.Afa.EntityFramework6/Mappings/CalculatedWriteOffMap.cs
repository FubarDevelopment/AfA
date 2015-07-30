using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using FubarDev.Afa.Entities;

namespace FubarDev.Afa.EntityFramework6.Mappings
{
    /// <summary>
    /// Mapping für die <see cref="CalculatedWriteOff"/> Klasse.
    /// </summary>
    public class CalculatedWriteOffMap : EntityTypeConfiguration<CalculatedWriteOff>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="CalculatedWriteOffMap"/> Klasse.
        /// </summary>
        public CalculatedWriteOffMap()
        {
            ToTable("CalculatedWriteOff");
            HasKey(p => p.Id);

            Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Ignore(p => p.Month);
            Property(p => p.MonthDateTime).IsRequired().HasColumnName("Month");
            Property(p => p.Depreciation).IsRequired();
            Property(p => p.RemainingValue).IsRequired();
            Property(p => p.Status).IsRequired();
            Property(p => p.IsFixed).IsRequired();
        }
    }
}
