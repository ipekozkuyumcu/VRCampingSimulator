using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    public CharacterController character;
    
    void Start()
    {
        
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 forward = transform.right * x + transform.forward * z;
        character.SimpleMove(forward * speed);
    }
}
