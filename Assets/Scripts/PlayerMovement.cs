using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UIElements;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public static PlayerMovement Instance;
    
    float playerHeight = 2f;
    
    [SerializeField] Transform orientation;

    [Header("Gun")] 
    [SerializeField] private Item[] item;

    private int itemIndex;
    private int previusItemIndex = -1;
    
    
    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;
    float movementMultiplier = 10f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Jumping")]
    public float jumpForce = 5f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    public float horizontalMovement;
    public float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;
    public bool isGrounded { get; private set; }
    [HideInInspector] public bool isGrapling;
    [HideInInspector] public bool switchGun;
    [HideInInspector] public bool TakeGun;
    Vector3 moveDirection;
    Vector3 slopeMoveDirection;
    private PhotonView pv;
    Rigidbody rb;
    public Animator _animator;
    RaycastHit slopeHit;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        Instance = this;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
        if (pv.IsMine)
        {
            switchGun = true;
            EquipItem(0);
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
        
    }

    private void Update()
    {
        _animator.SetFloat("Horizontal",horizontalMovement);
        _animator.SetFloat("Vertical",verticalMovement);
        if (!pv.IsMine)
        {
            return;
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        for (int i = 0; i < item.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                switchGun = true;
                EquipItem(i);
                break;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (itemIndex >= item.Length -1)
            {
                switchGun = true;
                EquipItem(0);
            }
            else
            {
                switchGun = true;
                EquipItem(itemIndex + 1);    
            }
        }
        else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (itemIndex <= item.Length -1)
            {
                switchGun = true;
                EquipItem(0);
            }
            else
            {
                switchGun = true;
                EquipItem(itemIndex + 1);    
            }
        }
    }
    
    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        if (!pv.IsMine)
        {
            return;
        }
        MovePlayer();
        if (isGrapling)
        {
            airMultiplier = 1;
        }
        else
        {
            airMultiplier = 0.4f;
        }
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
        else if (isGrapling && !isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
    }

    void EquipItem(int _index)
    {
        itemIndex = _index;
        
        
        if (switchGun)
        { 
            item[itemIndex].itemGameObject.SetActive(true);
            item[itemIndex].itemGameObject.GetComponent<Animator>().Play("Take");
            switchGun = false;
            TakeGun = true;
        }

        if (previusItemIndex != -1)
        {
            if (TakeGun)
            {
                item[previusItemIndex].itemGameObject.SetActive(false);
                TakeGun = false;
            }
        }

        previusItemIndex = itemIndex;
        if (pv.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex",itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!pv.IsMine && targetPlayer == pv.Owner)
        {
            EquipItem((int)changedProps["itemIndex"]);
        }
    }
}