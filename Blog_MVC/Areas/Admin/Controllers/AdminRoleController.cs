using Blog_MVC.Areas.Admin.Models;
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
        private readonly UserManager<AppUser> _userManager;

        public AdminRoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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

        public IActionResult UserRoleList()    //kullanıcılar ve rolleri görüntülenecek. Rol ataması yapılacak
        {
            var values = _userManager.Users.ToList();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> AssingRole(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x=>x.Id==id);    //verdiğimiz id değerine denk gelen kullanıcıyı user'a atadı
            var roles = _roleManager.Roles.ToList();                        //şartsız tüm rolleri roles'e atadı

            TempData["Userid"] = user.Id;

            var userRoles = await _userManager.GetRolesAsync(user);   //kullanıcıya atanan rolleri çektik   //aspnetuserroles tablosunda kullancının ıdsine denk gelen kaç role varsa onları döndü

            List<RoleAssignViewModel> model = new List<RoleAssignViewModel>();

            foreach(var item in roles)      //aspnetuserroles tablosundaki tüm rolleri sırayla alır. Bu rollerin id,isim ve exists(true,false) değerlerini RoleAssingviewmodeldeki değerlere atar.
            {
                RoleAssignViewModel m = new RoleAssignViewModel();
                m.RoleID = item.Id;
                m.Name = item.Name;
                m.Exists = userRoles.Contains(item.Name);
                model.Add(m);
            }
            return View(model);      //model içerisinde 4 tane değer var(roller). Bunların içerisinde exists durumu true olanlar tikli gelecek AssingRole.cshtml'de
        }
    }
}
