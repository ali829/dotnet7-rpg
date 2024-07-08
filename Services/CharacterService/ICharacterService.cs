using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.DTOs.charachters;
using dotnet_rpg.DTOs.characters;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ResponseService<List<GetCharacterDTO>>> getAllCharacters();
        Task<ResponseService<GetCharacterDTO>> getCharacterById(int id);
        Task<ResponseService<List<GetCharacterDTO>>> createNewCharacter(AddCharacterDto newCharacter);
    }
}