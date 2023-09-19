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
        public ActionResult<IEnumerable<Company>> GetCompanies()
        {
            return _dbContext.Companies;
        }

        [HttpGet("{companyId:Int}")]
        public async Task<ActionResult<Company>> GetById(int companyId)
        {
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
            var company = await _dbContext.Companies.FindAsync(companyId);
            _dbContext.Companies.Remove(company);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}

