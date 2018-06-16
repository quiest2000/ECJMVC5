﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC_TH2012_J.Models.Domain.EfModels;

namespace EC_TH2012_J.Models
{
    public class Chitietgiohang
    {
        public SanPham sanPham { get; set; }
        public int Soluong { get; set; }
        private double thanhtien;

        public double Thanhtien
        {
            get { return (double)sanPham.GiaTien * Soluong; }
            set { thanhtien = value; }
        }
        public Domain.EfModels.DonHangKH Donhangkh { get; set; }
       
        public void Tinhtien()
        {
            Thanhtien = (double)sanPham.GiaTien * Soluong;
        }
    }
}