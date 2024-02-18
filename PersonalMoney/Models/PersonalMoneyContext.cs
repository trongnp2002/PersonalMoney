using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PersonalMoney.Models
{
    public class PersonalMoneyContext : IdentityDbContext<User>
    {


        public PersonalMoneyContext(DbContextOptions<PersonalMoneyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Budget> Budgets { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<DebtDetail> DebtDetails { get; set; } = null!;
        public virtual DbSet<Debtor> Debtors { get; set; } = null!;
        public virtual DbSet<Otp> Otps { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Budget>(entity =>
            {
                entity.ToTable("Budget");

                entity.HasIndex(e => e.UserId, "IX_Budget_UserId");

                entity.Property(e => e.AnnuallySpending).HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Currency)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('USD')");

                entity.Property(e => e.MonthlyEarning).HasColumnType("decimal(11, 2)");

                entity.Property(e => e.MonthlySaving).HasColumnType("decimal(11, 2)");

                entity.Property(e => e.MonthlySpending).HasColumnType("decimal(11, 2)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Budgets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_budget_users");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_Categories_UserId");

                entity.Property(e => e.Budget).HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_categories");
            });

            modelBuilder.Entity<DebtDetail>(entity =>
            {
                entity.ToTable("DebtDetail");

                entity.HasIndex(e => e.DebtorId, "IX_DebtDetail_DebtorId");

                entity.HasIndex(e => e.WalletId, "IX_DebtDetail_WalletId");

                entity.Property(e => e.DateDebt).HasColumnType("datetime");

                entity.Property(e => e.MoneyDebt).HasColumnType("decimal(11, 2)");

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Debtor)
                    .WithMany(p => p.DebtDetails)
                    .HasForeignKey(d => d.DebtorId)
                    .HasConstraintName("fk_debtor_debt_detail");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.DebtDetails)
                    .HasForeignKey(d => d.WalletId)
                    .HasConstraintName("fk_wallet_debt_detail");
            });

            modelBuilder.Entity<Debtor>(entity =>
            {
                entity.ToTable("Debtor");

                entity.HasIndex(e => e.UserId, "IX_Debtor_UserId");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.Property(e => e.DateUpdate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Debtors)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_debtor");
            });

            modelBuilder.Entity<Otp>(entity =>
            {
                entity.ToTable("Otp");

                entity.HasIndex(e => e.UserId, "IX_Otp_UserId");

                entity.Property(e => e.Code)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Otps)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_otp_users");
            });

        
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.HasIndex(e => e.CategoryId, "IX_Transaction_CategoryId");

                entity.HasIndex(e => e.WalletId, "IX_Transaction_WalletId");

                entity.Property(e => e.Amount).HasColumnType("decimal(11, 2)");

                entity.Property(e => e.DateOfTransaction).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("fk_history_transaction_category");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.WalletId)
                    .HasConstraintName("fk_history_transaction_wallet");
            });

            modelBuilder.Entity<User>(entity =>
            {

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.AvatarUrl)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("AvatarURL");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(16)
                    .IsUnicode(false);


            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallet");

                entity.HasIndex(e => e.UserId, "IX_Wallet_UserId");

                entity.Property(e => e.Balance).HasColumnType("decimal(11, 2)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_wallet");
            });

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

        }

    }
}
