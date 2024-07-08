using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.DTOs.charachters;
using dotnet_rpg.DTOs.characters;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>(){
            new Character(),
            new Character {Id = 1,Name = "ali"}
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ResponseService<List<GetCharacterDTO>>> createNewCharacter(AddCharacterDto newCharacter)
        {
            var responseService = new ResponseService<List<GetCharacterDTO>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(e => e.Id) + 1;
            characters.Add(character);
            responseService.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return responseService;
        }

        public async Task<ResponseService<List<GetCharacterDTO>>> getAllCharacters()
        {
            var responseService = new ResponseService<List<GetCharacterDTO>>();
            responseService.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return responseService;
        }

        public async Task<ResponseService<GetCharacterDTO>> getCharacterById(int id)
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
            var responseService = new ResponseService<GetCharacterDTO>();
            if (character is not null)
            {
                responseService.Data = _mapper.Map<GetCharacterDTO>(character);
                return responseService;
            }
            throw new Exception("character does not exist");
        }

        public async Task<ResponseService<GetCharacterDTO>> updateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            var responseService = new ResponseService<GetCharacterDTO>();
            try
            {
                var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
                if (character is null)
                    throw new Exception($"The character with the id '{updatedCharacter.Id}' can not be found");

                _mapper.Map(updatedCharacter, character);

                responseService.Data = _mapper.Map<GetCharacterDTO>(character);
            }
            catch (Exception ex)
            {
                responseService.Success = false;
                responseService.Message = ex.Message;
            }
            return responseService;
        }
    }
}