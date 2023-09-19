using System;
using CRUD.Model;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PersonController : ControllerBase
	{
		private readonly AppDbContext _dbContext;

		public PersonController(AppDbContext appDbContext)
		{
			_dbContext = appDbContext;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Person>> GetPersons()
		{
			return _dbContext.Persons;
		}

		[HttpGet("{personId:Int}")]
		public async Task<ActionResult<Person>> GetById(int personId)
		{
			var person = await _dbContext.Persons.FindAsync(personId);
			return person;
		}

		[HttpPost]
		public async Task<ActionResult> Create(Person person)
		{
			await _dbContext.Persons.AddAsync(person);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}

        [HttpPut]
        public async Task<ActionResult> Update(Person person)
        {
            _dbContext.Persons.Update(person);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

		[HttpDelete("{personId:int}")]
        public async Task<ActionResult> Delete(int personId)
		{
			var person = await _dbContext.Persons.FindAsync(personId);
			_dbContext.Persons.Remove(person);
			await _dbContext.SaveChangesAsync();
			return Ok();
		}
    }
}

