using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleEFCodeFirst.Repositories;

namespace ConsoleEFCodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin moving data... ");
            Database.SetInitializer(new DropCreateDatabaseAlways<OracleDbContext>());

            using (var oracleContext = new OracleDbContext())
            {
                using (var sqlContext = new SqlContext())
                {
                    Console.WriteLine($"Moving table: {nameof(AspNetRole)}");
                    oracleContext.AspNetRoles.AddRange(sqlContext.AspNetRoles.AsNoTracking().ToList());
                    Console.WriteLine($"Moving table: {nameof(AspNetUser)}");
                    oracleContext.AspNetUsers.AddRange(sqlContext.AspNetUsers.AsNoTracking().ToList());
                    Console.WriteLine($"Moving table: {nameof(ConfigAPI)}");
                    oracleContext.ConfigAPIs.AddRange(sqlContext.ConfigAPIs.AsNoTracking().ToList());
                    Console.WriteLine($"Moving table: {nameof(GiaoDien)}");
                    oracleContext.GiaoDiens.AddRange(sqlContext.GiaoDiens.AsNoTracking().ToList());
                    Console.WriteLine($"Moving table: {nameof(HangSanXuat)}");
                    oracleContext.HangSanXuats.AddRange(sqlContext.HangSanXuats.AsNoTracking().ToList());
                    Console.WriteLine($"Moving table: {nameof(LoaiSP)}");
                    oracleContext.LoaiSPs.AddRange(sqlContext.LoaiSPs.AsNoTracking().ToList());
                    Console.WriteLine($"Moving table: {nameof(NhaCungCap)}");
                    oracleContext.NhaCungCaps.AddRange(sqlContext.NhaCungCaps.AsNoTracking().ToList());
                    Console.WriteLine($"Moving table: {nameof(Trackingaction)}");
                    oracleContext.Trackingactions.AddRange(sqlContext.Trackingactions.AsNoTracking().ToList());
                    Console.WriteLine($"Moving table: {nameof(Link)}");
                    oracleContext.Links.AddRange(sqlContext.Links.AsNoTracking().ToList());
                    Console.WriteLine($"Moving table: {nameof(SanPham)}");
                    oracleContext.SanPhams.AddRange(sqlContext.SanPhams.AsNoTracking().ToList());
                    Console.WriteLine($"Moving table: {nameof(ThongSoKyThuat)}");
                    oracleContext.ThongSoKyThuats.AddRange(sqlContext.ThongSoKyThuats.AsNoTracking().ToList());
                    Console.WriteLine($"Moving table: {nameof(KhuyenMai)}");
                    oracleContext.KhuyenMais.AddRange(sqlContext.KhuyenMais.AsNoTracking().ToList());
                    Console.WriteLine($"Moving table: {nameof(SanPhamKhuyenMai)}");
                    oracleContext.SanPhamKhuyenMais.AddRange(sqlContext.SanPhamKhuyenMais.AsNoTracking().ToList());
                    Console.WriteLine($"Saving changes...");
                    var rows = oracleContext.SaveChanges();
                    Console.WriteLine($"{rows} rows has been move to oracle database");

                    Console.WriteLine($"Update user roles...");
                    var oRoles = oracleContext.AspNetRoles.ToList();
                    var sUsers = sqlContext.AspNetUsers.Include(aa=>aa.AspNetRoles).ToList();
                    var users = oracleContext.AspNetUsers.ToList();
                    users.ForEach(oUser =>
                    {
                        var sUser = sUsers.FirstOrDefault(aa => aa.Id == oUser.Id);
                        var sRoleId = sUser?.AspNetRoles?.FirstOrDefault()?.Id;
                        if(string.IsNullOrEmpty(sRoleId))
                            return;
                        oUser.AspNetRoles.Add(oRoles.FirstOrDefault(aa=>aa.Id.Equals(sRoleId)));
                    });
                    
                    oracleContext.SaveChanges();
                    Console.WriteLine("All done!");
                }
            }

            Console.Write("Press any key to continue... ");
            Console.ReadLine();
        }
    }

    public class SqlContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AspNetUser>()
                .HasMany<AspNetRole>(s => s.AspNetRoles)
                .WithMany(c => c.AspNetUsers)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("RoleId");
                    cs.ToTable("AspNetUserRoles");
                });
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
    public class OracleDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("QUINN2");
            modelBuilder.Entity<AspNetUser>()
                .HasMany<AspNetRole>(s => s.AspNetRoles)
                .WithMany(c => c.AspNetUsers)
                .Map(cs =>
                {
                    cs.MapLeftKey("AspNetUserId");
                    cs.MapRightKey("AspNetRoleId");
                    cs.ToTable("AspNetUserAspNetRoles");
                });
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
