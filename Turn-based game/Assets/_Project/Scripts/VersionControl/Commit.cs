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
        public readonly Turn CurrentTurn;
        public readonly int CurrentCharacterIndexTurn;
        public readonly List<CharacterWorldData> CharactersData;

        public Commit(int currentCharacterIndexTurn, IEnumerable<CharacterWorldData> charactersWorldData, Turn currentTurn)
        {
            Guid = Guid.NewGuid();
            Next = new List<Commit>();

            CurrentTurn = currentTurn;
            CharactersData = charactersWorldData.ToList();
            CurrentCharacterIndexTurn = currentCharacterIndexTurn;
        }

        public bool Equals(Commit other)
        {
            for (int i = 0; i < CharactersData.Count; i++)
            {
                if (!CharactersData[i].Equals(other.CharactersData[i]))
                {
                    return false;
                }
            }

            if (CurrentCharacterIndexTurn != other.CurrentCharacterIndexTurn)
            {
                return false;
            }

            return true;
        }
    }
}
