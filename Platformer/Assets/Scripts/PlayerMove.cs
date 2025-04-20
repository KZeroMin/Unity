using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float MaxSpeed;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Stop Speed
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        // Direction Sprite
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            animator.SetBool("isWalking", false);
        else
            animator.SetBool("isWalking", true);

    }

    private void FixedUpdate() // Default 설정: 1초에 50번 실행
    {
        // Move Speed
        var h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > MaxSpeed) // Right Max Speed
            rigid.velocity = new Vector2(MaxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < MaxSpeed * -1)
            rigid.velocity = new Vector2(MaxSpeed * -1, rigid.velocity.y);
    }
}
