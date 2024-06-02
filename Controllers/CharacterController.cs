global using dotnet_rpg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult<List<Character>> Get()
        {
            return Ok(_characterService.getAllCharacters());
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetSingle(int id)
        {
            return Ok(_characterService.getCharacterById(id));
        }
        [HttpPost]
        public ActionResult<List<Character>> CreatedCharacter(Character newCharacter)
        {
            return Ok(_characterService.createNewCharacter(newCharacter));
        }
    }
}