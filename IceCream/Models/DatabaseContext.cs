using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace IceCream.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountPayment> AccountPayments { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<FeedbackFormula> FeedbackFormulas { get; set; }
        public virtual DbSet<Formula> Formulas { get; set; }
        public virtual DbSet<InvoiceAccount> InvoiceAccounts { get; set; }
        public virtual DbSet<InvoiceDetailAccount> InvoiceDetailAccounts { get; set; }
        public virtual DbSet<PhotoFormula> PhotoFormulas { get; set; }
        public virtual DbSet<Savour> Savours { get; set; }
        public virtual DbSet<SavourBook> SavourBooks { get; set; }
        public virtual DbSet<SavourFormula> SavourFormulas { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceAccount> ServiceAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=CIA\\SQLEXPRESS;Database=Ice_Cream;user id=sa;password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccId)
                    .HasName("PK__account__9A20D5542E4E5F36");

                entity.ToTable("account");

                entity.Property(e => e.AccId).HasColumnName("acc_id");

                entity.Property(e => e.AccAddr)
                    .HasMaxLength(255)
                    .HasColumnName("acc_addr");

                entity.Property(e => e.AccAvatar)
                    .HasColumnType("text")
                    .HasColumnName("acc_avatar");

                entity.Property(e => e.AccDob)
                    .HasColumnType("date")
                    .HasColumnName("acc_dob");

                entity.Property(e => e.AccEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("acc_email");

                entity.Property(e => e.AccFullname)
                    .HasMaxLength(100)
                    .HasColumnName("acc_fullname");

                entity.Property(e => e.AccGender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("acc_gender");

                entity.Property(e => e.AccMoney).HasColumnName("acc_money");

                entity.Property(e => e.AccPassword)
                    .HasColumnType("text")
                    .HasColumnName("acc_password");

                entity.Property(e => e.AccPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("acc_phone");

                entity.Property(e => e.AccRole).HasColumnName("acc_role");

                entity.Property(e => e.AccStatus)
                    .HasColumnName("acc_status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AccUsername)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("acc_username");
            });

            modelBuilder.Entity<AccountPayment>(entity =>
            {
                entity.HasKey(e => e.AccPayId)
                    .HasName("PK__account___8FE96C60AFBDCD9B");

                entity.ToTable("account_payment");

                entity.Property(e => e.AccPayId).HasColumnName("acc_pay_id");

                entity.Property(e => e.AccId).HasColumnName("acc_id");

                entity.Property(e => e.AccPayAddr)
                    .HasMaxLength(250)
                    .HasColumnName("acc_pay_addr");

                entity.Property(e => e.AccPayName)
                    .HasMaxLength(50)
                    .HasColumnName("acc_pay_name");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.AccountPayments)
                    .HasForeignKey(d => d.AccId)
                    .HasConstraintName("FK__account_p__acc_i__276EDEB3");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.BookCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("book_created");

                entity.Property(e => e.BookDescription).HasColumnName("book_description");

                entity.Property(e => e.BookName)
                    .HasMaxLength(100)
                    .HasColumnName("book_name");

                entity.Property(e => e.BookPhoto)
                    .HasColumnType("text")
                    .HasColumnName("book_photo");

                entity.Property(e => e.BookPrice).HasColumnName("book_price");

                entity.Property(e => e.BookQuantity).HasColumnName("book_quantity");

                entity.Property(e => e.BookStatus)
                    .HasColumnName("book_status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.BookUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("book_update");

                entity.Property(e => e.BookYear).HasColumnName("book_year");
            });

            modelBuilder.Entity<FeedbackFormula>(entity =>
            {
                entity.HasKey(e => e.FeedbackId)
                    .HasName("PK__feedback__7A6B2B8CADDDA5A9");

                entity.ToTable("feedback_formula");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.AccId).HasColumnName("acc_id");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasColumnName("content");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.FeedbackStatus)
                    .HasColumnName("feedback_status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FormulaId).HasColumnName("formula_id");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.FeedbackFormulas)
                    .HasForeignKey(d => d.AccId)
                    .HasConstraintName("FK__feedback___acc_i__440B1D61");

                entity.HasOne(d => d.Formula)
                    .WithMany(p => p.FeedbackFormulas)
                    .HasForeignKey(d => d.FormulaId)
                    .HasConstraintName("FK__feedback___formu__4316F928");
            });

            modelBuilder.Entity<Formula>(entity =>
            {
                entity.HasKey(e => e.ForId)
                    .HasName("PK__formula__02A50948917052C5");

                entity.ToTable("formula");

                entity.Property(e => e.ForId).HasColumnName("for_id");

                entity.Property(e => e.ForCondition)
                    .HasMaxLength(6)
                    .HasColumnName("for_condition");

                entity.Property(e => e.ForContributors).HasColumnName("for_contributors");

                entity.Property(e => e.ForCover)
                    .HasColumnType("text")
                    .HasColumnName("for_cover");

                entity.Property(e => e.ForCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("for_created");

                entity.Property(e => e.ForDescription).HasColumnName("for_description");

                entity.Property(e => e.ForName)
                    .HasMaxLength(200)
                    .HasColumnName("for_name");

                entity.Property(e => e.ForStatus)
                    .HasColumnName("for_status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ForUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("for_update");

                entity.HasOne(d => d.ForContributorsNavigation)
                    .WithMany(p => p.Formulas)
                    .HasForeignKey(d => d.ForContributors)
                    .HasConstraintName("FK__formula__for_con__31EC6D26");
            });

            modelBuilder.Entity<InvoiceAccount>(entity =>
            {
                entity.HasKey(e => e.InvId)
                    .HasName("PK__invoice___A8749C2928621A69");

                entity.ToTable("invoice_account");

                entity.Property(e => e.InvId).HasColumnName("inv_id");

                entity.Property(e => e.AccId).HasColumnName("acc_id");

                entity.Property(e => e.Addr)
                    .HasMaxLength(255)
                    .HasColumnName("addr");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.InvPayment)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("inv_payment");

                entity.Property(e => e.InvStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("inv_status");

                entity.Property(e => e.InvTotal).HasColumnName("inv_total");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.InvoiceAccounts)
                    .HasForeignKey(d => d.AccId)
                    .HasConstraintName("FK__invoice_a__acc_i__47DBAE45");
            });

            modelBuilder.Entity<InvoiceDetailAccount>(entity =>
            {
                entity.ToTable("invoice_detail_account");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.InvId).HasColumnName("inv_id");

                entity.Property(e => e.ScName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("sc_name");

                entity.Property(e => e.ScPhoto)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("sc_photo");

                entity.Property(e => e.ScPrice).HasColumnName("sc_price");

                entity.Property(e => e.ScQuantity).HasColumnName("sc_quantity");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.InvoiceDetailAccounts)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__invoice_d__book___4BAC3F29");

                entity.HasOne(d => d.Inv)
                    .WithMany(p => p.InvoiceDetailAccounts)
                    .HasForeignKey(d => d.InvId)
                    .HasConstraintName("FK__invoice_d__inv_i__4AB81AF0");
            });

            modelBuilder.Entity<PhotoFormula>(entity =>
            {
                entity.HasKey(e => e.IdPhoto)
                    .HasName("PK__photo_fo__599E10ACFCCEB0BD");

                entity.ToTable("photo_formula");

                entity.Property(e => e.IdPhoto).HasColumnName("id_photo");

                entity.Property(e => e.ForId).HasColumnName("for_id");

                entity.Property(e => e.Img)
                    .HasColumnType("text")
                    .HasColumnName("img");

                entity.HasOne(d => d.For)
                    .WithMany(p => p.PhotoFormulas)
                    .HasForeignKey(d => d.ForId)
                    .HasConstraintName("FK__photo_for__for_i__398D8EEE");
            });

            modelBuilder.Entity<Savour>(entity =>
            {
                entity.HasKey(e => e.HashtagId)
                    .HasName("PK__savour__F59C84EC5FCF5084");

                entity.ToTable("savour");

                entity.Property(e => e.HashtagId)
                    .HasMaxLength(50)
                    .HasColumnName("hashtag_id");

                entity.Property(e => e.SavIngredients)
                    .HasMaxLength(250)
                    .HasColumnName("sav_ingredients");

                entity.Property(e => e.SavName)
                    .HasMaxLength(50)
                    .HasColumnName("sav_name");

                entity.Property(e => e.SavPhoto)
                    .HasColumnType("text")
                    .HasColumnName("sav_photo");

                entity.Property(e => e.SavProcedure).HasColumnName("sav_procedure");
            });

            modelBuilder.Entity<SavourBook>(entity =>
            {
                entity.HasKey(e => e.IdSavourBook)
                    .HasName("PK__savour_b__62190A3863156F2B");

                entity.ToTable("savour_book");

                entity.Property(e => e.IdSavourBook).HasColumnName("id_savour_book");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.HashtagId)
                    .HasMaxLength(50)
                    .HasColumnName("hashtag_id");

                entity.HasOne(d => d.Hashtag)
                    .WithMany(p => p.SavourBooks)
                    .HasForeignKey(d => d.HashtagId)
                    .HasConstraintName("FK__savour_bo__hasht__3F466844");
            });

            modelBuilder.Entity<SavourFormula>(entity =>
            {
                entity.HasKey(e => e.IdSavourFormula)
                    .HasName("PK__savour_f__0E57A4073A6FC1C9");

                entity.ToTable("savour_formula");

                entity.Property(e => e.IdSavourFormula).HasColumnName("id_savour_formula");

                entity.Property(e => e.ForId).HasColumnName("for_id");

                entity.Property(e => e.HashtagId)
                    .HasMaxLength(50)
                    .HasColumnName("hashtag_id");

                entity.HasOne(d => d.For)
                    .WithMany(p => p.SavourFormulas)
                    .HasForeignKey(d => d.ForId)
                    .HasConstraintName("FK__savour_fo__for_i__36B12243");

                entity.HasOne(d => d.Hashtag)
                    .WithMany(p => p.SavourFormulas)
                    .HasForeignKey(d => d.HashtagId)
                    .HasConstraintName("FK__savour_fo__hasht__35BCFE0A");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.SerId)
                    .HasName("PK__service__628330EB268C0131");

                entity.ToTable("service");

                entity.Property(e => e.SerId).HasColumnName("ser_id");

                entity.Property(e => e.SerDescription)
                    .HasMaxLength(250)
                    .HasColumnName("ser_description");

                entity.Property(e => e.SerName)
                    .HasMaxLength(50)
                    .HasColumnName("ser_name");

                entity.Property(e => e.SerPrice).HasColumnName("ser_price");
            });

            modelBuilder.Entity<ServiceAccount>(entity =>
            {
                entity.HasKey(e => e.SerAccId)
                    .HasName("PK__service___CF50F8733D1A551B");

                entity.ToTable("service_account");

                entity.Property(e => e.SerAccId).HasColumnName("ser_acc_id");

                entity.Property(e => e.AccId).HasColumnName("acc_id");

                entity.Property(e => e.SerAccCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("ser_acc_created");

                entity.Property(e => e.SerAccEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("ser_acc_end");

                entity.Property(e => e.SerAccPrice).HasColumnName("ser_acc_price");

                entity.Property(e => e.SerId).HasColumnName("ser_id");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.ServiceAccounts)
                    .HasForeignKey(d => d.AccId)
                    .HasConstraintName("FK__service_a__acc_i__2D27B809");

                entity.HasOne(d => d.Ser)
                    .WithMany(p => p.ServiceAccounts)
                    .HasForeignKey(d => d.SerId)
                    .HasConstraintName("FK__service_a__ser_i__2C3393D0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
