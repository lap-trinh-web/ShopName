using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Giohang
    {


            //Tao doi tuong data chua dữ liệu từ model dbBansach đã tạo. 
            dbQLSNDataContext data = new dbQLSNDataContext();
            public int iMASP { set; get; }
            public string iTENSP { set; get; }
            public string sANHBIA { set; get; }
            public Double dDONGIA { set; get; }
            public int iSOLUONG{ set; get; }
            public Double dTHANHTIEN
            {
                get { return iSOLUONG * dDONGIA; }

            }
          
            public Giohang(int MASP)
            {
                iMASP = MASP;
                SANPHAM sp = data.SANPHAMs.Single(n => n.MASP == iMASP);
                iTENSP = sp.TENSP;
            sANHBIA = sp.ANHBIA;
            dDONGIA = double.Parse(sp.GIABAN.ToString());
            iSOLUONG = 1;
            }
        
    
}
}