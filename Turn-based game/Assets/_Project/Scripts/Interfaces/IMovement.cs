using UnityEngine;

namespace Assets._Project.Scripts.Entity.Interfaces
{
    public interface IMovement
    {
        public void MoveTo(Vector2 position);
        public void StopMove();
    }
}