using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelterApi.Models;

namespace ShelterApi.Controllers
{
    [Route("queries")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly AnimalshelterContext _context;

        public QueriesController(AnimalshelterContext context)
        {
            _context = context;
        }

        

        [HttpGet("allSheltersWithAnimals")]
        public async Task<IActionResult> AllSheltersWithAnimals()
        {
            try
            {
                var shelters = await _context.Shelters
                    .Include(s => s.Animals)
                    .ToListAsync();

                return StatusCode(200, new
                {
                    message = "a menhelyek és allataik sikeresen lekerdezve",
                    result = shelters
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "hiba tortent a lekerdezes soran",
                    result = ex.Message
                });
            }
        }


        [HttpGet("allAnimalsWithShelter")]
        public async Task<IActionResult> AllAnimalsWithShelter()
        {
            try
            {
                var animals = await (
                    from a in _context.Animals
                    join s in _context.Shelters
                    on a.ShelterId equals s.Id
                    select new
                    {
                        AnimalName = a.Name,
                        Species = a.Species,
                        Age = a.Age,
                        Weight = a.Weight,
                        ShelterName = s.Name
                    }
                ).ToListAsync();
                return StatusCode(200, new
                {
                    message = "allatok és menhelyeik sikeresen lekerdezve",
                    result = animals
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "hiba tortent a lekerdezes soran",
                    result = ex.Message
                });
            }


        }

      
        [HttpGet("animalsOfShelter")]
        public async Task<IActionResult> AnimalsOfShelter(Guid shelterId)
        {

            try
            {
                var shelter = await _context.Shelters
                    .Include(s => s.Animals)
                    .Where(s => s.Id == shelterId)
                    .FirstOrDefaultAsync();

                if (shelter == null)
                {
                    return StatusCode(404, new
                    {
                        message = "a menhely nem letezik",
                        result = shelter
                    });
                }

                return StatusCode(200, new
                {
                    message = "menhely és allatai skieresen lekerdezve",
                    result = shelter
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "hiba tortent a lekerdezes soran",
                    result = ex.Message
                });
            }
        }


        [HttpGet("animalCountByShelter")]

        public async Task<IActionResult> AnimalCountByShelter()
        {
            try
            {

                var result = await _context.Shelters
                    .Select(s => new
                    {
                        ShelterName = s.Name,
                        City = s.City,
                        AnimalCount = s.Animals.Count()
                    })
                    .OrderByDescending(s => s.AnimalCount).ToListAsync();

                return StatusCode(200, new
                {
                    message = "az allatok szama menhelyenként sikeresen lekerdezve",
                    result = result
                });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "hiba tortent a lekerdezes soran",
                    result = ex.Message
                });
            }
        }
    }
}
