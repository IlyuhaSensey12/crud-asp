using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Model
{
	[Table("persons")]
	public class Person
	{
		[Key]
		[Column("person_id")]
		public int Id { get; set; }
		public String Name { get; set; }
		public int Age { get; set; }
		public String Country { get; set; }
	}

	[Table("companies")]
	public class Company
	{
		[Key]
		[Column("company_id")]
		public int Id { get; set; }
		public String Name { get; set; }
	}

	[Table("customers")]
	public class Customer
	{
        [Key]
		[Column("customer_id")]
        public int Id { get; set; }
		public String Name { get; set; }
	}

	[Table("orders")]
	public class Order
	{
		[Key]
		[Column("order_id")]
		public int Id { get; set; }
		public String OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }

}

