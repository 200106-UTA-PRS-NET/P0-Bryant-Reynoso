using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaBox.Client.Models
{
    public partial class PizzaDbContext : DbContext
    {
        public PizzaDbContext()
        {
        }

        public PizzaDbContext(DbContextOptions<PizzaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CrustTypes> CrustTypes { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OurPizzaToppings> OurPizzaToppings { get; set; }
        public virtual DbSet<OurPizzas> OurPizzas { get; set; }
        public virtual DbSet<PanSizes> PanSizes { get; set; }
        public virtual DbSet<Stores> Stores { get; set; }
        public virtual DbSet<Toppings> Toppings { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress; Database=PizzaDb; Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrustTypes>(entity =>
            {
                entity.ToTable("CrustTypes", "PizzaBox");

                entity.Property(e => e.CrustName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("Orders", "PizzaBox");

                entity.Property(e => e.Pizzas)
                    .IsRequired()
                    .HasColumnName("pizzas")
                    .IsUnicode(false);

                entity.Property(e => e.Storeid)
                    .HasColumnName("storeid")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TimeOrdered).HasColumnType("smalldatetime");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Storeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Id");
            });

            modelBuilder.Entity<OurPizzaToppings>(entity =>
            {
                entity.HasKey(e => new { e.PizzaId, e.ToppingId })
                    .HasName("PK__OurPizza__45803E152DF280C9");

                entity.ToTable("OurPizzaToppings", "PizzaBox");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.OurPizzaToppings)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OurPizzaT__Pizza__160F4887");

                entity.HasOne(d => d.Topping)
                    .WithMany(p => p.OurPizzaToppings)
                    .HasForeignKey(d => d.ToppingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OurPizzaT__Toppi__17036CC0");
            });

            modelBuilder.Entity<OurPizzas>(entity =>
            {
                entity.ToTable("OurPizzas", "PizzaBox");

                entity.Property(e => e.PizzaName)
                    .IsRequired()
                    .HasColumnName("pizzaName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<PanSizes>(entity =>
            {
                entity.ToTable("PanSizes", "PizzaBox");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stores>(entity =>
            {
                entity.ToTable("Stores", "PizzaBox");

                entity.Property(e => e.Inventory)
                    .HasColumnName("inventory")
                    .IsUnicode(false);

                entity.Property(e => e.Sales)
                    .HasColumnName("sales")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.StoreAddress)
                    .IsRequired()
                    .HasColumnName("storeAddress")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Toppings>(entity =>
            {
                entity.ToTable("Toppings", "PizzaBox");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ToppingName)
                    .IsRequired()
                    .HasColumnName("toppingName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users", "PizzaBox");

                entity.Property(e => e.IsEmployee).HasColumnName("isEmployee");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("pass")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
