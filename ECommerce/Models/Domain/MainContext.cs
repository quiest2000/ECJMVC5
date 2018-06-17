using System.Collections.Generic;
using System.Data.Entity;
using ECommerce.Models.Constants;
using ECommerce.Models.Domain.EfModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
        protected override async void Seed(MainContext context)
        {
            base.Seed(context);
            const string defaultPass = "123456";
            //insert sample data
            //[dbo].[AspNetRoles]

            var roles = new List<AspNetRole>
            {
                new AspNetRole{Name = RoleNames.Deactivated},
                new AspNetRole{Name = RoleNames.Customer},
                new AspNetRole{Name =RoleNames.Vendor},
                new AspNetRole{Name = RoleNames.Employee},
                new AspNetRole{Name = RoleNames.Administrator},
            };
            context.AspNetRoles.AddRange(roles);
            await context.SaveChangesAsync();
            //AspNetUsers admin

            var adminUser = new ApplicationUser
            {
                Email = "admin@gmail.com",
                DiaChi = "TPHCM",
                EmailConfirmed = true,
                HoTen = "Nguyễn Thanh Tâm",
            };
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            await userManager.CreateAsync(new ApplicationUser(), defaultPass);
            await userManager.AddToRoleAsync(adminUser.Id, RoleNames.Administrator);
            //GiaoDien
            var giaoDiens = new List<GiaoDien>
            {
                new GiaoDien
                {
                    ThuocTinh = "Logo",
                    GiaTri = "logo.svg"
                },
                new GiaoDien
                {
                    ThuocTinh = "WebsiteName",
                    GiaTri = "Website TMĐT"
                },
                new GiaoDien
                {
                    ThuocTinh = "FooterText",
                },
                new GiaoDien
                {
                    ThuocTinh = "Facebook",
                    GiaTri = "https://www.facebook.com/nguyenngocqui88"
                },
                new GiaoDien
                {
                    ThuocTinh = "Pinterest",
                },
                new GiaoDien
                {
                    ThuocTinh = "Twitter",
                },
            };
            context.GiaoDiens.AddRange(giaoDiens);
            await context.SaveChangesAsync();
            //HangSx
            var manufacturers = new List<HangSanXuat>
            {
                new HangSanXuat{TenHang = "Apple",TruSoChinh = "Cupertino, California, Mỹ",QuocGia = "Mỹ"},
                new HangSanXuat {TenHang = "ASUS", TruSoChinh = "Đài Bắc, Đài Loan", QuocGia = "Đài Loan"},
                new HangSanXuat {TenHang = "Kingston", TruSoChinh = "Fountain Valley, California, Mỹ", QuocGia = "Mỹ"},
                new HangSanXuat {TenHang = "Nokia", TruSoChinh = "Keilaniemi, Espoo, Phần Lan", QuocGia = "Phần Lan"},
                new HangSanXuat {TenHang = "Unknow", TruSoChinh = "", QuocGia = ""},
                new HangSanXuat
                {
                    TenHang = "OPPO",
                    TruSoChinh = "Đông Hoản, Quảng Đông, Trung Quốc",
                    QuocGia = "Trung Quốc"
                },
                new HangSanXuat {TenHang = "SamSung", TruSoChinh = "", QuocGia = "Hàn Quốc"},
                new HangSanXuat {TenHang = "Sony", TruSoChinh = "", QuocGia = "Nhật Bản"},
                new HangSanXuat {TenHang = "Toshiba", TruSoChinh = "", QuocGia = "Nhật Bản"},
                new HangSanXuat {TenHang = "Vivo", TruSoChinh = "", QuocGia = "Trung Quốc"},
            };
            context.HangSanXuats.AddRange(manufacturers);
            await context.SaveChangesAsync();
            //KhuyenMai - sanphamKhuyenMai
            //link
            //LoaiSP
            var loaiSps = new List<LoaiSanPham>
            {
                new LoaiSanPham{TenLoai = "Bao da, ốp lưng"},
                new LoaiSanPham{TenLoai = "Điện thoại di động"},
                new LoaiSanPham{TenLoai = "Sạc pin"},
                new LoaiSanPham{TenLoai = "Tai nghe"},
                new LoaiSanPham{TenLoai = "Thẻ nhớ"},
                new LoaiSanPham{TenLoai = "Khác"},
            };
            context.LoaiSPs.AddRange(loaiSps);
            await context.SaveChangesAsync();

            //NCC
            var nccs = new List<NhaCungCap>
            {
                new NhaCungCap{TenNCC = "Nhà cung cấp 1"},
                new NhaCungCap{TenNCC = "Nhà cung cấp 2"},
                new NhaCungCap{TenNCC = "Cửa hàng điện thoại"},
                new NhaCungCap{TenNCC = "Cửa hàng phụ kiện Kiến Hưng"},
                new NhaCungCap{TenNCC = "Công ty TNHH Thương mại FPT"},
                new NhaCungCap{TenNCC = "Hàng chính hiệu "},
                new NhaCungCap{TenNCC = ""},
            };
            context.NhaCungCaps.AddRange(nccs);
            await context.SaveChangesAsync();

            //SP
            var sanphams = new List<SanPham>
            {
                new SanPham
                {
                    TenSP = "Điện thoại Oppo R7YGP (Vàng hồng)",
                    LoaiSpId = 2,
                    HangSxId = 6,
                    XuatXu = "Trung Quốc",
                    GiaGoc = 9400000,
                    GiaTien = 9400000,
                    MoTa = "",
                    AnhDaiDien = "016131.jpg",
                    AnhNen = "016132.jpg",
                    AnhKhac = "016133.jpg",
                    SoLuong = 23,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại Samsung G530 (Xám)",
                    LoaiSpId = 2,
                    HangSxId = 7,
                    XuatXu = "Việt Nam",
                    GiaGoc = 3500000,
                    GiaTien = 3150000,
                    MoTa = "",
                    AnhDaiDien = "021151.jpg",
                    AnhNen = "021152.jpg",
                    AnhKhac = "021153.jpg",
                    SoLuong = 40,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại Oppo A51WW (Trắng)",
                    LoaiSpId = 2,
                    HangSxId = 6,
                    XuatXu = "Trung Quốc",
                    GiaGoc = 5000000,
                    GiaTien = 5000000,
                    MoTa = "",
                    AnhDaiDien = "148841.jpg",
                    AnhNen = "148842.jpg",
                    AnhKhac = "148843.jpg",
                    SoLuong = 32,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại Samsung SM-G318H (Đen)",
                    LoaiSpId = 2,
                    HangSxId = 7,
                    XuatXu = "Việt Nam",
                    GiaGoc = 1700000,
                    GiaTien = 1700000,
                    MoTa = "",
                    AnhDaiDien = "236051.jpg",
                    AnhNen = "236052.jpg",
                    AnhKhac = "236053.jpg",
                    SoLuong = 20,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "điện thoại 2",
                    LoaiSpId = 2,
                    HangSxId = 6,
                    XuatXu = "a",
                    GiaGoc = 3000000,
                    GiaTien = 3000000,
                    MoTa = "<p>aaa</p>  ",
                    AnhDaiDien = "236411.jpg",
                    AnhNen = "236412.jpg",
                    AnhKhac = "236413.jpg",
                    SoLuong = 0,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại Nokia 630 (Yellow)",
                    LoaiSpId = 2,
                    HangSxId = 4,
                    XuatXu = "Việt Nam",
                    GiaGoc = 2670000,
                    GiaTien = 2670000,
                    MoTa = "",
                    AnhDaiDien = "241731.jpg",
                    AnhNen = "241732.jpg",
                    AnhKhac = "241733.jpg",
                    SoLuong = 40,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại Nokia 930 (Orange)",
                    LoaiSpId = 2,
                    HangSxId = 4,
                    XuatXu = "Việt Nam",
                    GiaGoc = 9500000,
                    GiaTien = 9500000,
                    MoTa = "",
                    AnhDaiDien = "270031.jpg",
                    AnhNen = "270032.jpg",
                    AnhKhac = "270033.jpg",
                    SoLuong = 23,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại Nokia 532 (Xanh lá)",
                    LoaiSpId = 2,
                    HangSxId = 4,
                    XuatXu = "Trung Quốc",
                    GiaGoc = 1600000,
                    GiaTien = 1600000,
                    MoTa = "",
                    AnhDaiDien = "330581.jpg",
                    AnhNen = "330582.jpg",
                    AnhKhac = "330583.jpg",
                    SoLuong = 40,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại iPhone 6S 16GB (Bạc)",
                    LoaiSpId = 2,
                    HangSxId = 1,
                    XuatXu = "Mỹ",
                    GiaGoc = 18000000,
                    GiaTien = 18000000,
                    MoTa = "",
                    AnhDaiDien = "351221.jpg",
                    AnhNen = "351222.jpg",
                    AnhKhac = "351223.jpg",
                    SoLuong = 9,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại Nokia 105 ( Cyan)",
                    LoaiSpId = 2,
                    HangSxId = 4,
                    XuatXu = "Trung Quốc",
                    GiaGoc = 400000,
                    GiaTien = 400000,
                    MoTa = "",
                    AnhDaiDien = "357541.jpg",
                    AnhNen = "357542.jpg",
                    AnhKhac = "357543.jpg",
                    SoLuong = 20,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại iPhone 6S Plus 128GB (Vàng)",
                    LoaiSpId = 2,
                    HangSxId = 1,
                    XuatXu = "Mỹ",
                    GiaGoc = 24400000,
                    GiaTien = 24400000,
                    MoTa = "",
                    AnhDaiDien = "477101.jpg",
                    AnhNen = "477102.jpg",
                    AnhKhac = "477103.jpg",
                    SoLuong = 2,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại iPhone 6S 64GB (Vàng)",
                    LoaiSpId = 2,
                    HangSxId = 1,
                    XuatXu = "Mỹ",
                    GiaGoc = 21000000,
                    GiaTien = 21000000,
                    MoTa = "",
                    AnhDaiDien = "560231.jpg",
                    AnhNen = "560232.jpg",
                    AnhKhac = "560233.jpg",
                    SoLuong = 6,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại iPhone 6 16GB (Xám)",
                    LoaiSpId = 2,
                    HangSxId = 1,
                    XuatXu = "Mỹ",
                    GiaGoc = 10000000,
                    GiaTien = 10000000,
                    MoTa = "",
                    AnhDaiDien = "680101.jpg",
                    AnhNen = "680102.jpg",
                    AnhKhac = "680103.jpg",
                    SoLuong = 13,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại iPhone 6S 16GB (Vàng Hồng)",
                    LoaiSpId = 2,
                    HangSxId = 1,
                    XuatXu = "Mỹ",
                    GiaGoc = 18000000,
                    GiaTien = 18000000,
                    MoTa = "",
                    AnhDaiDien = "771281.jpg",
                    AnhNen = "771282.jpg",
                    AnhKhac = "771283.jpg",
                    SoLuong = 5,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện Thoại Samsung SM- J500H (Vàng)",
                    LoaiSpId = 2,
                    HangSxId = 7,
                    XuatXu = "Hàn Quốc",
                    GiaGoc = 4750000,
                    GiaTien = 4750000,
                    MoTa = "",
                    AnhDaiDien = "813571.jpg",
                    AnhNen = "813572.jpg",
                    AnhKhac = "813573.jpg",
                    SoLuong = 12,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại Oppo R7 lite- R7KFS (Bạc)",
                    LoaiSpId = 2,
                    HangSxId = 6,
                    XuatXu = "Trung Quốc",
                    GiaGoc = 9200000,
                    GiaTien = 9200000,
                    MoTa = "",
                    AnhDaiDien = "824321.jpg",
                    AnhNen = "824322.jpg",
                    AnhKhac = "824323.jpg",
                    SoLuong = 20,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại Asus Zenfone 2 (Vàng)",
                    LoaiSpId = 2,
                    HangSxId = 2,
                    XuatXu = "Đài Loan",
                    GiaGoc = 6900000,
                    GiaTien = 6900000,
                    MoTa = "",
                    AnhDaiDien = "837461.jpg",
                    AnhNen = "837462.jpg",
                    AnhKhac = "837463.jpg",
                    SoLuong = 9,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại Asus Z007 Zenfone (Trắng)",
                    LoaiSpId = 2,
                    HangSxId = 2,
                    XuatXu = "Đài Loan",
                    GiaGoc = 1990000,
                    GiaTien = 1990000,
                    MoTa = "",
                    AnhDaiDien = "876321.jpg",
                    AnhNen = "876322.jpg",
                    AnhKhac = "876323.jpg",
                    SoLuong = 13,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Bao da đeo lưng iPhone 3GS",
                    LoaiSpId = 1,
                    HangSxId = 7,
                    XuatXu = "Trung Quốc",
                    GiaGoc = 40000,
                    GiaTien = 40000,
                    MoTa = "<p>Chất liệu: Da, / Mầu sắc: Đen / Kiểu đặt điện thoại: T&uacute;i đeo</p>  ",
                    AnhDaiDien = "BD001.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 38,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Bao da ĐTDĐ sọc dọc",
                    LoaiSpId = 1,
                    HangSxId = 10,
                    XuatXu = "Việt Nam",
                    GiaGoc = 20000,
                    GiaTien = 20000,
                    MoTa = "<p>M&agrave;u sắc: x&aacute;m, Chất liệu: da. K&iacute;ch thước: 15 x 7 cm.</p>  ",
                    AnhDaiDien = "BD002.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 92,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Bao da ĐTDĐ hình con gấu trúc",
                    LoaiSpId = 1,
                    HangSxId = 10,
                    XuatXu = "Việt Nam",
                    GiaGoc = 65000,
                    GiaTien = 65000,
                    MoTa = "Màu sắc: trắng + đen. Chất liệu: da. Kích thước: 15 x 7 cm. ",
                    AnhDaiDien = "BD003.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 37,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Điện thoại Nokia Lumia 830",
                    LoaiSpId = 2,
                    HangSxId = 4,
                    XuatXu = "Mỹ",
                    GiaGoc = 5550000,
                    GiaTien = 5272500,
                    MoTa =
                        "<p>Nokia Lumia 830 sở hữu bộ khung viền bằng nh&ocirc;m với k&iacute;nh cường lực mặt trước, v&agrave; chất liệu nhựa ở mặt sau, c&aacute;c g&oacute;c được bo nhẹ nhưng tổng thể vẫn l&agrave; sự vu&ocirc;ng vức chắc chắn đầy nam t&iacute;nh.</p>  ",
                    AnhDaiDien = "DT0011.jpg",
                    AnhNen = "DT0012.jpg",
                    AnhKhac = "DT0013.jpg",
                    SoLuong = 7,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Ốp lưng ĐTDĐ hình con mèo",
                    LoaiSpId = 1,
                    HangSxId = 10,
                    XuatXu = "Việt Nam",
                    GiaGoc = 50000,
                    GiaTien = 50000,
                    MoTa = "Màu sắc: cam. Chất liệu: nhựa dẻo. Kích thước: 15 x 7 cm.",
                    AnhDaiDien = "OL001.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 3,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Ốp lưng ĐTDĐ sọc carô",
                    LoaiSpId = 1,
                    HangSxId = 10,
                    XuatXu = "Việt Nam",
                    GiaGoc = 30000,
                    GiaTien = 30000,
                    MoTa = "Màu sắc: xanh dương. Chất liệu: nhựa dẻo. Kích thước: 10 x 6cm.",
                    AnhDaiDien = "OL002.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 60,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Ốp lưng ĐTDĐ họa tiết cây dừa",
                    LoaiSpId = 1,
                    HangSxId = 10,
                    XuatXu = "Việt Nam",
                    GiaGoc = 30000,
                    GiaTien = 30000,
                    MoTa = "Màu sắc: nhiều màu. Chất liệu: nhựa dẻo. Kích thước: 10 x 6cm.",
                    AnhDaiDien = "OL003.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 59,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Sạc pin ĐTDĐ SamSung",
                    LoaiSpId = 3,
                    HangSxId = 7,
                    XuatXu = "Hàn Quốc",
                    GiaGoc = 165000,
                    GiaTien = 165000,
                    MoTa = "Dung lượng pin: 10000 mAh. Điện áp đầu vào/ra: 5V - 2A.",
                    AnhDaiDien = "SP001.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 100,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Sạc pin ĐTDĐ Oppo",
                    LoaiSpId = 3,
                    HangSxId = 6,
                    XuatXu = "Trung Quốc",
                    GiaGoc = 350000,
                    GiaTien = 350000,
                    MoTa = "Dung lượng pin: 10400 mAh. Điện áp đầu vào/ra: 5V - 2A.",
                    AnhDaiDien = "SP002.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 17,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Sạc pin ĐTDĐ Asus",
                    LoaiSpId = 3,
                    HangSxId = 2,
                    XuatXu = "Đài Loan",
                    GiaGoc = 115000,
                    GiaTien = 115000,
                    MoTa = "Dung lượng pin: 10000 mAh. Điện áp đầu vào/ra: 5V - 1A.",
                    AnhDaiDien = "SP003.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 32,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Sạc pin ĐTDĐ 1le",
                    LoaiSpId = 3,
                    HangSxId = 1,
                    XuatXu = "Mỹ",
                    GiaGoc = 350000,
                    GiaTien = 350000,
                    MoTa = "Dung lượng pin: 10200 mAh. Điện áp đầu vào/ra: 4V - 1A.",
                    AnhDaiDien = "SP004.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 39,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Củ sạc Sony (Đen)",
                    LoaiSpId = 3,
                    HangSxId = 8,
                    XuatXu = "Nhật Bản",
                    GiaGoc = 151000,
                    GiaTien = 151000,
                    MoTa = "Bảo hành 1 tháng bằng hóa đơn",
                    AnhDaiDien = "SP005.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 101,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Củ sạc SamSung Galaxy Note 2",
                    LoaiSpId = 3,
                    HangSxId = 7,
                    XuatXu = "Trung Quốc",
                    GiaGoc = 130000,
                    GiaTien = 130000,
                    MoTa = "Dùng cho loại máy: Samsung  Dòng ra: 2000 mAh",
                    AnhDaiDien = "SP006.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 100,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Tai nghe bluetooth China N7000 (Đen)",
                    LoaiSpId = 4,
                    HangSxId = 7,
                    XuatXu = "Trung Quốc",
                    GiaGoc = 280000,
                    GiaTien = 280000,
                    MoTa =
                        "Tính năng: Nghe nhạc MP3, Nghe đài FM, Kết nối nhiều thiết bị Bluetooth, Thích hợp dùng trên xe hơi  Màu sắc: Xám bạc",
                    AnhDaiDien = "TA001.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 50,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Tai nghe Skullcandy Ink'd 2.0 (Đen)",
                    LoaiSpId = 4,
                    HangSxId = 6,
                    XuatXu = "Trung Quốc",
                    GiaGoc = 100000,
                    GiaTien = 100000,
                    MoTa = "Màu sắc: đen + đỏ. Chất liệu: nhựa. Kích thước: dây dài 1m.",
                    AnhDaiDien = "TA002.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 60,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Tai nghe Joinhandmade Jelly Galaxy (Xanh)",
                    LoaiSpId = 4,
                    HangSxId = 7,
                    XuatXu = "Hàn Quốc",
                    GiaGoc = 1100000,
                    GiaTien = 1100000,
                    MoTa =
                        "Màu sắc: trắng / đỏ / đen. Chất liệu: nhựa. Kích thước: dây dài 1.3m. Trang bị: microphone.",
                    AnhDaiDien = "TA003.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 11,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Tai nghe ĐTDĐ Nokia",
                    LoaiSpId = 4,
                    HangSxId = 4,
                    XuatXu = "Phần Lan",
                    GiaGoc = 220000,
                    GiaTien = 220000,
                    MoTa = "Màu sắc: đen. Chất liệu: nhựa. Kích thước: dây dài 1.2m. Trang bị: microphone.",
                    AnhDaiDien = "TA004.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 19,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Tai nghe ĐTDĐ 1le",
                    LoaiSpId = 4,
                    HangSxId = 1,
                    XuatXu = "Mỹ",
                    GiaGoc = 450000,
                    GiaTien = 450000,
                    MoTa =
                        "Màu sắc: trắng / hồng / đen. Chất liệu: nhựa. Kích thước: dây dài 1.2m. Trang bị: microphone.",
                    AnhDaiDien = "TA005.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 77,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Thẻ nhớ MicroSD Kingston Class 10 8GB",
                    LoaiSpId = 5,
                    HangSxId = 3,
                    XuatXu = "Mỹ",
                    GiaGoc = 125000,
                    GiaTien = 125000,
                    MoTa =
                        "Dung lượng: 8GB  Được chuẩn hóa tích hợp với các tiêu chuẩn về thông số kĩ thuật của thẻ SD  Đa năng  khi kết hợp với bộ tương thích sẵn có, có thể sử dụng như thể SD toàn phần",
                    AnhDaiDien = "TN001.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 78,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Thẻ nhớ 16GB Sandisk Ultra Micro SD Card",
                    LoaiSpId = 5,
                    HangSxId = 6,
                    XuatXu = "Trung Quốc",
                    GiaGoc = 300000,
                    GiaTien = 300000,
                    MoTa = "Dung lượng: 16GB",
                    AnhDaiDien = "TN002.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 98,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Thẻ nhớ Adata MicroSDHC Class 10 32GB",
                    LoaiSpId = 5,
                    HangSxId = 2,
                    XuatXu = "Đài Loan",
                    GiaGoc = 600000,
                    GiaTien = 600000,
                    MoTa = "Thẻ nhớ Adata MicroSD 8GB Class 4",
                    AnhDaiDien = "TN003.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 25,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Thẻ nhớ Transcend microSDHC 16GB Class 10",
                    LoaiSpId = 5,
                    HangSxId = 6,
                    XuatXu = "Trung Quốc",
                    GiaGoc = 500000,
                    GiaTien = 500000,
                    MoTa = "Thẻ nhớ Transcend Micro SDHC Class 4 8GB",
                    AnhDaiDien = "TN004.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 23,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Đầu đọc thẻ nhớ Card reader Kingston G4 USB 3.0",
                    LoaiSpId = 5,
                    HangSxId = 3,
                    XuatXu = "Mỹ",
                    GiaGoc = 300000,
                    GiaTien = 300000,
                    MoTa = "Thẻ nhớ Micro SD Kingston 16GB Class 10",
                    AnhDaiDien = "TN005.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 102,
                    isnew = false,
                    ishot = false
                },
                new SanPham
                {
                    TenSP = "Toshiba SDHC FlashAir Wi-Fi (Class 10) 16GB",
                    LoaiSpId = 5,
                    HangSxId = 9,
                    XuatXu = "Hàn Quốc",
                    GiaGoc = 50000,
                    GiaTien = 50000,
                    MoTa = "Loại card: SD High Capacity (SDHC)  Tốc độ đọc: 10MB/s",
                    AnhDaiDien = "TN006.jpg",
                    AnhNen = "",
                    AnhKhac = "",
                    SoLuong = 5,
                    isnew = false,
                    ishot = false
                },
            };
            context.SanPhams.AddRange(sanphams);
            await context.SaveChangesAsync();
            //thongsokythuat
            var tskt = new List<ThongSoKyThuat>
            {
                new ThongSoKyThuat {SanPhamId = 1, ThuocTinh = "Bộ nhớ trong", GiaTri = "32G"},
                new ThongSoKyThuat {SanPhamId = 1, ThuocTinh = "Camera", GiaTri = "13 MP"},
                new ThongSoKyThuat {SanPhamId = 1, ThuocTinh = "Độ phân giải", GiaTri = "1080 x 1920 Pixels"},
                new ThongSoKyThuat {SanPhamId = 1, ThuocTinh = "Dung lượng pin", GiaTri = "3070mAh"},
                new ThongSoKyThuat {SanPhamId = 1, ThuocTinh = "Hệ điều hành", GiaTri = "Android 5.1"},
                new ThongSoKyThuat {SanPhamId = 1, ThuocTinh = "Kích thước", GiaTri = "151.8 x 75.4 x 7 mm"},
                new ThongSoKyThuat {SanPhamId = 1, ThuocTinh = "Màn hình", GiaTri = "5.5\""},
                new ThongSoKyThuat {SanPhamId = 1, ThuocTinh = "Ram", GiaTri = "4G"},
                new ThongSoKyThuat {SanPhamId = 1, ThuocTinh = "Trọng lượng", GiaTri = "155g"},
                new ThongSoKyThuat {SanPhamId = 2, ThuocTinh = "Camera", GiaTri = "8.0 MP"},
                new ThongSoKyThuat {SanPhamId = 2, ThuocTinh = "Dung lượng pin", GiaTri = "2600mAh"},
                new ThongSoKyThuat {SanPhamId = 2, ThuocTinh = "Hệ điều hành", GiaTri = "Android 4.4"},
                new ThongSoKyThuat {SanPhamId = 2, ThuocTinh = "Kích thước", GiaTri = "144.8 x 72.1 x 8.6mm"},
                new ThongSoKyThuat {SanPhamId = 2, ThuocTinh = "Màn hình", GiaTri = "5\""},
                new ThongSoKyThuat {SanPhamId = 2, ThuocTinh = "Ram", GiaTri = "1G"},
                new ThongSoKyThuat {SanPhamId = 2, ThuocTinh = "Trọng lượng", GiaTri = "143g"},
                new ThongSoKyThuat {SanPhamId = 3, ThuocTinh = "Camera", GiaTri = "8.0 MP"},
                new ThongSoKyThuat {SanPhamId = 3, ThuocTinh = "Độ phân giải", GiaTri = "540 x 960 pixels"},
                new ThongSoKyThuat {SanPhamId = 3, ThuocTinh = "Dung lượng pin", GiaTri = "2420mAh"},
                new ThongSoKyThuat {SanPhamId = 3, ThuocTinh = "Hệ điều hành", GiaTri = "Android 5.0"},
                new ThongSoKyThuat {SanPhamId = 3, ThuocTinh = "Kích thước", GiaTri = "143.4mm x 71.2mm x7.65mm"},
                new ThongSoKyThuat {SanPhamId = 3, ThuocTinh = "Màn hình", GiaTri = "5\""},
                new ThongSoKyThuat {SanPhamId = 3, ThuocTinh = "Ram", GiaTri = "2G"},
                new ThongSoKyThuat {SanPhamId = 3, ThuocTinh = "Trọng lượng", GiaTri = "155g"},
                new ThongSoKyThuat {SanPhamId = 4, ThuocTinh = "Camera", GiaTri = "3.0 MP"},
                new ThongSoKyThuat {SanPhamId = 4, ThuocTinh = "Dung lượng pin", GiaTri = "1500 mAh"},
                new ThongSoKyThuat {SanPhamId = 4, ThuocTinh = "Hệ điều hành", GiaTri = "Android 4.4"},
                new ThongSoKyThuat {SanPhamId = 4, ThuocTinh = "Kích thước", GiaTri = "121.2 x 62.7 x 10.65 mm"},
                new ThongSoKyThuat {SanPhamId = 4, ThuocTinh = "Màn", GiaTri = "4\""},
                new ThongSoKyThuat {SanPhamId = 4, ThuocTinh = "Ram", GiaTri = "512 MB"},
                new ThongSoKyThuat {SanPhamId = 4, ThuocTinh = "Trọng lượng", GiaTri = "100g"},
                new ThongSoKyThuat {SanPhamId = 5, ThuocTinh = "Thông số 1", GiaTri = "AAA"},
                new ThongSoKyThuat {SanPhamId = 5, ThuocTinh = "Thông số 2", GiaTri = "BBB"},
                new ThongSoKyThuat {SanPhamId = 6, ThuocTinh = "Bộ nhớ trong", GiaTri = "8G"},
                new ThongSoKyThuat {SanPhamId = 6, ThuocTinh = "Độ phân giải", GiaTri = "480 x 854 Pixels"},
                new ThongSoKyThuat {SanPhamId = 6, ThuocTinh = "Dung lượng pin", GiaTri = "1830mAh"},
                new ThongSoKyThuat {SanPhamId = 6, ThuocTinh = "Hệ điều hành", GiaTri = "Windows Phone 8.1"},
                new ThongSoKyThuat {SanPhamId = 6, ThuocTinh = "Kích thước", GiaTri = "129.5 x 66.7 x 9.2 mm"},
                new ThongSoKyThuat {SanPhamId = 6, ThuocTinh = "Màn hình", GiaTri = "4.5\""},
                new ThongSoKyThuat {SanPhamId = 6, ThuocTinh = "Ram", GiaTri = "512M"},
                new ThongSoKyThuat {SanPhamId = 6, ThuocTinh = "Thẻ nhớ ngoài", GiaTri = "128G"},
                new ThongSoKyThuat {SanPhamId = 6, ThuocTinh = "Trọng lượng", GiaTri = "134g"},
                new ThongSoKyThuat {SanPhamId = 7, ThuocTinh = "Camera", GiaTri = "10 MP"},
                new ThongSoKyThuat {SanPhamId = 7, ThuocTinh = "Chip đồ họa (GPU)", GiaTri = "Adreno 330"},
                new ThongSoKyThuat {SanPhamId = 7, ThuocTinh = "Độ phân giải", GiaTri = "1080 x 1920 pixels"},
                new ThongSoKyThuat {SanPhamId = 7, ThuocTinh = "Dung lượng pin", GiaTri = "2420mAh"},
                new ThongSoKyThuat {SanPhamId = 7, ThuocTinh = "Hệ điều hành", GiaTri = "Windows Phone 8.1"},
                new ThongSoKyThuat {SanPhamId = 7, ThuocTinh = "Kích thước", GiaTri = "137 x 71 x 9.8 mm"},
                new ThongSoKyThuat {SanPhamId = 7, ThuocTinh = "Loại sim", GiaTri = "Nano sim"},
                new ThongSoKyThuat {SanPhamId = 7, ThuocTinh = "Màn hình", GiaTri = "5\""},
                new ThongSoKyThuat {SanPhamId = 7, ThuocTinh = "Ram", GiaTri = "2G"},
                new ThongSoKyThuat {SanPhamId = 7, ThuocTinh = "Tốc độ CPU", GiaTri = "2.2GHz"},
                new ThongSoKyThuat {SanPhamId = 7, ThuocTinh = "Trọng lượng", GiaTri = "167g"},
                new ThongSoKyThuat {SanPhamId = 8, ThuocTinh = "Bộ nhớ trong", GiaTri = "8G"},
                new ThongSoKyThuat {SanPhamId = 8, ThuocTinh = "Camera", GiaTri = "5.0 MP"},
                new ThongSoKyThuat {SanPhamId = 8, ThuocTinh = "Độ phân giải", GiaTri = "480 x 800 pixels"},
                new ThongSoKyThuat {SanPhamId = 8, ThuocTinh = "Dung lượng pin", GiaTri = "1560mAh"},
                new ThongSoKyThuat {SanPhamId = 8, ThuocTinh = "Kích thước", GiaTri = "118.9 x 65.5 x 11.6 mm"},
                new ThongSoKyThuat {SanPhamId = 8, ThuocTinh = "Màn hình", GiaTri = "4\""},
                new ThongSoKyThuat {SanPhamId = 8, ThuocTinh = "Thẻ nhớ ngoài", GiaTri = "128G"},
                new ThongSoKyThuat {SanPhamId = 8, ThuocTinh = "Trọng lượng", GiaTri = "136.3g"},
                new ThongSoKyThuat {SanPhamId = 9, ThuocTinh = "Bộ nhớ trong", GiaTri = "16G"},
                new ThongSoKyThuat {SanPhamId = 9, ThuocTinh = "Camera", GiaTri = "12.0 MP"},
                new ThongSoKyThuat {SanPhamId = 9, ThuocTinh = "Độ phân giải màn hình", GiaTri = "1334 x 750 pixels"},
                new ThongSoKyThuat {SanPhamId = 9, ThuocTinh = "Hệ điều hành", GiaTri = "IOS 9"},
                new ThongSoKyThuat {SanPhamId = 9, ThuocTinh = "Kích thước", GiaTri = "138.3 x 67.1 x 7.1 mm"},
                new ThongSoKyThuat {SanPhamId = 9, ThuocTinh = "Màn hình", GiaTri = "4.7\""},
                new ThongSoKyThuat {SanPhamId = 9, ThuocTinh = "Trọng lượng", GiaTri = "143g"},
                new ThongSoKyThuat {SanPhamId = 10, ThuocTinh = "Bộ nhớ trong", GiaTri = "8MB"},
                new ThongSoKyThuat {SanPhamId = 10, ThuocTinh = "Độ phân giải", GiaTri = "128 x 128 pixels"},
                new ThongSoKyThuat {SanPhamId = 10, ThuocTinh = "Dung lượng pin", GiaTri = "800 mAh"},
                new ThongSoKyThuat {SanPhamId = 10, ThuocTinh = "Kích thước", GiaTri = "107 x 44.8 x 14.3 mm"},
                new ThongSoKyThuat {SanPhamId = 10, ThuocTinh = "Màn hình", GiaTri = "1.4\""},
                new ThongSoKyThuat {SanPhamId = 10, ThuocTinh = "Trọng lượng", GiaTri = "70g"},
                new ThongSoKyThuat {SanPhamId = 11, ThuocTinh = "Bộ nhớ trong", GiaTri = "128G"},
                new ThongSoKyThuat {SanPhamId = 11, ThuocTinh = "Camera", GiaTri = "12.0 MP"},
                new ThongSoKyThuat {SanPhamId = 11, ThuocTinh = "Độ phân giải", GiaTri = "1080 x 1920"},
                new ThongSoKyThuat {SanPhamId = 11, ThuocTinh = "Hệ điều hành", GiaTri = "IOS 9"},
                new ThongSoKyThuat {SanPhamId = 11, ThuocTinh = "Kết nối", GiaTri = "Băng tần 2G, 3G, 4G"},
                new ThongSoKyThuat {SanPhamId = 11, ThuocTinh = "Kích thước", GiaTri = "158.2 x 77.9 x 7.3 mm"},
                new ThongSoKyThuat {SanPhamId = 11, ThuocTinh = "Màn hình", GiaTri = "5.5\""},
                new ThongSoKyThuat {SanPhamId = 11, ThuocTinh = "RAM", GiaTri = "2G"},
                new ThongSoKyThuat {SanPhamId = 11, ThuocTinh = "Trọng lượng", GiaTri = "192g"},
                new ThongSoKyThuat {SanPhamId = 12, ThuocTinh = "Bộ nhớ trong", GiaTri = "16G"},
                new ThongSoKyThuat {SanPhamId = 12, ThuocTinh = "Camera", GiaTri = "12.0 MP"},
                new ThongSoKyThuat {SanPhamId = 12, ThuocTinh = "Chipset", GiaTri = "A9"},
                new ThongSoKyThuat {SanPhamId = 12, ThuocTinh = "Kết nối", GiaTri = "Băng tần 2G, 3G, 4G"},
                new ThongSoKyThuat {SanPhamId = 12, ThuocTinh = "Kích thước", GiaTri = "138.3 x 67.1 x 7.1 mm"},
                new ThongSoKyThuat {SanPhamId = 12, ThuocTinh = "Loại pin", GiaTri = "Lithium-ion battery"},
                new ThongSoKyThuat {SanPhamId = 12, ThuocTinh = "Màn hình", GiaTri = "4.7\""},
                new ThongSoKyThuat {SanPhamId = 12, ThuocTinh = "Trọng lượng", GiaTri = "143g"},
                new ThongSoKyThuat {SanPhamId = 12, ThuocTinh = "Video Call", GiaTri = "720p HD"},
                new ThongSoKyThuat {SanPhamId = 13, ThuocTinh = "Bộ nhớ & lưu trữ", GiaTri = "RAM 1G, ROM 16G"},
                new ThongSoKyThuat {SanPhamId = 13, ThuocTinh = "Camera", GiaTri = "8.0 MP"},
                new ThongSoKyThuat {SanPhamId = 13, ThuocTinh = "Chất liệu", GiaTri = "Nhôm"},
                new ThongSoKyThuat {SanPhamId = 13, ThuocTinh = "Dung lượng pin", GiaTri = "1810 mAh"},
                new ThongSoKyThuat {SanPhamId = 13, ThuocTinh = "Hệ điều hành", GiaTri = "IOS 8.0"},
                new ThongSoKyThuat {SanPhamId = 13, ThuocTinh = "Kết nối", GiaTri = "Băng tần 2G, 3G"},
                new ThongSoKyThuat {SanPhamId = 13, ThuocTinh = "Kích thước", GiaTri = "138.1 mm x 67 mm -  6.9 mm"},
                new ThongSoKyThuat
                {
                    SanPhamId = 13,
                    ThuocTinh = "Màn hình",
                    GiaTri = "LED-backlit IPS LCD. Độ phân giải 1334 x 750. Màn hình rộng 4.7\""
                },
                new ThongSoKyThuat {SanPhamId = 14, ThuocTinh = "Màn hình", GiaTri = "4.7\""},
                new ThongSoKyThuat {SanPhamId = 15, ThuocTinh = "Bộ nhớ trong", GiaTri = "1.5G"},
                new ThongSoKyThuat {SanPhamId = 15, ThuocTinh = "Camera", GiaTri = "13.0 MP"},
                new ThongSoKyThuat {SanPhamId = 15, ThuocTinh = "Dung lượng pin", GiaTri = "2600mAh"},
                new ThongSoKyThuat {SanPhamId = 15, ThuocTinh = "Hệ điều hành", GiaTri = "Android 5.1"},
                new ThongSoKyThuat {SanPhamId = 15, ThuocTinh = "Kích thước", GiaTri = "142.1 x 71.8 x 7.9 mm"},
                new ThongSoKyThuat {SanPhamId = 15, ThuocTinh = "Màn hình", GiaTri = "5.5\""},
                new ThongSoKyThuat {SanPhamId = 15, ThuocTinh = "Trọng lượng", GiaTri = "149g"},
                new ThongSoKyThuat {SanPhamId = 16, ThuocTinh = "Bộ nhớ trong", GiaTri = "32G"},
                new ThongSoKyThuat {SanPhamId = 16, ThuocTinh = "Độ phân giải", GiaTri = "1080 x 1920 Pixels"},
                new ThongSoKyThuat {SanPhamId = 16, ThuocTinh = "Dung lượng pin", GiaTri = "3070mAh"},
                new ThongSoKyThuat {SanPhamId = 16, ThuocTinh = "Hệ điều hành", GiaTri = "Android 5.1"},
                new ThongSoKyThuat {SanPhamId = 16, ThuocTinh = "Kích thước", GiaTri = "151.8 x 75.4 x 7 mm"},
                new ThongSoKyThuat {SanPhamId = 16, ThuocTinh = "Màn hình", GiaTri = "5.5\""},
                new ThongSoKyThuat {SanPhamId = 16, ThuocTinh = "Ram", GiaTri = "4G"},
                new ThongSoKyThuat {SanPhamId = 16, ThuocTinh = "Trọng lượng", GiaTri = "155g"},
                new ThongSoKyThuat {SanPhamId = 17, ThuocTinh = "Bộ nhớ trong", GiaTri = "32G"},
                new ThongSoKyThuat {SanPhamId = 17, ThuocTinh = "Camera", GiaTri = "13.0 MP"},
                new ThongSoKyThuat {SanPhamId = 17, ThuocTinh = "Độ phân giải", GiaTri = "1080 x 1920 pixels"},
                new ThongSoKyThuat {SanPhamId = 17, ThuocTinh = "Dung lượng pin", GiaTri = "3000 mAh"},
                new ThongSoKyThuat {SanPhamId = 17, ThuocTinh = "Hệ điều hành", GiaTri = "Android 5.0"},
                new ThongSoKyThuat {SanPhamId = 17, ThuocTinh = "Kích thước", GiaTri = "152.5 x 77.2 x 10.9 mm"},
                new ThongSoKyThuat {SanPhamId = 17, ThuocTinh = "Màn hình", GiaTri = "5.5\""},
                new ThongSoKyThuat {SanPhamId = 17, ThuocTinh = "Trọng lượng", GiaTri = "170g"},
                new ThongSoKyThuat {SanPhamId = 18, ThuocTinh = "Camera", GiaTri = "5 MP"},
                new ThongSoKyThuat {SanPhamId = 18, ThuocTinh = "Dung lượng pin", GiaTri = "2100 mAh"},
                new ThongSoKyThuat {SanPhamId = 18, ThuocTinh = "Hệ điều hành", GiaTri = "Android 4.4"},
                new ThongSoKyThuat {SanPhamId = 18, ThuocTinh = "Kích thước", GiaTri = "136.5 x 67 x 10.9mm"},
                new ThongSoKyThuat {SanPhamId = 18, ThuocTinh = "Màn hình", GiaTri = "4.5\""},
                new ThongSoKyThuat {SanPhamId = 18, ThuocTinh = "Ram", GiaTri = "1G"},
                new ThongSoKyThuat {SanPhamId = 18, ThuocTinh = "Trọng lượng", GiaTri = "143g"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "3.5mm jack", GiaTri = "Có"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Audio", GiaTri = "MP3/WAV/eAAC+/WMA"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Bảo Hành", GiaTri = "12 tháng"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Bluetooth", GiaTri = "v4.0, A2DP"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Bộ Nhớ Trong", GiaTri = "RAM 1GB ROM 16GB"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Camera chính", GiaTri = "10 MP"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Camera Phụ", GiaTri = "0.9 MP"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Chipset", GiaTri = "Qualcomm Snapdragon 400"},
                new ThongSoKyThuat
                {
                    SanPhamId = 22,
                    ThuocTinh = "Chức Năng",
                    GiaTri = "Geo-tagging, nhận diện khuôn mặt, panorama"
                },
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "CPU", GiaTri = "Quad-core 1.2 GHz"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "EDGE", GiaTri = "Có"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "GPRS", GiaTri = "Có"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "GPS ( A-GPS)", GiaTri = "Có"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "GPU", GiaTri = "Adreno 305"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Hệ Điều Hành", GiaTri = "WP 8.1"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Java", GiaTri = "Có"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Khe Cắm Thẻ Nhớ", GiaTri = "MicroSD hỗ trợ đến 128GB"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Kích Thước", GiaTri = "139.4 x 70.7 x 8.5 mm"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Kích Thước", GiaTri = "720 x 1280 pixels, 5.0 inches"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Kiểu Chuông", GiaTri = "Rung, midi, đa âm"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Loa Ngoài", GiaTri = "Có"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Loại", GiaTri = "IPS LCD"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Loại Pin", GiaTri = "Li-Ion 2200 mAh"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Mạng 2G", GiaTri = "GSM 850 / 900 / 1800 / 1900"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Mạng 3G", GiaTri = "HSDPA 850 / 900 / 1900 / 2100"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Màu Sắc", GiaTri = "Black/Green/White/Yellow"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "NFC", GiaTri = "Có"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Quay Phim", GiaTri = "1080p@30fps"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Radio", GiaTri = "Có"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Sim", GiaTri = "Nano"},
                new ThongSoKyThuat
                {
                    SanPhamId = 22,
                    ThuocTinh = "Tin Nhắn",
                    GiaTri = "SMS (threaded view), MMS, Email, Push Email, IM"
                },
                new ThongSoKyThuat
                {
                    SanPhamId = 22,
                    ThuocTinh = "Tốc độ 3G",
                    GiaTri = "HSDPA, 42.2 Mbps; HSUPA, 5.76 Mbps"
                },
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Trình Duyệt", GiaTri = "HTML5"},
                new ThongSoKyThuat
                {
                    SanPhamId = 22,
                    ThuocTinh = "Trò Chơi",
                    GiaTri = "Có thể cài đặt thêm tại Viễn Thông A"
                },
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Trọng Lượng", GiaTri = "150 g"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "USB", GiaTri = "microUSB v2.0"},
                new ThongSoKyThuat {SanPhamId = 22, ThuocTinh = "Video", GiaTri = "MP4/H.264/H.263/WMV"},
                new ThongSoKyThuat
                {
                    SanPhamId = 22,
                    ThuocTinh = "WLAN",
                    GiaTri = "Wi-Fi 802.11 a/b/g/n, DLNA, Wi-Fi hotspot"
                },
            };
            context.ThongSoKyThuats.AddRange(tskt);
            await context.SaveChangesAsync();
        }
    }
}