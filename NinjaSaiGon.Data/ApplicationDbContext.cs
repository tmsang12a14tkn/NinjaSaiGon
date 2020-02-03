using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NinjaSaiGon.Data.Models;

namespace NinjaSaiGon.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<DrinkPhoto> DrinkPhotos { get; set; }
        public DbSet<DrinkCategory> DrinkCategories { get; set; }
        public DbSet<CategoryPhoto> DrinkCategoryPhotos { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<ToppingCategory> ToppingCategories { get; set; }
        public DbSet<DrinkTopping> DrinkToppings { get; set; }
        public DbSet<DrinkToppingCategory> DrinkToppingCategories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderSourceType> OrderSourceTypes { get; set; }
        public DbSet<OrderSource> OrderSources { get; set; }
        public DbSet<DeliveryPartner> DeliveryPartners { get; set; }
        public DbSet<OrderDelivery> OrderDeliveries { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderDetailTopping> OrderDetailToppings { get; set; }
        public DbSet<DrinkCategoryType> DrinkCategoryTypes { get; set; }
        public DbSet<DrinkUnit> DrinkUnits { get; set; }
        public DbSet<DrinkSize> DrinkSizes { get; set; }
        public DbSet<IceOption> IceOptions { get; set; }
        public DbSet<SugarOption> SugarOptions { get; set; }
        public DbSet<ToppingPhoto> ToppingPhotos { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionPhoto> PromotionPhotos { get; set; }
        public DbSet<PromotionApplyHour> PromotionApplyHours { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<NotifyPopup> NotifyPopups { get; set; }
        public DbSet<PromotionDrink> PromotionDrinks { get; set; }
        public DbSet<PromotionDrinkTopping> PromotionDrinkToppings { get; set; }
        public DbSet<PromotionDrinkSetting> PromotionDrinkSettings { get; set; }

        public DbSet<PrivatePromotion> PrivatePromotions { get; set; }
        public DbSet<PrivatePromotionPhoto> PrivatePromotionPhotos { get; set; }
        public DbSet<PrivatePromotionCode> PrivatePromotionCodes { get; set; }
        public DbSet<PrivatePromotionDrinkSetting> PrivatePromotionDrinkSettings { get; set; }
        public DbSet<PrivatePromotionDrink> PrivatePromotionDrinks { get; set; }
        public DbSet<PrivatePromotionDrinkTopping> PrivatePromotionDrinkToppings { get; set; }

        public DbSet<FreeDrinkReason> FreeDrinkReasons { get; set; }
        public DbSet<Device> Devices { get; set; }


        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonLevel> PersonLevels { get; set; }
        public DbSet<PersonalName> PersonalNames { get; set; }
        public DbSet<PersonalNameType> PersonalNameTypes { get; set; }
        public DbSet<PersonalPhoto> PersonalPhotos { get; set; }
        public DbSet<PersonalPhotoType> PersonalPhotoTypes { get; set; }
        public DbSet<PersonalDob> PersonalDOBs { get; set; }
        public DbSet<PersonalDobType> PersonalDOBTypes { get; set; }
        public DbSet<PersonalPob> PersonalPOBs { get; set; }
        public DbSet<PersonalHomeTown> PersonalHomeTowns { get; set; }
        public DbSet<Ethnic> Ethnics { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<MaritalStatusType> MaritalStatusTypes { get; set; }
        public DbSet<IDCard> IDCards { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<IDDocument> IDDocuments { get; set; }
        public DbSet<IDDocumentType> IDDocumentTypes { get; set; }
        public DbSet<PersonBank> PersonBanks { get; set; }
        public DbSet<BankAccountType> BankAccountTypes { get; set; }
        public DbSet<BankCard> BankCards { get; set; }
        public DbSet<BankCardType> BankCardTypes { get; set; }
        public DbSet<PersonCustomerWorking> PersonCustomerWorkings { get; set; }
        public DbSet<PersonEmployeeWorking> PersonEmployeeWorkings { get; set; }
        public DbSet<PersonCustomerSourceType> PersonCustomerSourceTypes { get; set; }
        public DbSet<PersonCustomerCare> PersonCustomerCares { get; set; }

        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ContactAddress> ContactAddresses { get; set; }
        public DbSet<ContactEmail> ContactEmails { get; set; }
        public DbSet<ContactPhone> ContactPhones { get; set; }
        public DbSet<ContactSocial> ContactSocials { get; set; }
        public DbSet<ContactOtt> ContactOTTs { get; set; }
        public DbSet<EmailType> EmailTypes { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }
        public DbSet<SocialType> SocialTypes { get; set; }
        public DbSet<OttType> OTTTypes { get; set; }

        public DbSet<VehicleInfo> VehicleInfos { get; set; }
        public DbSet<VehiclePhoto> VehiclePhotos { get; set; }

        public DbSet<Province> Provinces { get; set; }
        public DbSet<DistrictPlace> DistrictPlaces { get; set; }
        public DbSet<Ward> Wards { get; set; }

        public DbSet<Agency> Agencies { get; set; }
        public DbSet<AgencyGroup> AgencyGroups { get; set; }
        public DbSet<AgencyBusiness> AgencyBusinesses { get; set; }
        public DbSet<AgencyContactInfo> AgencyContactInfos { get; set; }
        public DbSet<AgencyContactAddress> AgencyContactAddresses { get; set; }
        public DbSet<AgencyContactEmail> AgencyContactEmails { get; set; }
        public DbSet<AgencyContactPhone> AgencyContactPhones { get; set; }
        public DbSet<AgencyContactSocial> AgencyContactSocials { get; set; }
        public DbSet<AgencyContactOtt> AgencyContactOTTs { get; set; }
        public DbSet<AgencyRepresentative> AgencyRepresentatives { get; set; }
        public DbSet<AgencyBank> AgencyBanks { get; set; }
        public DbSet<AgencyBankCard> AgencyBankCards { get; set; }
        public DbSet<CurrencyType> CurrencyTypes { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PaymentTermType> PaymentTermTypes { get; set; }
        public DbSet<AgencyDiscountType> AgencyDiscountTypes { get; set; }
        public DbSet<AgencyPayment> AgencyPayments { get; set; }
        public DbSet<PickupType> PickupTypes { get; set; }
        public DbSet<AgencyDelivery> AgencyDeliveries { get; set; }
        public DbSet<AgencyDiscountCustomerType> AgencyDiscountCustomerTypes { get; set; }
        public DbSet<AgencyCare> AgencyCares { get; set; }
        

        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentPosition> DepartmentPositions { get; set; }

        public DbSet<BusinessArea> BusinessAreas { get; set; }
        public DbSet<CommissionFormula> CommissionFormulas { get; set; }

        public virtual DbSet<ControllerActionPermission> ControllerActionPermissions { get; set; }
        public virtual DbSet<ControllerAction> ControllerActions { get; set; }


        private readonly string _connectionString;
        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ControllerActionPermission>().HasKey(p => new { p.ControllerActionId, p.RoleId });

            builder.Entity<DrinkTopping>().HasKey(t => new { t.DrinkId, t.ToppingId });
            builder.Entity<DrinkTopping>()
                .HasOne(pt => pt.Drink)
                .WithMany(p => p.DrinkToppings)
                .HasForeignKey(pt => pt.DrinkId);
            builder.Entity<DrinkTopping>()
                .HasOne(pt => pt.Topping)
                .WithMany(t => t.DrinkToppings)
                .HasForeignKey(pt => pt.ToppingId);

            builder.Entity<DrinkToppingCategory>().HasKey(t => new { t.DrinkId, t.ToppingCategoryId });
            builder.Entity<DrinkToppingCategory>()
                .HasOne(dtc => dtc.Drink)
                .WithMany(d => d.DrinkToppingCategories)
                .HasForeignKey(dtc => dtc.DrinkId);
            builder.Entity<DrinkToppingCategory>()
                .HasOne(dtc => dtc.ToppingCategory)
                .WithMany(tc => tc.DrinkToppingCategories)
                .HasForeignKey(dtc => dtc.ToppingCategoryId);


            builder.Entity<OrderDetailTopping>().HasKey(t => new { t.OrderDetailId, t.ToppingId });
            builder.Entity<OrderDetailTopping>()
               .HasOne(pt => pt.OrderDetail)
               .WithMany(p => p.OrderDetailToppings)
               .HasForeignKey(pt => pt.OrderDetailId);

            builder.Entity<AgencyRepresentative>()
               .HasKey(ar => new { ar.PersonId, ar.AgencyId });
            builder.Entity<PersonalDob>()
               .HasKey(ar => new { ar.PersonId, ar.TypeId });
            builder.Entity<PersonalNationality>()
               .HasKey(ar => new { ar.PersonId, ar.NationalityId });
            builder.Entity<PersonCustomerCare>()
                .HasOne(m => m.Person)
                .WithMany(t => t.CustomerRepresentativeCares)
                .HasForeignKey(m => m.PersonId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PersonCustomerCare>()
                .HasOne(m => m.Employee)
                .WithMany(t => t.EmployeeCares)
                .HasForeignKey(m => m.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Person>()
                .HasOne(a => a.User)
                .WithOne(b => b.Person)
                .HasForeignKey<ApplicationUser>(b => b.PersonId);
        }
    }


}
