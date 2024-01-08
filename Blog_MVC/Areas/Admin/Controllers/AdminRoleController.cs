using Blog_MVC.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Blog_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AdminRoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public AdminRoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }



        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }



        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel model)   //viewmodel olduğu için ekleme işlemi farklı
        {
            if (ModelState.IsValid)  //eğer modelstate geçerliyse
            {
                AppRole role = new AppRole
                {
                    Name = model.name
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }



        [HttpGet]
        public IActionResult UpdateRole(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);   //Girilen id ile _roleManager.Roles'daki Id eşitse bu rolü values adlı bir değişkene atar.

            RoleUpdateViewModel model = new RoleUpdateViewModel  //RoleUpdateViewModelden yeni bir model oluşturulur ve bu model, values nesnesinin Id ve Name özellikleriyle doldurulur
            {
                Id = values.Id,
                name = values.Name
            };
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleUpdateViewModel model)
        {
            var values = _roleManager.Roles.Where(x => x.Id == model.Id).FirstOrDefault();  //güncelleyeceğim değeri hafızaya aldım
                                                                                            //_roleManager'ın Roles listesinde model.Id ile eşleşen  rolü values'e atar

            values.Name = model.name;                                                       //atamamı yaptım  //model nesnesinin name'i, values nesnesinin Name'ine atanır
                                                                                            //bunu yapmalısın çünkü direk db ile çalışmıyorsun arada modelview var!!!!!!!!!!!!!!!

            var result = await _roleManager.UpdateAsync(values);                             //valuesden gelen değer update olacak(veritabanında)

            if (result.Succeeded)
            {
                return RedirectToAction("Index");  //başarılı ise indexe yönlendir
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x=>x.Id==id);                    //dışarıdan gönderdiğim id değerine Id eşit olanı hafızaya al
            var result = await _roleManager.DeleteAsync(values);                            //veritabanından silme işlemi

            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
