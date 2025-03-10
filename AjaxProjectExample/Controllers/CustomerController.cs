using AjaxProjectExample.Context;
using AjaxProjectExample.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AjaxProjectExample.Controllers
{
	public class CustomerController : Controller
	{
		private CustomerContext _context;

		public CustomerController(CustomerContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}


		public IActionResult CustomerList()
		{
			var values=_context.Customers.ToList();
			var jsonValues=JsonConvert.SerializeObject(values);
			return Json(jsonValues);
		}

		public IActionResult CreateCustomer(Customer customerObject)
		{
			_context.Customers.Add(customerObject);
			_context.SaveChanges();
			var values = JsonConvert.SerializeObject(customerObject);
			return Json(values);

		}

		public IActionResult DeleteCustomer(int id)
		{
			var value = _context.Customers.Find(id);
			_context.Customers.Remove(value);
			_context.SaveChanges();
			return NoContent();
		}
		public IActionResult GetCustomer(int id)
		{
			var value = _context.Customers.Find(id);
			var jsonValues = JsonConvert.SerializeObject(value);
			return Json(jsonValues);
		}

		[HttpPut]
		public IActionResult EditCustomer([FromBody] Customer customer)
		{
			try
			{
				if (customer == null || customer.CustomerId == 0)
				{
					return BadRequest("Geçersiz müşteri verisi.");
				}

				var existingCustomer = _context.Customers.Find(customer.CustomerId);
				if (existingCustomer == null)
				{
					return NotFound("Müşteri bulunamadı.");
				}

				existingCustomer.CustomerName = customer.CustomerName;
				existingCustomer.CustomerSurname = customer.CustomerSurname;

				_context.SaveChanges();
				return Ok("Müşteri güncellendi.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Hata: {ex.Message} - {ex.InnerException?.Message}");
			}
		}



	}
}
