using System;
using UnityEngine;

namespace Assets._Project.Scripts.Data
{
    [Serializable]
    public class CharacterWorldData
    {
        public Vector2Surrogate Position;

        public int HealthPoints;
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
