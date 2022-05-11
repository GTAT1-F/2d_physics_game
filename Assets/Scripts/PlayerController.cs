using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    private Vector3 playerVelocity;
    
    private bool groundedPlayer;
    private float playerSpeed = 5.0f;

    public Animator animator;
    public Transform transform;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      Vector3 move = new Vector3(Input.GetAxis("Horizontal"),0, 0);
      
      controller.Move(move * Time.deltaTime * playerSpeed);

      if(Input.GetAxis("Horizontal") != 0)
      {
            Quaternion rotation = Quaternion.LookRotation(move);
            transform.rotation = rotation;
      }

      animator.SetFloat("speed", Mathf.Abs(move.x));

    }
}
