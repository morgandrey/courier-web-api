using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CourierWebApi.Models
{
    public partial class BoxOwlDbContext : DbContext
    {
        public BoxOwlDbContext()
        {
        }

        public BoxOwlDbContext(DbContextOptions<BoxOwlDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarCourier> CarCouriers { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Courier> Couriers { get; set; }
        public virtual DbSet<CourierRating> CourierRatings { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductBranch> ProductBranches { get; set; }
        public virtual DbSet<ProductOrder> ProductOrders { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=BoxOwlDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.IdBranch)
                    .HasName("Branch_pk")
                    .IsClustered(false);

                entity.ToTable("Branch");

                entity.Property(e => e.IdBranch).HasColumnName("id_branch");

                entity.Property(e => e.BranchAddress)
                    .IsRequired()
                    .HasColumnName("branch_address");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasColumnName("branch_name");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.IdCar)
                    .HasName("Car_pk")
                    .IsClustered(false);

                entity.ToTable("Car");

                entity.Property(e => e.IdCar).HasColumnName("id_car");

                entity.Property(e => e.CarName)
                    .IsRequired()
                    .HasColumnName("car_name");
            });

            modelBuilder.Entity<CarCourier>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Car_Courier_pk")
                    .IsClustered(false);

                entity.ToTable("Car_Courier");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CarNumber)
                    .IsRequired()
                    .HasColumnName("car_number");

                entity.Property(e => e.IdCar).HasColumnName("id_car");

                entity.Property(e => e.IdCourier).HasColumnName("id_courier");

                entity.HasOne(d => d.IdCarNavigation)
                    .WithMany(p => p.CarCouriers)
                    .HasForeignKey(d => d.IdCar)
                    .HasConstraintName("Car_Courier_Car_id_car_fk");

                entity.HasOne(d => d.IdCourierNavigation)
                    .WithMany(p => p.CarCouriers)
                    .HasForeignKey(d => d.IdCourier)
                    .HasConstraintName("Car_Courier_Courier_id_courier_fk");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient)
                    .HasName("Client_pk")
                    .IsClustered(false);

                entity.ToTable("Client");

                entity.Property(e => e.IdClient).HasColumnName("id_client");

                entity.Property(e => e.ClientEmail)
                    .IsRequired()
                    .HasColumnName("client_email");

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasColumnName("client_name");

                entity.Property(e => e.ClientPassword)
                    .IsRequired()
                    .HasColumnName("client_password");

                entity.Property(e => e.ClientPatronymic).HasColumnName("client_patronymic");

                entity.Property(e => e.ClientPhone)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasColumnName("client_phone");

                entity.Property(e => e.ClientRating)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("client_rating");

                entity.Property(e => e.ClientSalt)
                    .IsRequired()
                    .HasColumnName("client_salt");

                entity.Property(e => e.ClientSurname)
                    .IsRequired()
                    .HasColumnName("client_surname");
            });

            modelBuilder.Entity<Courier>(entity =>
            {
                entity.HasKey(e => e.IdCourier)
                    .HasName("Courier_pk")
                    .IsClustered(false);

                entity.ToTable("Courier");

                entity.Property(e => e.IdCourier).HasColumnName("id_courier");

                entity.Property(e => e.CourierImage).HasColumnName("courier_image");

                entity.Property(e => e.CourierMoney)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("courier_money");

                entity.Property(e => e.CourierName)
                    .IsRequired()
                    .HasColumnName("courier_name");

                entity.Property(e => e.CourierPassword)
                    .IsRequired()
                    .HasColumnName("courier_password");

                entity.Property(e => e.CourierPatronymic).HasColumnName("courier_patronymic");

                entity.Property(e => e.CourierPhone)
                    .IsRequired()
                    .HasColumnName("courier_phone");

                entity.Property(e => e.CourierRating)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("courier_rating");

                entity.Property(e => e.CourierSalt)
                    .IsRequired()
                    .HasColumnName("courier_salt");

                entity.Property(e => e.CourierSurname)
                    .IsRequired()
                    .HasColumnName("courier_surname");
            });

            modelBuilder.Entity<CourierRating>(entity =>
            {
                entity.HasKey(e => e.IdCourierRating)
                    .HasName("Courier_Rating_pk")
                    .IsClustered(false);

                entity.ToTable("Courier_Rating");

                entity.Property(e => e.IdCourierRating).HasColumnName("id_courier_rating");

                entity.Property(e => e.IdClient).HasColumnName("id_client");

                entity.Property(e => e.IdCourier).HasColumnName("id_courier");

                entity.Property(e => e.IdRating).HasColumnName("id_rating");

                entity.Property(e => e.IsRatedByCourier).HasColumnName("is_rated_by_courier");

                entity.Property(e => e.RatingComment).HasColumnName("rating_comment");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.CourierRatings)
                    .HasForeignKey(d => d.IdClient)
                    .HasConstraintName("Courier_Rating_Client_id_client_fk");

                entity.HasOne(d => d.IdCourierNavigation)
                    .WithMany(p => p.CourierRatings)
                    .HasForeignKey(d => d.IdCourier)
                    .HasConstraintName("Courier_Rating_Courier_id_courier_fk");

                entity.HasOne(d => d.IdRatingNavigation)
                    .WithMany(p => p.CourierRatings)
                    .HasForeignKey(d => d.IdRating)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Courier_Rating_Rating_id_rating_fk");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.IdMessage)
                    .HasName("Message_pk")
                    .IsClustered(false);

                entity.ToTable("Message");

                entity.Property(e => e.IdMessage).HasColumnName("id_message");

                entity.Property(e => e.IdChatClient).HasColumnName("id_chat_client");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.MessageDate)
                    .IsRequired()
                    .HasColumnName("message_date");

                entity.Property(e => e.MessageFrom)
                    .IsRequired()
                    .HasColumnName("message_from");

                entity.Property(e => e.MessageInput)
                    .IsRequired()
                    .HasColumnName("message_input");

                entity.Property(e => e.MessageTo)
                    .IsRequired()
                    .HasColumnName("message_to");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("Message_Order_id_order_fk");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("Order_pk")
                    .IsClustered(false);

                entity.ToTable("Order");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.CourierReward)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("courier_reward");

                entity.Property(e => e.DeliveryAddress)
                    .IsRequired()
                    .HasColumnName("delivery_address");

                entity.Property(e => e.IdClient).HasColumnName("id_client");

                entity.Property(e => e.IdCourier).HasColumnName("id_courier");

                entity.Property(e => e.IdOrderStatus).HasColumnName("id_order_status");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.OrderDescription).HasColumnName("order_description");

                entity.Property(e => e.OrderPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("order_price");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdClient)
                    .HasConstraintName("Order_Client_clientId_fk");

                entity.HasOne(d => d.IdCourierNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdCourier)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Order_Courier_courierId_fk");

                entity.HasOne(d => d.IdOrderStatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdOrderStatus)
                    .HasConstraintName("Order_OrderStatus_idOrderStatus_fk");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasKey(e => e.IdOrderStatus)
                    .HasName("OrderStatus_pk")
                    .IsClustered(false);

                entity.ToTable("Order_Status");

                entity.Property(e => e.IdOrderStatus).HasColumnName("id_order_status");

                entity.Property(e => e.OrderStatusName)
                    .IsRequired()
                    .HasColumnName("order_status_name");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("Product_pk")
                    .IsClustered(false);

                entity.ToTable("Product");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.ProductDescription).HasColumnName("product_description");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name");

                entity.Property(e => e.ProductPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("product_price");
            });

            modelBuilder.Entity<ProductBranch>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Product_Branch_pk")
                    .IsClustered(false);

                entity.ToTable("Product_Branch");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdBranch).HasColumnName("id_branch");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.Property(e => e.IsAvailable).HasColumnName("is_available");

                entity.Property(e => e.ProductAmount).HasColumnName("product_amount");

                entity.HasOne(d => d.IdBranchNavigation)
                    .WithMany(p => p.ProductBranches)
                    .HasForeignKey(d => d.IdBranch)
                    .HasConstraintName("Product_Branch_Branch_id_branch_fk");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductBranches)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("Product_Branch_Product_id_product_fk");
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("ProductOrder_pk")
                    .IsClustered(false);

                entity.ToTable("Product_Order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdOrder).HasColumnName("id_order");

                entity.Property(e => e.IdProduct).HasColumnName("id_product");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("Product_Order_Order_id_order_fk");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("Product_Order_Product_id_product_fk");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => e.IdRating)
                    .HasName("Rating_pk")
                    .IsClustered(false);

                entity.ToTable("Rating");

                entity.HasIndex(e => e.IdRating, "Rating_id_rating_uindex")
                    .IsUnique();

                entity.Property(e => e.IdRating)
                    .ValueGeneratedNever()
                    .HasColumnName("id_rating");

                entity.Property(e => e.RatingDescription)
                    .IsRequired()
                    .HasColumnName("rating_description");

                entity.Property(e => e.RatingScore).HasColumnName("rating_score");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
