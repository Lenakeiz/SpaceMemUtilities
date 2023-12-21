using UnityEngine;

namespace SpaceMem.General
{
    public class CameraDebugController : MonoBehaviour
    {
        public float speed = 2.0f;  // Speed of camera movement
        public float sensitivity = 2.0f; // Mouse sensitivity

        private float _pitch = 0.0f;  // Pitch of the camera (looking up and down)
        private float _yaw = 0.0f;  // Yaw of the camera (looking left and right)

        void Start()
        {

        }

        void Update()
        {
            // Get input from arrow keys
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            // Calculate movement direction based on parent's local axes
            Vector3 moveDirection = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;

            // Apply movement in local space
            transform.localPosition += moveDirection * speed * Time.deltaTime;

            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            // Adjust pitch and yaw values based on mouse input
            _yaw += mouseX;
            _pitch -= mouseY;

            // Limit the pitch to prevent the camera from flipping over
            _pitch = Mathf.Clamp(_pitch, -90.0f, 90.0f);

            // Apply rotation as local rotation to the transform
            transform.localRotation = Quaternion.Euler(_pitch, _yaw, 0.0f);

            // Move up and down
            if (Input.GetKey(KeyCode.Q))
            {
                transform.localPosition += transform.up * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.localPosition -= transform.up * speed * Time.deltaTime;
            }

            Debug.DrawLine(transform.position, transform.position + transform.forward * 2.0f, Color.red);
        }
    }
}
