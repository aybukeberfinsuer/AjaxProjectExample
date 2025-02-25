using AjaxProjectExample.Entities;
using Microsoft.EntityFrameworkCore;

namespace AjaxProjectExample.Context
{
	public class CustomerContext:DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			
			optionsBuilder.UseSqlServer("Server=LAPTOP-JKIESAIB; initial Catalog=CustomerAjaxDb; integrated Security=true");
			
		}

		public DbSet<Customer> Customers { get; set; }
	}
}
