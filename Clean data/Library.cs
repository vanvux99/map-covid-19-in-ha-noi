using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;

namespace ConsoleApp
{
    public class Library
    {
        #region Biến và hằng
        public const int Table_SoCaMacBenhTheoHuyen_HaNoi = 1;
        public const int Table_SoCaMacBenhTheoHuyen2_HaNoi = 2;
        public const int Table_CaBenhTheoNgay = 3;
        public static string connectionString = "Data Source=DESKTOP-3SJPQVF;Initial Catalog=COVID-19;Integrated Security=True";
        static string Date_table2 = "";
        static int CaNhiem_BaDinh = 0;
        static int CaNhiem_BaVi = 0;
        static int CaNhiem_BacTuLiem = 0;
        static int CaNhiem_CauGiay = 0;
        static int CaNhiem_ChuongMy = 0;
        static int CaNhiem_DanPhuong = 0;
        static int CaNhiem_DongAnh = 0;
        static int CaNhiem_DongDa = 0;
        static int CaNhiem_GiaLam = 0;
        static int CaNhiem_HaDong = 0;
        static int CaNhiem_HaiBaTrung = 0;
        static int CaNhiem_HoaiDuc = 0;
        static int CaNhiem_HoanKiem = 0;
        static int CaNhiem_HoangMai = 0;
        static int CaNhiem_LongBien = 0;
        static int CaNhiem_MeLinh = 0;
        static int CaNhiem_MyDuc = 0;
        static int CaNhiem_NamTuLiem = 0;
        static int CaNhiem_PhuXuyen = 0;
        static int CaNhiem_PhucTho = 0;
        static int CaNhiem_QuocOai = 0;
        static int CaNhiem_SocSon = 0;
        static int CaNhiem_SonTay = 0;
        static int CaNhiem_TayHo = 0;
        static int CaNhiem_ThachThat = 0;
        static int CaNhiem_ThanhOai = 0;
        static int CaNhiem_ThanhTri = 0;
        static int CaNhiem_ThanhXuan = 0;
        static int CaNhiem_ThuongTin = 0;
        static int CaNhiem_UngHoa = 0;
        const string BaDinh = "Ba Dinh";
        const string BaVi = "Ba Vi";
        const string BacTuLiem = "Bac Tu Liem";
        const string CauGiay = "Cau Giay";
        const string ChuongMy = "Chuong My";
        const string DanPhuong = "Dan Phuong";
        const string DongAnh = "Dong Anh";
        const string DongDa = "Dong Da";
        const string GiaLam = "Gia Lam";
        const string HaDong = "Ha Dong";
        const string HaiBaTrung = "Hai Ba Trung";
        const string HoaiDuc = "Hoai Duc";
        const string HoanKiem = "Hoan Kiem";
        const string HoangMai = "Hoang Mai";
        const string LongBien = "Long Bien";
        const string MeLinh = "Me Linh";
        const string MyDuc = "My Duc";
        const string NamTuLiem = "Nam Tu Liem";
        const string PhuXuyen = "Phu Xuyen";
        const string PhucTho = "Phuc Tho";
        const string QuocOai = "Quoc Oai";
        const string SocSon = "Soc Son";
        const string SonTay = "Son Tay";
        const string TayHo = "Tay Ho";
        const string ThachThat = "Thach That";
        const string ThanhOai = "Thanh Oai";
        const string ThanhTri = "Thanh Tri";
        const string ThanhXuan = "Thanh Xuan";
        const string ThuongTin = "Thuong Tin";
        const string UngHoa = "Ung Hoa";
        #endregion

        // connect db
        public static DataTable ConnectionDatabase(string cmd)
        {
            SqlConnection con = new SqlConnection(connectionString);
            DataTable dataTable = new DataTable();
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand(cmd, con);
                SqlDataAdapter sqlData = new SqlDataAdapter(command);
                sqlData.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return dataTable;
        }

        // xử lí dấu
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ", "đ",
                                            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                                            "í","ì","ỉ","ĩ","ị",
                                            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                                            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                                            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "d",
                                            "e","e","e","e","e","e","e","e","e","e","e",
                                            "i","i","i","i","i",
                                            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                                            "u","u","u","u","u","u","u","u","u","u","u",
                                            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }

            return text;
        }

        // xử lý dữ liệu trùng lặp, khi huyên đó trong 1 ngày có thêm mới ca nhiễm, tự động cộng dồn
        public static Hashtable HashData(List<string> dataa)
        {
            Hashtable hash = new Hashtable();
            string[] a12 = { };
            foreach (var item in dataa)
            {
                a12 = item.Split(',');
            }
            foreach (var value in a12)
            {
                string data = Regex.Replace(value.Trim(), "[(,)]", "");
                string local = data.Substring(0, data.Length - 2).Trim();
                int caBenh = int.Parse(data.Substring(data.Length - 2).Trim().ToString());
                if (hash.ContainsKey(local) == false)
                {
                    hash.Add(local, caBenh);
                }
                else
                {
                    int tongCaBenh = 0;
                    tongCaBenh = int.Parse(hash[local].ToString()) + caBenh;
                    hash.Remove(local);
                    hash.Add(local, tongCaBenh);
                }
            }

            return hash;
        }

        // cập nhật số ca bệnh theo ngày và huyện 
        public static bool InsertData(DateTime date, string local, int countPatient, int select)
        {
            bool result = false;
            switch (local)
            {
                case BaDinh:
                    if (select == 1)
                        UpdateData(BaDinh.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(BaDinh, countPatient, date, select);
                    if (select == 3)
                        UpdateData(BaDinh, countPatient, date, select);
                    result = true;
                    break;

                case BaVi:
                    if (select == 1)
                        UpdateData(BaVi.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(BaVi, countPatient, date, select);
                    if (select == 3)
                        UpdateData(BaVi, countPatient, date, select);
                    result = true;
                    break;

                case BacTuLiem:
                    if (select == 1)
                        UpdateData(BacTuLiem.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(BacTuLiem, countPatient, date, select);
                    if (select == 3)
                        UpdateData(BacTuLiem, countPatient, date, select);
                    result = true;
                    break;

                case CauGiay:
                    if (select == 1)
                        UpdateData(CauGiay.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(CauGiay, countPatient, date, select);
                    if (select == 3)
                        UpdateData(CauGiay, countPatient, date, select);
                    result = true;
                    break;

                case ChuongMy:
                    if (select == 1)
                        UpdateData(ChuongMy.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(ChuongMy, countPatient, date, select);
                    if (select == 3)
                        UpdateData(ChuongMy, countPatient, date, select);
                    result = true;
                    break;

                case DanPhuong:
                    if (select == 1)
                        UpdateData(DanPhuong.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(DanPhuong, countPatient, date, select);
                    if (select == 3)
                        UpdateData(DanPhuong, countPatient, date, select);
                    result = true;
                    break;

                case DongAnh:
                    if (select == 1)
                        UpdateData(DongAnh.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(DongAnh, countPatient, date, select);
                    if (select == 3)
                        UpdateData(DongAnh, countPatient, date, select);
                    result = true;
                    break;

                case DongDa:
                    if (select == 1)
                        UpdateData(DongDa.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(DongDa, countPatient, date, select);
                    if (select == 3)
                        UpdateData(DongDa, countPatient, date, select);
                    result = true;
                    break;

                case GiaLam:
                    if (select == 1)
                        UpdateData(GiaLam.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(GiaLam, countPatient, date, select);
                    if (select == 3)
                        UpdateData(GiaLam, countPatient, date, select);
                    result = true;
                    break;

                case HaDong:
                    if (select == 1)
                        UpdateData(HaDong.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(HaDong, countPatient, date, select);
                    if (select == 3)
                        UpdateData(HaDong, countPatient, date, select);
                    result = true;
                    break;

                case HaiBaTrung:
                    if (select == 1)
                        UpdateData(HaiBaTrung.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(HaiBaTrung, countPatient, date, select);
                    if (select == 3)
                        UpdateData(HaiBaTrung, countPatient, date, select);
                    result = true;
                    break;

                case HoaiDuc:
                    if (select == 1)
                        UpdateData(HoaiDuc.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(HoaiDuc, countPatient, date, select);
                    if (select == 3)
                        UpdateData(HoaiDuc, countPatient, date, select);
                    result = true;
                    break;

                case HoanKiem:
                    if (select == 1)
                        UpdateData(HoanKiem.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(HoanKiem, countPatient, date, select);
                    if (select == 3)
                        UpdateData(HoanKiem, countPatient, date, select);
                    result = true;
                    break;

                case HoangMai:
                    if (select == 1)
                        UpdateData(HoangMai.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(HoangMai, countPatient, date, select);
                    if (select == 3)
                        UpdateData(HoangMai, countPatient, date, select);
                    result = true;
                    break;

                case LongBien:
                    if (select == 1)
                        UpdateData(LongBien.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(LongBien, countPatient, date, select);
                    if (select == 3)
                        UpdateData(LongBien, countPatient, date, select);
                    result = true;
                    break;

                case MeLinh:
                    if (select == 1)
                        UpdateData(MeLinh.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(MeLinh, countPatient, date, select);
                    if (select == 3)
                        UpdateData(MeLinh, countPatient, date, select);
                    result = true;
                    break;

                case MyDuc:
                    if (select == 1)
                        UpdateData(MyDuc.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(MyDuc, countPatient, date, select);
                    if (select == 3)
                        UpdateData(MyDuc, countPatient, date, select);
                    result = true;
                    break;

                case NamTuLiem:
                    if (select == 1)
                        UpdateData(NamTuLiem.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(NamTuLiem, countPatient, date, select);
                    if (select == 3)
                        UpdateData(NamTuLiem, countPatient, date, select);
                    result = true;
                    break;

                case PhuXuyen:
                    if (select == 1)
                        UpdateData(PhuXuyen.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(PhuXuyen, countPatient, date, select);
                    if (select == 3)
                        UpdateData(PhuXuyen, countPatient, date, select);
                    result = true;
                    break;

                case PhucTho:
                    if (select == 1)
                        UpdateData(PhucTho.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(PhucTho, countPatient, date, select);
                    if (select == 3)
                        UpdateData(PhucTho, countPatient, date, select);
                    result = true;
                    break;

                case QuocOai:
                    if (select == 1)
                        UpdateData(QuocOai.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(QuocOai, countPatient, date, select);
                    if (select == 3)
                        UpdateData(QuocOai, countPatient, date, select);
                    result = true;
                    break;

                case SocSon:
                    if (select == 1)
                        UpdateData(SocSon.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(SocSon, countPatient, date, select);
                    if (select == 3)
                        UpdateData(SocSon, countPatient, date, select);
                    result = true;
                    break;

                case SonTay:
                    if (select == 1)
                        UpdateData(SonTay.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(SonTay, countPatient, date, select);
                    if (select == 3)
                        UpdateData(SonTay, countPatient, date, select);
                    result = true;
                    break;

                case TayHo:
                    if (select == 1)
                        UpdateData(TayHo.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(TayHo, countPatient, date, select);
                    if (select == 3)
                        UpdateData(TayHo, countPatient, date, select);
                    result = true;
                    break;

                case ThachThat:
                    if (select == 1)
                        UpdateData(ThachThat.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(ThachThat, countPatient, date, select);
                    if (select == 3)
                        UpdateData(ThachThat, countPatient, date, select);
                    result = true;
                    break;

                case ThanhOai:
                    if (select == 1)
                        UpdateData(ThanhOai.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(ThanhOai, countPatient, date, select);
                    if (select == 3)
                        UpdateData(ThanhOai, countPatient, date, select);
                    result = true;
                    break;

                case ThanhTri:
                    if (select == 1)
                        UpdateData(ThanhTri.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(ThanhTri, countPatient, date, select);
                    if (select == 3)
                        UpdateData(ThanhTri, countPatient, date, select);
                    result = true;
                    break;

                case ThanhXuan:
                    if (select == 1)
                        UpdateData(ThanhXuan.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(ThanhXuan, countPatient, date, select);
                    if (select == 3)
                        UpdateData(ThanhXuan, countPatient, date, select);
                    result = true;
                    break;

                case ThuongTin:
                    if (select == 1)
                        UpdateData(ThuongTin.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(ThuongTin, countPatient, date, select);
                    if (select == 3)
                        UpdateData(ThuongTin, countPatient, date, select);
                    result = true;
                    break;

                case UngHoa:
                    if (select == 1)
                        UpdateData(UngHoa.Replace(" ", String.Empty), countPatient, date, select);
                    if (select == 2)
                        UpdateData(UngHoa, countPatient, date, select);
                    if (select == 3)
                        UpdateData(UngHoa, countPatient, date, select);
                    result = true;
                    break;
            }

            return result;
        }

        // query update vào db
        public static void UpdateData(string local, int countPatient, DateTime date, int select)
        {
            string cmd = "";
            string table_hn1 = "Proc_CheckColumName '" + ConvertDate(date) + "', '" + "" + date + "" + "', " + Table_SoCaMacBenhTheoHuyen_HaNoi + "";
            string table_hn2 = "Proc_CheckColumName '" + ConvertDate(date) + "', '" + date + "', " + Table_SoCaMacBenhTheoHuyen2_HaNoi + " ";
            string bol = "false";

            if (select == 1)
            {
                ConnectionDatabase(table_hn1);

                cmd = "UPDATE dbo.Table_SoCaMacBenhTheoHuyen_HaNoi SET " + local + " = " + countPatient + " " +
                    "WHERE NgayMacBenh = CONVERT(DATETIME, '" + date + "') if (@@ROWCOUNT > 0) select 'ok' else select 'false'";
                ConnectionDatabase(cmd);
            }

            if (select == 2)
            {
                string resultConnect = ConnectionDatabase(table_hn1).Rows[0][0].ToString();
                if (string.Compare(resultConnect, bol) == 0) 
                {
                    // thêm ngày vào table nếu nó chưa có trong table
                    cmd = "ALTER TABLE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi ADD " + ConvertDate(date) + " INT ";
                    ConnectionDatabase(cmd);
                }

                // cập nhật các ca nhiễm theo huyện trong ngày
                cmd = "UPDATE dbo.Table_SoCaMacBenhTheoHuyen2_HaNoi SET " + ConvertDate(date) + " = " + countPatient + " WHERE TenHuyen = '" + local + "'";
                ConnectionDatabase(cmd);
            }

            if (select == 3)
            {
                cmd = "Proc_CheckRowIdentityTable_CaBenhTheoNgay '" + local + "', '" + date + "' ";
                string resultConnect = ConnectionDatabase(cmd).Rows[0][0].ToString();
                if (string.Compare(resultConnect, bol) == 0)
                {
                    cmd = "Proc_CRUDTable_CaBenhTheoNgay '" + local + "', " + countPatient + ", '" + date + "', 1 ";
                    ConnectionDatabase(cmd);
                }
                else
                {
                    cmd = "Proc_CRUDTable_CaBenhTheoNgay '" + local + "', " + countPatient + ", '" + date + "', 2 ";
                    ConnectionDatabase(cmd);
                }    
            }
        }

        // xử lý ngày để thêm vào table
        public static string ConvertDate(DateTime date)
        {
            string data = String.Format("{0:s}", date);
            Date_table2 = ("N_" + data.Substring(0, 10)).Replace('-', '_');
            return Date_table2;
        }

        // cập nhật ca bệnh từng huyện theo ngày(hà nội 2)

    }
}
