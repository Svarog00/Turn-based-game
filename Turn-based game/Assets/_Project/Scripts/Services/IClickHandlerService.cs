using UnityEngine;

namespace Services
{
    public interface IClickHandlerService
    {
        public bool IsLeftButtonDown();
        public bool IsRightButtonDown();

        public MouseClickData GetClickData();
    }
}