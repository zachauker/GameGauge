using Application.Companies;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CompaniesController
{
    public class GamesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetCompanies()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Company>> GetCompany(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }
    }
}