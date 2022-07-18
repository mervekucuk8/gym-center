using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SporSalonu.Models;
namespace SporSalonu.Controllers
{
    public class LessonController : Controller
    {
        //ilk açılan ındex sayfasında tüm kayıtları göster.
        public IActionResult Index()
        {
             ViewData["baslik"] = "Veri Tabanındaki Ürünler";
             Context dersdbisle1 = new Context();
             ModelState.Clear();//model erişim bilgisini temizle

             return View(dersdbisle1.DersleriGetir());
          
        }
        
        [HttpGet]
        public IActionResult Derskayit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Derskayit(dersmodel liste2)
        {
            if (ModelState.IsValid)
            {
                Context dersdbisle1 = new Context();
                if (dersdbisle1.DersEkle(liste2))
                {
                    ViewData["sonucmesaj"] = "Kayıt eklendi";
                    ModelState.Clear();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Dersduzenle(int Id)
        {
            Context dersdbisle1 = new Context();
            return View(dersdbisle1.DersleriGetir().Find(dersmodel => dersmodel.ders_id == Id));//link satırı ile
        }
        [HttpPost]
        public IActionResult Dersduzenle(dersmodel liste2)
        {
            try
            {
                Context dersdbisle1 = new Context();
                dersdbisle1.DersDuzenle(liste2);//değişlikleri güncelle
                return RedirectToAction("Index");//ındex sayfasına yönlendir.
            }
            catch (Exception hata)
            {
                ViewData["sonucmesaji"] = hata;
                return View();//hata oluşursa görünümü göster.
            }

        }


        public IActionResult Derssil(int Id)
        {
            try
            {
                Context dersdbisle1 = new Context();
                if (dersdbisle1.DersSil(Id))
                {
                    ViewData["sonucmesaj"] = "Kayıt silindi";
                }
                return RedirectToAction("Index");
            }
            catch
            {

                return View();
            }
        }
    }
}
