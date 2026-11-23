using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShelterApi.Models;
using ShelterApi.Models.DTOs;



namespace ShelterApi.Controllers
{
    [Route("animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalshelterContext _context;
        public AnimalsController(AnimalshelterContext context)
        {
            _context = context;
        }
      
        [HttpPost("newAnimal")]
        public async Task<IActionResult> NewAnimal([FromBody] NewAnimalDto dto)
        {
            try
            {
                var shelter = await _context.Shelters.FindAsync(dto.ShelterId);

                if (shelter == null)
                {
                    return StatusCode(404, new
                    {
                        message = "a menhely nem talalhato",
                        result = shelter
                    });
                }

                var animal = new Animal
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Species = dto.Species,
                    Age = dto.Age,
                    Weight = dto.Weight,
                    ShelterId = dto.ShelterId,
                    RegTime = DateTime.Now
                };
                await _context.Animals.AddAsync(animal);
                await _context.SaveChangesAsync();

                return StatusCode(201, new
                {
                    message = "allat sikeresen hozzáadva!",
                    result = animal
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "hiba tortént a hozzaadás közbe",
                    result = ex.Message
                });
            }
        }

        
        [HttpGet("allAnimal")]
        public async Task<IActionResult> GetAllAnimal()
        {
         try
            {
                var animals = await _context.Animals.ToListAsync();
                if (animals.Count == 0)
                {
                    return StatusCode(404, new
                    {
                        message = "nincs egy allat sem az adatbazisban",
                        result = animals
                    });

                }
                return StatusCode(200, new
                {

                    message = "sikeres lekerdezes",
                    result = animals
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {

                    message = "hiba tortent a lekerdezeskor",
                    result = ex.Message
                });
            }
      }
        [HttpGet("byId")]
        public async Task<IActionResult> ById(Guid id)
        {
            try
            {
                var animal = await _context.Animals.FindAsync(id);

                if (animal == null)
                {
                    return StatusCode(404, new
                    {
                        message = "az allat nem talalhato",
                        result = animal
                    });
                }

                return StatusCode(200, new
                {
                    message = "allat sikeresen lekerdezve",
                    result = animal
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Hiba tortént a lekérdezéskor",
                    result = ex.Message
                });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAnimalDto dto)
        {
            try
            {
                var animal = await _context.Animals.FindAsync(id);
                if (animal == null)
                {
                    return StatusCode(404, new
                    {
                        message = "az allat nem talalhato",
                        result = animal
                    });
                }
                var shelter = await _context.Shelters.FindAsync(dto.ShelterId);
                if (shelter == null)
                {
                    return StatusCode(404, new
                    {
                        message = "a menhely nem talalhato",
                        result = shelter
                    });
                }
                animal.Name = dto.Name;
                animal.Species = dto.Species;
                animal.Age = dto.Age;
                animal.Weight = dto.Weight;
                animal.ShelterId = dto.ShelterId;
                await _context.SaveChangesAsync();
                return StatusCode(200, new
                {
                    message = "allát sikeresen modositva",
                    result = animal
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "hiba tortént a modositás során",
                    result = ex.Message
                });
            }
        }



        [HttpDelete("deleteAnimal")]
        public async Task<IActionResult> DeleteAnimal(Guid id)
        {
            try
            {
                var animal = await _context.Animals.FindAsync(id);
                if (animal == null)
                {
                    return StatusCode(404, new
                    {
                        message = "Az allat nem talalhato sajnos",
                        result = animal
                    });
                }

                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();

                return StatusCode(200, new
                {
                    message = "állat sikeresen  torolve",
                    result = animal
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "hiba tortent a torles soran",
                    result = ex.Message
                });
            }
        }
    }
}