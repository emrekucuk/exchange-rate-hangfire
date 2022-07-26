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
    public class CurenciesController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CurenciesController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<List<CurrencyDto>> All()
        {
            var entities = await _applicationDbContext.Currencies
                                    .ToListAsync();

            // Mapping to Dto
            var dtoModels = new List<CurrencyDto>();
            entities?.ForEach((entity) =>
            {
                dtoModels.Add(new CurrencyDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Code = entity.Code,
                });
            });

            return dtoModels;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<Currency> Create(CurrencyDto currency)
        {
            // Verify That the Currency Name or Code Does Not Already Exist
            var isThereAnyCurrency = await _applicationDbContext.Currencies.AnyAsync(c => c.Name.ToLower() == currency.Name.ToLower() || c.Code.ToLower() == currency.Code.ToLower());
            if (isThereAnyCurrency)
                throw new Exception("Currency Name or Code Already Exist");

            // Create Currency
            var createCurrency = new Currency()
            {
                Name = currency.Name,
                Code = currency.Code,
            };

            // Add and Save Database
            _applicationDbContext.Currencies.Add(createCurrency);
            await _applicationDbContext.SaveChangesAsync();

            return createCurrency;
        }

        [AllowAnonymous]
        [HttpPut("[action]")]
        public async Task<Currency> Update(CurrencyDto currency)
        {
            // Verify That the Currency Name or Code Does Not Already Exist
            var isThereAnyCurrency = await _applicationDbContext.Currencies.AnyAsync(c => c.Id != currency.Id && (c.Name.ToLower() == currency.Name.ToLower() || c.Code.ToLower() == currency.Code.ToLower()));
            if (isThereAnyCurrency)
                throw new Exception("Currency Name or Code Already Exist");

            // Get Curency
            var oldEntity = await _applicationDbContext.Currencies.FirstOrDefaultAsync(c => c.Id == currency.Id);
            if (oldEntity == null)
                throw new Exception("Currency Not Found");

            // Set Properties
            oldEntity.Name = currency.Name;
            oldEntity.Code = currency.Code;

            // Update and Save Database
            _applicationDbContext.Currencies.Update(oldEntity);
            await _applicationDbContext.SaveChangesAsync();

            return oldEntity;
        }

        [AllowAnonymous]
        [HttpDelete("[action]")]
        public async Task<Currency> Delete(Guid currencyId)
        {
            var deleteEntity = await _applicationDbContext.Currencies.FirstOrDefaultAsync(c => c.Id == currencyId);
            if (deleteEntity == null)
                throw new Exception("Currency Not Found");

            // Remove
            _applicationDbContext.Currencies.Remove(deleteEntity);
            await _applicationDbContext.SaveChangesAsync();

            return deleteEntity;
        }
    }
}
