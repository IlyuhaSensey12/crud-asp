using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Model
{
	[Table("person")]
	public class Person
	{
		[Key]
		[Column("person_id")]
		public int Id { get; set; }
		public String name { get; set; }
		public int age { get; set; }
		public String country { get; set; }

	}

	public class Company
	{
		[Key]
		[Column("company_id")]
		public int Id { get; set; }
		public String name { get; set; }
	}
}

