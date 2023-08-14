using UnityEngine;
using Utility;

namespace Assets._Project.Scripts.CameraControl
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _cameraSpeed;

        private float _screenWidth = Screen.width;
        private float _screenHeight = Screen.height;

        private float _screenMiddleWidth;
        private float _screenMiddleHeight;

        private void Start()
        {
            _screenMiddleWidth = Screen.width/2;
            _screenMiddleHeight = Screen.height/2;
        }

        private void Update()
        {
            var mouseRelativeToCentreWidth = (Input.mousePosition.x - _screenMiddleWidth)/_screenMiddleWidth;
            var mouseRelativeToCentreHeigth = (Input.mousePosition.y - _screenMiddleHeight)/ _screenMiddleHeight;

            Vector3 direction = new Vector3(
                Input.mousePosition.x >= _screenWidth ? 1 : 
                    Input.mousePosition.x <= 0 ? -1 : 0,
                Input.mousePosition.y >= _screenHeight ? 1 : 
                    Input.mousePosition.y <= 0 ? -1 : 0);

            //Vector3 direction = new Vector3(mouseRelativeToCentreWidth, mouseRelativeToCentreHeigth);

            Move(direction);
        }

        private void Move(Vector3 direction)
        {
            _camera.transform.position += direction * _cameraSpeed * Time.deltaTime;
        }
    }
}
