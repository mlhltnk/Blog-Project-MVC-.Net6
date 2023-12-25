using BlogApiDemo.Domain.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApiDemo.Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public IActionResult EmployeeList()
        {
            using var c = new Context();
            var values = c.Employees.ToList();
            return Ok(values);   //API'lar defaultta Ok ile 200 değeri döndürülür.
        }

        [HttpPost]

        public IActionResult EmployeeAdd(Employee employee)    //ekleme işlemi
        {
            using var c = new Context();
            c.Add(employee);
            c.SaveChanges();
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult EmployeeGet(int id)    //idye göre veri getirme
        {
            using var c = new Context();
            var employee = c.Employees.Find(id);  //idyi bul
			if (employee == null)
            {
                return NotFound();   //olmayan id gelirse notfound döndür(404)
            }
            else
            {
                return Ok(employee);
            }
        }

        [HttpDelete("{id}")]        //silme işlemi
        public IActionResult EmployeeDelete(int id)
        {
            using var c = new Context();
            var employee=c.Employees.Find(id);   //idyi bul
            if(employee==null)
            {
				return NotFound();
			}
            else
            {
                c.Remove(employee);
                c.SaveChanges();
                return Ok();
            }
        }

        [HttpPut]
        public IActionResult EmployeeUpdate(Employee employee)  //güncelleme işlemi  //swaggerda hem id hem name alanını değişmemiz gerekecek
        {
            using var c = new Context();
            var emp = c.Find<Employee>(employee.ID);  //idyi bul
			if (employee == null)
			{
				return NotFound();
			}
			else
            {
                emp.Name=employee.Name;
                c.Update(emp);
                c.SaveChanges();
                return Ok();
            }


		}
        
    }
}
