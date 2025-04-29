using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public int NextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 3);
    }

    void FixedUpdate()
    {
        // Move
        rigid.velocity = new Vector2(NextMove, rigid.velocity.y);

        // Platform Check
        var frontVec = new Vector2(rigid.position.x + NextMove * 0.2f, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
            Turn();
    }

    void Think()
    {
        // Set Next Active
        NextMove = Random.Range(0, 2) == 0 ? -1 : 1;

        // Sprite Animation
        animator.SetInteger("WalkSpeed", NextMove);
        
        // Filp Sprite
        if (NextMove != 0)
            spriteRenderer.flipX = NextMove == 1;

        // Set Next Active
        var nextThinkTime = Random.Range(2f, 10f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        NextMove *= -1;
        spriteRenderer.flipX = NextMove == 1;

        CancelInvoke();

        Think();
        //Invoke("Think", 5);
    }
}
