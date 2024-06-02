using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>(){
            new Character(),
            new Character {Id = 1,Name = "ali"}
        };
        public List<Character> createNewCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }

        public List<Character> getAllCharacters()
        {
            return characters;
        }

        public Character getCharacterById(int id)
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
            if (characters is not null)
            {

                return character;
            }
            throw new Exception("character does not exist");
        }
    }
}