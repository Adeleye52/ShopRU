using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.DbContext.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasData
        (
            new Discount
            {
                Name = "Prunedge Deevelopment Technologies",
                Address = "32, Ade Ajayi Street, Ogudu GRA, Lagos",
                Country = "Nigeria"
            },
            new Discount
            {
                Name = "Elsavia",
                Address = "7b, Omo Ighodalo Street, Ogudu GRA, Lagos",
                Country = "Nigeria"
            }
        );
    }
}