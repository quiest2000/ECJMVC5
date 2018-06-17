using System.Data.Entity;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models.Domain
{
    public class MainContext : DbContext
    {
        public MainContext() 
            : base("name=OracleDbContext") //oracle database
            //: base("name=MainDbConnection") //sql database
        {
            Database.SetInitializer(new MainContextInitializer());
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<DonHangKH> DonHangKHs { get; set; }
        public virtual DbSet<GiaoDien> GiaoDiens { get; set; }
        public virtual DbSet<HangSanXuat> HangSanXuats { get; set; }
        public virtual DbSet<HopDongNcc> HopDongNccs { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSPs { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<Oauth> Oauths { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
        public virtual DbSet<ThongSoKyThuat> ThongSoKyThuats { get; set; }
        public virtual DbSet<TrackingAction> Trackingactions { get; set; }
        public virtual DbSet<SanPhamCanMua> Sanphamcanmuas { get; set; }
        public virtual DbSet<ConfigAPI> ConfigApIs { get; set; }
        public virtual DbSet<DangKySanPhamNcc> DanhSachDangkySanPhamNccs { get; set; }
    }

    public class MainContextInitializer : CreateDatabaseIfNotExists<MainContext>
    {
        protected override void Seed(MainContext context)
        {
            base.Seed(context);

            //insert sample data
            //[dbo].[AspNetRoles]
        }
    }
}