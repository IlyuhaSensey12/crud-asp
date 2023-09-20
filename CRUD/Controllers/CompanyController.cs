using System;
using System.ComponentModel.Design;
using CRUD.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
	{
        private readonly AppDbContext _dbContext;
        public CompanyController(AppDbContext appDbContext)
		{
            _dbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies(
            string sortField = "Name",
            string sortOrder = "asc",
            string filterByName = null)
        {
       
            var queryable = _dbContext.Companies.AsQueryable();

            if (!string.IsNullOrEmpty(filterByName))
            {
                queryable = queryable.Where(c => c.Name.Contains(filterByName));
            }

            if (sortField == "Name")
            {
                queryable = sortOrder == "asc"
                    ? queryable.OrderBy(c => c.Name)
                    : queryable.OrderByDescending(c => c.Name);
            }

            var companies = await queryable.ToListAsync();
            return Ok(companies);
        }


        [HttpGet("{companyId:Int}")]
        public async Task<ActionResult<Company>> GetById(int companyId)
        {
            if (companyId == null)
            {
                return NotFound();
            }
            var company = await _dbContext.Companies.FindAsync(companyId);
            return company;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Company company)
        {
            await _dbContext.Companies.AddAsync(company);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(Company company)
        {
            _dbContext.Companies.Update(company);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{companyId:int}")]
        public async Task<ActionResult> Delete(int companyId)
        {
            if (companyId == null)
            {
                return NotFound();
            }
            var company = await _dbContext.Companies.FindAsync(companyId);
            _dbContext.Companies.Remove(company);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}

