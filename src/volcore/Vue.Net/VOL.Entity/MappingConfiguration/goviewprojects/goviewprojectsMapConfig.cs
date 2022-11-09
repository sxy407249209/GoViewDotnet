using VOL.Entity.MappingConfiguration;
using VOL.Entity.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VOL.Entity.MappingConfiguration
{
    public class goviewprojectsMapConfig : EntityMappingConfiguration<goviewprojects>
    {
        public override void Map(EntityTypeBuilder<goviewprojects>
        builderTable)
        {
          //b.Property(x => x.StorageName).HasMaxLength(45);
        }
     }
}

