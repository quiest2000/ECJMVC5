using System.Data.Entity;

namespace EC_TH2012_J.Models
{
    public class Entities:DbContext
    {
        public Entities():base("name=Entities")
        {
            Database.CreateIfNotExists();
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
        public virtual DbSet<HopDongNCC> HopDongNCCs { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<LoaiSP> LoaiSPs { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<Oauth> Oauths { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
        public virtual DbSet<ThongSoKyThuat> ThongSoKyThuats { get; set; }
        public virtual DbSet<Trackingaction> Trackingactions { get; set; }
        public virtual DbSet<Sanphamcanmua> Sanphamcanmuas { get; set; }
        public virtual DbSet<ConfigAPI> ConfigAPIs { get; set; }
        public virtual DbSet<DanhsachdangkisanphamNCC> DanhsachdangkisanphamNCCs { get; set; }
    }
}