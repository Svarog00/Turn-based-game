using System;
using UnityEngine;

namespace Assets._Project.Scripts.Data
{
    [Serializable]
    public class CharacterWorldData : IEquatable<CharacterWorldData>
    {
        public Vector2Surrogate Position;

        public int HealthPoints;

        public bool Equals(CharacterWorldData other)
        {
            if(Position.X != other.Position.X || Position.Y != other.Position.Y)
            {
                return false;
            }

            if(HealthPoints != other.HealthPoints)
            {
                return false;
            }

            return true;
        }
    }

    [Serializable]
    public struct Vector2Surrogate
    {
        public float X;
        public float Y;

        public Vector2Surrogate(Vector2 vector)
        {
            X = vector.x;
            Y = vector.y;
        }
    }
}
