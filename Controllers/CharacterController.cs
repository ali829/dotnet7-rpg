global using dotnet_rpg.Models;
using dotnet_rpg.DTOs.charachters;
using dotnet_rpg.DTOs.characters;
using Microsoft.AspNetCore.Mvc;


namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            this._characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetCharacterDTO>>> Get()
        {
            return Ok(await _characterService.getAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCharacterDTO>> GetSingle(int id)
        {
            return Ok(await _characterService.getCharacterById(id));
        }
        [HttpPost]
        public async Task<ActionResult<List<GetCharacterDTO>>> CreatedCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.createNewCharacter(newCharacter));
        }
        [HttpPut]
        public async Task<ActionResult<UpdateCharacterDTO>> updateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            var results = await _characterService.updateCharacter(updatedCharacter);
            if (results is null)
            {
                return NotFound(results);
            }
            return Ok(results);
        }

        [HttpDelete]
        public async Task<ActionResult<List<GetCharacterDTO>>> deleteCharacter(int id)
        {
            var results = await _characterService.deleteCharacter(id);
            if (results.Data is null)
                return NotFound(results);
            return Ok(results);
        }
    }
}