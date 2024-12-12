using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PlayerMovement : MonoBehaviour
    {
        [Range(1f, 20f)]
        [SerializeField] private float _movementSpeed;
        [Tooltip("run multiplier of the movement speed")]
        [Range(1f, 20f)]
        [SerializeField] private float _runMultiplier;
        [SerializeField] private float _gravity = -9.81f;
        [Range(1f, 20f)]
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _groundCheckRadius = 0.3f;
        [SerializeField] private float _groundCheckDistance = 0.5f;
        [SerializeField] private LayerMask _groundMask = -1;
        public Vector3 moveVelocity;

        public CharacterController characterController;
        public Vector3 _controllerVelocity;

        public bool isGrounded; // New variable to track grounded state

        // Start is called before the first frame update
        void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            // Use raycasting to determine if the player is grounded
            isGrounded = CheckIfGrounded();

            // stops the y velocity when player is on the ground and the velocity has reached 0
            if (isGrounded && _controllerVelocity.y < 0)
            {
                _controllerVelocity.y = -2f;
            }

            // Get the movement input
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            // Get the camera's forward and right directions
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;

            // Flatten the vectors to ignore the y-axis
            forward.y = 0;
            right.y = 0;

            // Normalize the vectors to ensure consistent movement speed
            forward.Normalize();
            right.Normalize();

            // Calculate the desired movement direction based on camera orientation
            Vector3 desiredMoveDirection = (right * moveX + forward * moveZ).normalized;

            // Calculate movement velocity
            moveVelocity = desiredMoveDirection * _movementSpeed;

            // gravity affects the controller on the y-axis
            _controllerVelocity.y += _gravity * Time.deltaTime;

            // moves the controller on the y-axis
            characterController.Move(_controllerVelocity * Time.deltaTime);

            // the controller is able to jump when on the ground
            if (Input.GetButton("Jump") && characterController.isGrounded)
            {
                _controllerVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }

            // the controller is able to run
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveVelocity *= _runMultiplier;
            }

            Vector3 finalVelocity = new Vector3(moveVelocity.x, _controllerVelocity.y, moveVelocity.z);

            characterController.Move(finalVelocity * Time.deltaTime);
        }

        private bool CheckIfGrounded()
        {
            Vector3 spherePosition = transform.position + (Vector3.down * (characterController.height / 2 - characterController.radius));
            if (Physics.SphereCast(spherePosition, _groundCheckRadius, Vector3.down, out RaycastHit hit, _groundCheckDistance, _groundMask))
            {
                float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

                if (slopeAngle <= characterController.slopeLimit)
                {
                    return true;
                }
            }

            return false;
        }
    }

