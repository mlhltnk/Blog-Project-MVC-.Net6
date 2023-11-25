namespace Blog_MVC.Models
{
    public class AddProfileImage   //ekleme işlemini bura üzerinden gerçekleştireceğim
    {
        //WRİTERIMAGE PROPERTYSİNİN TÜRÜNÜ DEĞİŞECEĞİZ BU İŞLEMİ writer.cs İÇİNDE YAPMAK İSTEMEDİĞİM İÇİN
        //BU İŞLEMİ MODEL İÇERİSİNE OLUŞTURDUĞUM BU CLASS ÜZERİNDEN YAPACAĞIM
 
        public int WriterID { get; set; }

        public string WriterName { get; set; }

        public string WriterAbout { get; set; }

        public IFormFile WriterImage { get; set; }  //dosyadan bir dosya değeri seçebilmem için Iformfile türüne çevirdim

        public string WriterMail { get; set; }
        public string WriterPassword { get; set; }

        public bool WriterStatus { get; set; }
    }
}
