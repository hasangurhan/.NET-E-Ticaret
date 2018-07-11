using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Abc.Mvc.WebUI.Entitiy
{
    public class DataInitiliazer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var kategoriler = new List<Category>()
            {
                new Category(){ Name="Camera",Description="Kamera Ürünleri" },
                  new Category(){ Name="Bilgisayar",Description="Bilgisayar Ürünleri" },
                    new Category(){ Name="Elektronik",Description="Elektronik Ürünleri" },
                      new Category(){ Name="Telefon",Description="Telefon Ürünleri" },
                        new Category(){ Name="Beyaz Eşya",Description="Beyaz Eşya Ürünleri" }
                          

            };
            foreach (var kategori in kategoriler)
            {
                context.Categories.Add(kategori);
            }
            context.SaveChanges();

            var urunler = new List<Product>()
            {
                  new Product(){ Name="Camera",Description="Camera Ürünleri",Price=1000,Stock=500,IsApproved=true,CategoryId=1,IsHome=true,Image="2.jpg" },
                   new Product(){ Name="Bilgisayar",Description="camera Ürünleri",Price=1000,Stock=500,IsApproved=false,CategoryId=2,IsHome=true ,Image="2.jpg"},
                    new Product(){ Name="Telefon",Description="camera Ürünleri",Price=1000,Stock=500,IsApproved=false,CategoryId=4,IsHome=true,Image="1.jpg" },
                     new Product(){ Name="Beyaz Eşya",Description="Camera Ürünleri",Price=1000,Stock=500,IsApproved=true,CategoryId=5,IsHome=true,Image="2.jpg" },
                      new Product(){ Name="Telefon",Description="Camera Ürünleri",Price=1000,Stock=500,IsApproved=false,CategoryId=4 ,IsHome=true,Image="2.jpg"},
                       new Product(){ Name="Beyaz Eşya",Description="Camera Ürünleri",Price=1000,Stock=500,IsApproved=true,CategoryId=5,IsHome=true,Image="2.jpg" },
                        new Product(){ Name="Elektronik",Description="Kamera Ürünleri",Price=1000,Stock=500,IsApproved=true,CategoryId=3,IsHome=true,Image="1.jpg" }

            };
            foreach (var urun in urunler)
            {
                context.Products.Add(urun);
            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}