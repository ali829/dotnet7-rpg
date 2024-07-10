using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.DTOs.charachters;
using dotnet_rpg.DTOs.characters;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly RpgContext _DbContext;

        public CharacterService(IMapper mapper, RpgContext DbContext)
        {
            _DbContext = DbContext;
            _mapper = mapper;
        }
        public async Task<ResponseService<List<GetCharacterDTO>>> createNewCharacter(AddCharacterDto newCharacter)
        {
            var responseService = new ResponseService<List<GetCharacterDTO>>();
            var character = _mapper.Map<Character>(newCharacter);
            await _DbContext.AddAsync(character);
            await _DbContext.SaveChangesAsync();
            List<Character> characters = _DbContext.Characters.ToList();
            responseService.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return responseService;
        }



        public async Task<ResponseService<List<GetCharacterDTO>>> getAllCharacters()
        {
            var responseService = new ResponseService<List<GetCharacterDTO>>();
            List<Character> characters = _DbContext.Characters.ToList();
            responseService.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return responseService;
        }

        public async Task<ResponseService<GetCharacterDTO>> getCharacterById(int id)
        {
            var character = await _DbContext.Characters.FirstOrDefaultAsync(c => c.Id == id);
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
                var character = await _DbContext.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (character is null)
                    throw new Exception($"The character with the id '{updatedCharacter.Id}' can not be found");


                _DbContext.Characters.Update(_mapper.Map(updatedCharacter, character));
                await _DbContext.SaveChangesAsync();

                responseService.Data = _mapper.Map<GetCharacterDTO>(character);
            }
            catch (Exception ex)
            {
                responseService.Success = false;
                responseService.Message = ex.Message;
            }
            return responseService;
        }


        public async Task<ResponseService<List<GetCharacterDTO>>> deleteCharacter(int id)
        {
            ResponseService<List<GetCharacterDTO>> responseService = new ResponseService<List<GetCharacterDTO>>();

            try
            {
                var character = await _DbContext.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if (character is null)
                    throw new Exception($"The character with the id '{id}' can not be found");
                _DbContext.Characters.Remove(character);
                await _DbContext.SaveChangesAsync();
                responseService.Data = _DbContext.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
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