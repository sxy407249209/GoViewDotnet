using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class GoviewprojectsMapConfig : EntityMappingConfiguration<Goviewprojects>
    {
        public override void Map(EntityTypeBuilder<Goviewprojects>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

