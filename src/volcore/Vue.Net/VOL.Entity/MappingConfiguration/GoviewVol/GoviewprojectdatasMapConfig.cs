using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class GoviewprojectdatasMapConfig : EntityMappingConfiguration<Goviewprojectdatas>
    {
        public override void Map(EntityTypeBuilder<Goviewprojectdatas>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

