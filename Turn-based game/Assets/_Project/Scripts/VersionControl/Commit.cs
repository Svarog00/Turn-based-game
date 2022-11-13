using Assets._Project.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets._Project.Scripts.VersionControl
{
    public class Commit : IEquatable<Commit>
    {
        public readonly Guid Guid;

        public Commit Previous;
        public List<Commit> Next;

        //Data
        public readonly int CurrentCharacterIndexTurn;
        public readonly List<CharacterWorldData> CharactersData;

        public Commit(int currentCharacterIndexTurn, IEnumerable<CharacterWorldData> charactersWorldData)
        {
            Guid = Guid.NewGuid();

            CharactersData = charactersWorldData.ToList();
            CurrentCharacterIndexTurn = currentCharacterIndexTurn;
        }

        public bool Equals(Commit other)
        {
            if(CurrentCharacterIndexTurn != other.CurrentCharacterIndexTurn)
            {
                return false;
            }

            for(int i = 0; i < CharactersData.Count; i++)
            {
                if (!CharactersData[i].Equals(other.CharactersData[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
