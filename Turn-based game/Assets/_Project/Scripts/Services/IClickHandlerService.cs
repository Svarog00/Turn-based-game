using UnityEngine;

namespace Services
{
    public interface IClickHandlerService
    {
        public bool IsLeftButtonDown();
        public bool IsRightButtonDown();

        public bool IsLeftButtonDown(out Vector3 mousePoistion);
        public bool IsRightButtonDown(out Vector3 mousePoistion);
        public GameObject GetObjectFromClick();
    }
}