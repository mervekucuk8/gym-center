using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;


namespace SporSalonu.Models
{
    public class Context: DbContext
    {
       

        public DbSet<dersmodel> Dersmodels { get; set; }

        public DbSet<uyemodel> Uyemodels { get; set; }

        public DbSet<kayitolmodel> Kayitolmodels { get; set; }

        public DbSet<egitmenmodel> Egitmenmodels { get; set; }

        //Veri tabanına işler.Genel veri tabanı işkemlerini yapar.
        private SqlConnection baglanti;

        private void Baglan()
        {
            //Bağlantı cümlesini Program.cs'den getir.
             baglanti = new SqlConnection(Program.bcumle);
            baglanti.Open();
        }

        //Üye Yeni kayıt ekleme.
        public string UyeEkle(uyemodel liste1)
        {
            Baglan();
            //baglanti yap 



            string sql = "Select Count(*) From tbl_01_uyeler Where Tcno=@Tcno";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            komut.Parameters.AddWithValue("@Tcno", liste1.tc);
            if ((int)komut.ExecuteScalar() > 0)
            {
                return "TC Numarasına kayıtlı başka bir üye bulunmaktadır.";

            }


            sql = "Insert into tbl_01_uyeler (" +
              "Tcno," +
              "Adi," +
              "Soyadi," +
              "Telefon," +
              "DogumTarihi," +
              "İl," +
              "İlce," +
              "Adres" +
              ") " +
              " Values(" +
              "@Tcno," +
              "@Adi," +
              "@Soyadi," +
              "@Telefon," +
              "@DogumTarihi," +
              "@İl," +
              "@İlce," +
              "@Adres)";
            komut = new SqlCommand(sql, baglanti);
            //parametleri geç
            komut.Parameters.AddWithValue("@Tcno", liste1.tc);
            komut.Parameters.AddWithValue("@Adi", liste1.adi);
            komut.Parameters.AddWithValue("@Soyadi", liste1.soyad);
            komut.Parameters.AddWithValue("@Telefon", liste1.telefon);
            komut.Parameters.AddWithValue("@DogumTarihi", liste1.dogumtarihi);
            komut.Parameters.AddWithValue("@İl", liste1.il);
            komut.Parameters.AddWithValue("@İlce", liste1.ilce);
            komut.Parameters.AddWithValue("@Adres", liste1.adres);
            int eklenen = 0;
            eklenen = komut.ExecuteNonQuery();
            baglanti.Close();
            //Eklenen 1 den büyükse(kayıt eklenmişse) 1 değeri döndür.
            if (eklenen >= 1)
                return"Kayıt eklendi.";
            else
                return "Üye eklenemedi lütfen tekrar deneyin.";

        }

        //Üye Kayıtları listeleme
        public List<uyemodel> UyeleriGetir()
        {
            Baglan();
            List<uyemodel> liste1 = new List<uyemodel>();
            string sql = "Select * from tbl_01_uyeler order by id";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            SqlDataAdapter adap1 = new SqlDataAdapter(komut);
            DataTable tablo1 = new DataTable();
            adap1.Fill(tablo1);
            baglanti.Close();
            //Döngü yap
            foreach (DataRow kayit in tablo1.Rows)
            {
                //Listeyi ekle
                liste1.Add(new uyemodel
                {
                    id = Convert.ToInt32(kayit["id"]),
                    tc = Convert.ToString(kayit["Tcno"]),
                    adi = Convert.ToString(kayit["Adi"]),
                    soyad = Convert.ToString(kayit["Soyadi"]),
                    telefon = Convert.ToString(kayit["Telefon"]),
                    dogumtarihi = Convert.ToDateTime(kayit["DogumTarihi"]),
                    il = Convert.ToString(kayit["İl"]),
                    ilce = Convert.ToString(kayit["İlce"]),
                    adres = Convert.ToString(kayit["Adres"])
                });
            }
            return liste1;//Oluşan listeyi döndürür.
        }

        //Üye Kayıt Düzenleme
        public string UyeDuzenle(uyemodel liste1)
        {
            Baglan();

            string sql = "Select Count(*) From tbl_01_uyeler Where Tcno=@Tcno AND id != @id";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            komut.Parameters.AddWithValue("@Tcno", liste1.tc);
            komut.Parameters.AddWithValue("@id", liste1.id);

            if ((int)komut.ExecuteScalar() > 0)
            {
                return "TC Numarasına kayıtlı başka bir öğrenci bulunmaktadır.";

            }
           
            //baglanti yap 
            sql = "Update tbl_01_uyeler set " +
                "Tcno=@Tcno," +
                "Adi=@Adi," +
                "Soyadi=@Soyadi," +
                "Telefon=@Telefon," +
                "DogumTarihi=@DogumTarihi," +
                "İl=@İl," +
                "İlce=@İlce," +
                "Adres=@Adres WHERE id=@id";
            komut = new SqlCommand(sql, baglanti);
            //parametleri geç
            komut.Parameters.AddWithValue("@Tcno", liste1.tc);
            komut.Parameters.AddWithValue("@Adi", liste1.adi);
            komut.Parameters.AddWithValue("@Soyadi", liste1.soyad);
            komut.Parameters.AddWithValue("@Telefon", liste1.telefon);
            komut.Parameters.AddWithValue("@DogumTarihi", liste1.dogumtarihi);
            komut.Parameters.AddWithValue("@İl", liste1.il);
            komut.Parameters.AddWithValue("@İlce", liste1.ilce);
            komut.Parameters.AddWithValue("@Adres", liste1.adres);
            komut.Parameters.AddWithValue("@id", liste1.id);
            int eklenen = 0;
            eklenen = komut.ExecuteNonQuery();
            baglanti.Close();
            //Eklenen 1 den büyükse(kayıt düenlenmişse) 1 değeri döndür.
            if (eklenen >= 1)
                return "Kullanıcı başarıyla güncellendi.";
            else

                return "Kullanıcı güncellenemedi.";

        }

        //Üye Kayıt Silme
        public bool UyeSil(int Id)
        {
            Baglan();
            //baglanti yap 
            string sql = "Delete from tbl_01_uyeler Where id=@id";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            //parametleri geç
            komut.Parameters.AddWithValue("@id", Id);
            int eklenen = 0;
            eklenen = komut.ExecuteNonQuery();
            baglanti.Close();
            //Eklenen 1 den büyükse(kayıt silinmişse) 1 değeri döndür.
            if (eklenen >= 1)
                return true;
            else

                return false;

        }
        

        //Ders Yeni kayıt ekleme.
        public bool DersEkle(dersmodel liste2)
        {
            Baglan();
            //baglanti yap 

            string sql = "Insert into tbl_03_dersler (" +

               "ders_kodu," +
               "ders_adi" +
               ") " +
               " Values(" +

               "@ders_kodu," +
               "@ders_adi)";


            SqlCommand komut = new SqlCommand(sql, baglanti);
            //parametleri geç
            komut.Parameters.AddWithValue("@ders_kodu", liste2.ders_kodu);
            komut.Parameters.AddWithValue("@ders_adi", liste2.ders_adi);
            int eklenen = 0;
            eklenen = komut.ExecuteNonQuery();
            baglanti.Close();
            //Eklenen 1 den büyükse(kayıt eklenmişse) 1 değeri döndür.
            if (eklenen >= 1)
                return true;
            else
                return false;

        }

        //Ders Kayıtları listeleme
        public List<dersmodel> DersleriGetir()
        {
            Baglan();
            List<dersmodel> liste2 = new List<dersmodel>();
            string sql = "Select * from tbl_03_dersler order by ders_id ";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            SqlDataAdapter adap1 = new SqlDataAdapter(komut);
            DataTable tablo2 = new DataTable();
            adap1.Fill(tablo2);
            baglanti.Close();
            //Döngü yap
            foreach (DataRow kayit2 in tablo2.Rows)
            {
                //Listeyi ekle
                liste2.Add(new dersmodel
                {
                    ders_id = Convert.ToInt32(kayit2["ders_id"]),
                    ders_kodu = Convert.ToString(kayit2["ders_kodu"]),
                    ders_adi = Convert.ToString(kayit2["ders_adi"])
                });
            }
            return liste2;//Oluşan listeyi döndürür.
        }

        //Ders Kayıt Düzenleme
        public bool DersDuzenle(dersmodel liste2)
        {
            Baglan();
            //baglanti yap 
            string sql = "Update tbl_03_dersler set " +
                "ders_kodu=@ders_kodu," +
                "ders_adi=@ders_adi WHERE ders_id=@ders_id ";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            //parametleri geç
            komut.Parameters.AddWithValue("@ders_kodu", liste2.ders_kodu);
            komut.Parameters.AddWithValue("@ders_adi", liste2.ders_adi);
            komut.Parameters.AddWithValue("@ders_id", liste2.ders_id);
            int eklenen = 0;
            eklenen = komut.ExecuteNonQuery();
            baglanti.Close();
            //Eklenen 1 den büyükse(kayıt düenlenmişse) 1 değeri döndür.
            if (eklenen >= 1)
                return true;
            else

                return false;

        }

        //Ders Kayıt Silme
        public bool DersSil(int Id)
        {
             Baglan();
            //baglanti yap 
            string sql = "Delete from tbl_03_dersler Where ders_id=@ders_id";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            //parametleri geç
            komut.Parameters.AddWithValue("@ders_id", Id);
            int eklenen = 0;
            eklenen = komut.ExecuteNonQuery();
            baglanti.Close();
            //Eklenen 1 den büyükse(kayıt silinmişse) 1 değeri döndür.
            if (eklenen >= 1)
                return true;
            else

                return false;

        }
        //Kayıtol Yeni kayıt ekleme.
        public string Kayitol(kayitolmodel liste1)
        {
            Baglan();
            //baglanti yap 


            string sql = "Select Count(*) From tbl_00_kayitol Where tcno=@tcno";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            komut.Parameters.AddWithValue("@tcno", liste1.tcno);
            if ((int)komut.ExecuteScalar() > 0)
            {
                return "TC Numarasına kayıtlı başka bir kullanıcı bulunmaktadır.";

            }


            sql = "Insert into tbl_00_kayitol (" +
              "tcno," +
              "kullaniciadi," +
              "sifre," +
              "adi," +
              "soyadi," +
              "telno," +
              "dtarih," +
              "il," +
              "ilce," +
              "adres" +
              ") " +
              " Values(" +
               "@tcno," +
              "@kullaniciadi," +
              "@sifre," +
              "@adi," +
              "@soyadi," +
              "@telno," +
              "@dtarih," +
              "@il," +
              "@ilce," +
              "@adres )";
            komut = new SqlCommand(sql, baglanti);
            //parametleri geç
            komut.Parameters.AddWithValue("@tcno", liste1.tcno);
            komut.Parameters.AddWithValue("@kullaniciadi", liste1.kullaniciadi);
            komut.Parameters.AddWithValue("@sifre", liste1.sifre);
            komut.Parameters.AddWithValue("@adi", liste1.adi);
            komut.Parameters.AddWithValue("@soyadi", liste1.soyadi);
            komut.Parameters.AddWithValue("@telno", liste1.telno);
            komut.Parameters.AddWithValue("@dtarih", liste1.dtraih);
            komut.Parameters.AddWithValue("@il", liste1.il);
            komut.Parameters.AddWithValue("@ilce", liste1.ilce);
            komut.Parameters.AddWithValue("@adres", liste1.adres);
            int eklenen = 0;
            eklenen = komut.ExecuteNonQuery();
            baglanti.Close();
            //Eklenen 1 den büyükse(kayıt eklenmişse) 1 değeri döndür.
            if (eklenen >= 1)
                return "Kayıt olundu.";
            else
                return "Kayıt olunamadı lütfen tekrar deneyin.";
        }

        //Kayıtol Kayıtları listeleme
        public List<kayitolmodel> KayitleriGetir()
        {
            Baglan();
            List<kayitolmodel> liste1 = new List<kayitolmodel>();
            string sql = "Select * from tbl_00_kayitol order by kullanici_id";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            SqlDataAdapter adap1 = new SqlDataAdapter(komut);
            DataTable tablo1 = new DataTable();
            adap1.Fill(tablo1);
            baglanti.Close();
            //Döngü yap
            foreach (DataRow kayit in tablo1.Rows)
            {
                //Listeyi ekle
                liste1.Add(new kayitolmodel
                {

                    kullanici_id = Convert.ToInt32(kayit["kullanici_id"]),
                    tcno = Convert.ToString(kayit["tcno"]),
                    kullaniciadi = Convert.ToString(kayit["kullaniciadi"]),
                    sifre = Convert.ToString(kayit["sifre"]),
                    adi = Convert.ToString(kayit["adi"]),
                    soyadi = Convert.ToString(kayit["soyadi"]),
                    telno = Convert.ToString(kayit["telno"]),
                    dtraih = Convert.ToDateTime(kayit["dtarih"]),
                    il = Convert.ToString(kayit["il"]),
                    ilce = Convert.ToString(kayit["ilce"]),
                    adres = Convert.ToString(kayit["adres"])
                });
            }
            return liste1;//Oluşan listeyi döndürür.
        }
        //Eğitmen Yeni kayıt ekleme.
        public bool EgitmenEkle(egitmenmodel liste3)
        {
            Baglan();
            //baglanti yap 
            string sql = "Insert into tbl_02_antrenörler (" +
                "adi," +
                "soyadi," +
                "telefon," +
                "dogum_tarihi," +
                "il," +
                "ilce ," +
                "adres " +
                ")" +
                " Values(" +
                "@adi," +
                "@soyadi," +
                "@telefon," +
                "@dogum_tarihi," +
                "@il," +
                "@ilce ," +
                "@adres)";




            SqlCommand komut = new SqlCommand(sql, baglanti);
            //parametleri geç
            komut.Parameters.AddWithValue("@adi", liste3.adi);
            komut.Parameters.AddWithValue("@soyadi", liste3.soyad);
            komut.Parameters.AddWithValue("@telefon", liste3.telefon);
            komut.Parameters.AddWithValue("@dogum_tarihi", liste3.dogumtarihi);
            komut.Parameters.AddWithValue("@il", liste3.il);
            komut.Parameters.AddWithValue("@ilce", liste3.ilce);
            komut.Parameters.AddWithValue("@adres", liste3.adres);


            int eklenen = 0;
            eklenen = komut.ExecuteNonQuery();
            baglanti.Close();
            //Eklenen 1 den büyükse(kayıt eklenmişse) 1 değeri döndür.
            if (eklenen >= 1)
                return true;
            else
                return false;

        }

        //Eğitmen Kayıtları listeleme
        public List<egitmenmodel> EgitmenleriGetir()
        {
            Baglan();
            List<egitmenmodel> liste3 = new List<egitmenmodel>();
            string sql = "Select * from tbl_02_antrenörler order by id ";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            SqlDataAdapter adap1 = new SqlDataAdapter(komut);
            DataTable tablo3 = new DataTable();
            adap1.Fill(tablo3);
            baglanti.Close();
            //Döngü yap
            foreach (DataRow kayit3 in tablo3.Rows)
            {
                //Listeyi ekle
                liste3.Add(new egitmenmodel
                {
                    id = Convert.ToInt32(kayit3["id"]),
                    adi = Convert.ToString(kayit3["adi"]),
                    soyad = Convert.ToString(kayit3["soyadi"]),
                    telefon = Convert.ToString(kayit3["telefon"]),
                    dogumtarihi = Convert.ToDateTime(kayit3["dogum_tarihi"]),
                    il = Convert.ToString(kayit3["il"]),
                    ilce = Convert.ToString(kayit3["ilce"]),
                    adres = Convert.ToString(kayit3["adres"])

                });
            }
            return liste3;//Oluşan listeyi döndürür.
        }

        //Eğitmen Kayıt Düzenleme
        public bool EgitmenDuzenle(egitmenmodel liste3)
        {
            Baglan();
            //baglanti yap 
            string sql = "Update tbl_02_antrenörler set " +
                "adi=@adi," +
                "soyadi=@soyadi," +
                "telefon=@telefon," +
                "dogum_tarihi=@dogum_tarihi," +
                "il=@il," +
                "ilce=@ilce," +
                "adres=@adres WHERE id=@id";

            SqlCommand komut = new SqlCommand(sql, baglanti);
            //parametleri geç
            komut.Parameters.AddWithValue("@adi", liste3.adi);
            komut.Parameters.AddWithValue("@soyadi", liste3.soyad);
            komut.Parameters.AddWithValue("@telefon", liste3.telefon);
            komut.Parameters.AddWithValue("@dogum_tarihi", liste3.dogumtarihi);
            komut.Parameters.AddWithValue("@il", liste3.il);
            komut.Parameters.AddWithValue("@ilce", liste3.ilce);
            komut.Parameters.AddWithValue("@adres", liste3.adres);
            komut.Parameters.AddWithValue("@id", liste3.id);

            int eklenen = 0;
            eklenen = komut.ExecuteNonQuery();
            baglanti.Close();
            //Eklenen 1 den büyükse(kayıt düenlenmişse) 1 değeri döndür.
            if (eklenen >= 1)
                return true;
            else

                return false;

        }

        //eğitmen Kayıt Silme
        public bool EgitmenSil(int Id)
        {
            Baglan();
            //baglanti yap 
            string sql = "Delete from tbl_02_antrenörler Where id=@id";
            SqlCommand komut = new SqlCommand(sql, baglanti);
            //parametleri geç
            komut.Parameters.AddWithValue("@id", Id);
            int eklenen = 0;
            eklenen = komut.ExecuteNonQuery();
            baglanti.Close();
            //Eklenen 1 den büyükse(kayıt silinmişse) 1 değeri döndür.
            if (eklenen >= 1)
                return true;
            else

                return false;

        }
    }
}
