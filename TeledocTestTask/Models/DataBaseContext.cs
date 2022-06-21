using Microsoft.EntityFrameworkCore;
using System;
namespace TeledocTestTask.Models
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Client> clients { get; set; }
        public DbSet<Founder> founders { get; set; }
        public DbSet<ClientFounder> clientFounders { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TeledocDB");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientFounder>()
                .HasKey(c => new { c.ClientId, c.FounderId });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.HasMany(x => x.ClientFounders).WithOne(x => x.Client).IsRequired().HasForeignKey(c => c.ClientId);
                entity.Property(x => x.Name).HasMaxLength(30);
                entity.Property(x => x.MiddleName).HasMaxLength(30);
                entity.Property(x => x.LastName).HasMaxLength(30);
            });

            modelBuilder.Entity<Founder>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.HasMany(x => x.ClientFounders).WithOne(x => x.Founder).IsRequired().HasForeignKey(c => c.FounderId);
                entity.Property(x => x.Name).HasMaxLength(30);
                entity.Property(x => x.MiddleName).HasMaxLength(30);
                entity.Property(x => x.LastName).HasMaxLength(30);
            });

            Founder founder1 = new Founder(1, "1234567890", "Иван", "Иванов", "Иванович", DateTime.Now, DateTime.Now);
            Founder founder2 = new Founder(2, "6781234590", "Андрей", "Комаров", "Алексеевич", DateTime.Now, DateTime.Now);
            Founder founder3 = new Founder(3, "3741689250", "Арсений", "Исаев", "Витальевич", DateTime.Now, DateTime.Now);
            Founder founder4 = new Founder(4, "8615234907", "Виталий", "Коршунов", "Романович", DateTime.Now, DateTime.Now);

            modelBuilder.Entity<Founder>(entity =>
            {
                entity.HasData(founder1);
                entity.HasData(founder2);
                entity.HasData(founder3);
                entity.HasData(founder4);
            });
            modelBuilder.Entity<Client>().HasData(new Client(1, "5234896710", "Наталья", "Завьялова", "Ивановна", ClientType.LegalEntity, DateTime.Now, DateTime.Now));
            modelBuilder.Entity<Client>().HasData(new Client(2, "6012345978", "Артемий", "Смирнов", "Тимофеевич", ClientType.IndividualEntrepreneur, DateTime.Now, DateTime.Now));

            ClientFounder clientFounder1 = new ClientFounder(1, 1);
            ClientFounder clientFounder2 = new ClientFounder(1, 2);
            modelBuilder.Entity<ClientFounder>().HasData(clientFounder1);
            modelBuilder.Entity<ClientFounder>().HasData(clientFounder2);
        }
    }
}
