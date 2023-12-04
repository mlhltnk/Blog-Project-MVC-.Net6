using Blog_MVC.Areas.Admin.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace Blog_MVC.Areas.Admin.Controllers
{
	public class BlogController : Controller
	{
		public IActionResult ExportStaticExcelBlogList()
		{
			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("BlogListesi");
				worksheet.Cell(1, 1).Value = "Blog ID";  //1.satır 1. sutun değeri
				worksheet.Cell(1, 2).Value = "Blog Adı"; //1.satır 2. sutun değeri

				int BlogRowCount = 2;  //veriler 2. satırdan başlasın,çünkü ilk satırda başlıklar var
				foreach (var item in GetBlogList())
				{
					worksheet.Cell(BlogRowCount, 1).Value = item.ID;
					worksheet.Cell(BlogRowCount, 2).Value = item.BlogName;
					BlogRowCount++;
				}

				using (var stream = new MemoryStream())
				{
					workbook.SaveAs(stream);
					var content = stream.ToArray();
					return File(content, "application / vnd.openxmlformats - officedocument.spreadsheetml.sheet", "Calisma1.xlsx");
				}
			}
		}

		public List<BlogModel> GetBlogList()  //veriyi static olarka alacağımız
		{
			List<BlogModel> bm = new List<BlogModel>
			{
				new BlogModel{ID=1,BlogName="C# programlama"},
				new BlogModel{ID=2,BlogName="Tesla Firmasının Araçları"},
				new BlogModel{ID=3,BlogName="2020 olimpiyatları"}
			};
			return bm;
		}
	}
}
