using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController _gameController;
    private Rigidbody2D _playerRb;

    [Header("Jump")]
    [SerializeField] private LayerMask _layerJumpMask = default;
    [SerializeField] private Transform _groundCheckL = default;
    [SerializeField] private Transform _groundCheckR = default;
    [SerializeField] [Range(1.0f, 2.0f)] private float _distance = 1.0f;
    [SerializeField] [Range(500.0f, 1000.0f)] private float _jumpForce = 800.0f;
    private bool _isGrounded = true;

    [SerializeField] [Range(1.0f, 10.0f)]private float _speedPlayer = 5.0f;
    private float _horizontalInput = 0.0f;
    
    void Start()
    {
        _gameController = FindObjectOfType<GameController>();

        _playerRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        Jump();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.Raycast(_groundCheckL.position, Vector2.down, _distance, _layerJumpMask)
        || Physics2D.Raycast(_groundCheckR.position, Vector2.down, _distance, _layerJumpMask);
    }

    void Movement()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        transform.Translate(Vector2.right * _horizontalInput * _speedPlayer * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _playerRb.AddForce(Vector2.up * _jumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            _gameController.UpdateCoin();
            _gameController.StartVfxCoinCollected(other.transform);

            Destroy(other.gameObject);
        }
    }
}
