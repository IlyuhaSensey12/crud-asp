using System;
namespace CRUD.Model
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
        public String Country { get; set; }

    }
    public class CompanyViewModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class CustomerViewModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class OrderViewModel
    {
        public int Id { get; set; }
        public String OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}

