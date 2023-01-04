using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 7.5f;
    [SerializeField] private float runSpeed = 11.5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float lookSpeed = 5f;
    [SerializeField] private float lookXLimit = 10f;
    [SerializeField] private float gravity = 20f;
    [SerializeField] private Camera playerCamera;
    CharacterController _characterController;
    Vector3 moveDirection = Vector3.zero;
    private float rotationX;
    [HideInInspector] public bool canMove;
    
    void Start()
    {
        canMove = true;
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && _characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        
        if (!_characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        _characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

    }
}
