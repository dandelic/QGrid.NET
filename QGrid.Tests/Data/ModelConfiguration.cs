using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QGrid.Tests.Data
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable(nameof(Employee));

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.CompanyId);

            builder.Property(e => e.Age).IsRequired();
            builder.Property(e => e.Salary).IsRequired();
            builder.Property(e => e.SalaryFloat).IsRequired();
            builder.Property(e => e.JoinDate).IsRequired();
            builder.Property(e => e.IsActive).IsRequired();
        }
    }

    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable(nameof(Company));

            builder.HasKey(c => c.Id);

            builder.Property(e => e.JoinDate).IsRequired();
            builder.Property(e => e.Code).IsRequired();
            builder.Property(e => e.DoubleCode).IsRequired();
            builder.Property(e => e.DecimalCode).IsRequired();
            builder.Property(e => e.FloatCode).IsRequired();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.HasOne(c => c.Address)
               .WithMany() 
               .HasForeignKey(c => c.AddressId)
               .OnDelete(DeleteBehavior.Cascade); 
        }
    }

    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable(nameof(Address));

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.State)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.PostalCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(100);

        }
    }

}
