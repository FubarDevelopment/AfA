using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using FubarDev.Afa.Entities;

namespace FubarDev.Afa.EntityFramework6.Mappings
{
    /// <summary>
    /// Mapping für die <see cref="AdditionalWriteUpOrOff"/> Klasse.
    /// </summary>
    public class AdditionalWriteUpOrOffMap : EntityTypeConfiguration<AdditionalWriteUpOrOff>
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="AdditionalWriteUpOrOffMap"/> Klasse.
        /// </summary>
        public AdditionalWriteUpOrOffMap()
        {
            ToTable("AdditionalWriteUpOrOff");
            HasKey(p => p.Id);

            Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Value).IsRequired();
            Ignore(p => p.Date);
            Property(p => p.DateTime).IsRequired().HasColumnName("Date");
        }
    }
}
