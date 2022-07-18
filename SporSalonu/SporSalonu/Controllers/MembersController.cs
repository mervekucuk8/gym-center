using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SporSalonu.Models;
using Microsoft.EntityFrameworkCore;

namespace SporSalonu.Controllers
{
    public class MembersController : Controller
    {
        //ilk açılan ındex sayfasında tüm kayıtları göster. 
        public IActionResult Index()
        {
            ViewData["baslik"] = "Veri Tabanındaki Ürünler";
            Context uyedbisle1 = new Context();           
            ModelState.Clear();//model erişim bilgisini temizle
            return View(uyedbisle1.UyeleriGetir());
        }
        
        [HttpGet]
        public IActionResult Uyekayit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Uyekayit(uyemodel liste1)
        {
            if (ModelState.IsValid)
            {
                Context uyedbisle1 = new Context();
                string uyeekledurum = uyedbisle1.UyeEkle(liste1);
               
               
                    ViewData["sonucmesaj"] = uyeekledurum;
                    ModelState.Clear();
               
            }
            
            return View();
        }

        [HttpGet]
        public IActionResult Uyeduzenle(int Id)
        {
            Context uyedbisle1 = new Context();
            return View(uyedbisle1.UyeleriGetir().Find(uyemodel =>uyemodel.id==Id));//link satırı ile idye göre seçim yapması sağlandı..
        }
        [HttpPost]
        public IActionResult Uyeduzenle(uyemodel liste1)
        {
            try
            {
                Context uyedbisle1 = new Context();
                ViewData["sonucmesaji"] = uyedbisle1.UyeDuzenle(liste1);//değişlikleri güncelle
                return RedirectToAction("Index");//ındex sayfasına yönlendir.
            }
            catch (Exception hata)
            {
                ViewData["sonucmesaji"]=hata;
                return View();//hata oluşursa görünümü göster.
            }
            
        }

        
        public IActionResult Uyesil(int Id)
        {
            try
            {
                Context uyedbisle1 = new Context();
                if (uyedbisle1.UyeSil(Id))
                {
                    ViewData["sonucmesaj"]="Kayıt silindi";
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
