using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public record MouseClickData
    {
        public List<Collider2D> ClickedObjects;
        public Vector3 ClickPosition;
    }
}