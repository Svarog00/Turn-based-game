using UnityEngine;

namespace Services
{
    public class ClickHandlerService : IClickHandlerService
    {
        private const int LeftMouseButtonCode = 0;
        private const int RightMouseButtonCode = 1;

        private LayerMask _entityLayerMask;

        public ClickHandlerService(LayerMask layerMask)
        {
            _entityLayerMask = layerMask;
        }

        public bool IsLeftButtonDown()
        {
            return Input.GetMouseButtonDown(LeftMouseButtonCode);
        }

        public bool IsRightButtonDown()
        {
            return Input.GetMouseButtonDown(RightMouseButtonCode);
        }

        public bool IsLeftButtonDown(out Vector3 mousePosition)
        {
            mousePosition = Utility.UtilitiesClass.GetWorldMousePosition();
            return IsLeftButtonDown();
        }

        public bool IsRightButtonDown(out Vector3 mousePosition)
        {
            mousePosition = Utility.UtilitiesClass.GetWorldMousePosition();
            return IsRightButtonDown();
        }

        public GameObject GetObjectFromClick()
        {
            Vector3 mousePosition = Utility.UtilitiesClass.GetWorldMousePosition();
            Collider2D[] targetedGameObjects = Physics2D.OverlapCircleAll(mousePosition, 0.1f, _entityLayerMask);

            return targetedGameObjects.Length > 0 ?
                targetedGameObjects[0].gameObject :
                null;
        }
    }
}