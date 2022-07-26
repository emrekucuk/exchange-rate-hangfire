using Domain.Entites;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace exchange_rate_hangfire.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CurrencyExchangesController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CurrencyExchangesController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<List<CurrencyExchangeDto>> All()
        {
            var entities = await _applicationDbContext.CurrencyExchanges
                                    .Include(c => c.Currency)
                                    .ToListAsync();
            // Mapping to Dto
            var dtoModels = new List<CurrencyExchangeDto>();
            entities?.ForEach((entity) =>
            {
                dtoModels.Add(new CurrencyExchangeDto()
                {
                    Id = entity.Id,
                    CurrencyName = entity.Currency.Name,
                    Value = entity.Value,
                    Date = entity.Date,
                });
            });
            return dtoModels;
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<List<CurrencyExchangeDto>> Today()
        {
            var entities = await _applicationDbContext.CurrencyExchanges
                                    .Where(c => c.Date.Day == DateTime.Today.Day)
                                    .Include(c => c.Currency)
                                    .ToListAsync();
            // Mapping to Dto
            var dtoModels = new List<CurrencyExchangeDto>();
            entities?.ForEach((entity) =>
            {
                dtoModels.Add(new CurrencyExchangeDto()
                {
                    Id = entity.Id,
                    CurrencyName = entity.Currency.Name,
                    Value = entity.Value,
                    Date = entity.Date,
                });
            });

            return dtoModels;
        }

    }
}
