using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [HideInInspector] Rigidbody2D rigidbody;
    [HideInInspector] Collider2D collider;
    [HideInInspector] private float gravity; //重力
    [HideInInspector] const float GRAVITY_SCALE = 9.81f; //重力加速度
    [HideInInspector] private float jump; //ジャンプ力
    [HideInInspector] const float JUMP_HEIGHT = 7f; //最大jump
    [HideInInspector] private int count; //2回ジャンプ
    [HideInInspector] private bool isGround; //地面にいる：true　　空中：false
    [HideInInspector] private bool jumpFlag = false;
    [HideInInspector] private bool jumpFlag2 = false;

    void Start()
    {
        jump = Mathf.Pow(JUMP_HEIGHT * 2 * GRAVITY_SCALE, 0.45f);
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        Debug.Log(isGround);
        PlyMove();
    }

    void FixedUpdate()
    {
        isGround = false;　//空中
    }

    void PlyMove()　//プレイヤーの動き
    {

        Vector2 velocity = rigidbody.velocity;　//プレイヤーの位置

        // ジャンプさせる
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            jumpFlag = true;
        }

        if (jumpFlag && !isGround)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpFlag2 = true;
            }
            if (jumpFlag2)
            {
                jumpFlag2 = false;
            }
            jumpFlag = false;
        }
        if (Input.GetKeyUp(KeyCode.Space) && velocity.y > 0f)
        {
            gravity /= 1.5f;
        }
        GravityControl();
        rigidbody.velocity = new Vector2(0f, gravity);

        if(transform.position.y <= -7)
        {
            SceneManager.LoadScene("Title");
        }
    }

    // 重力の制御
    void GravityControl()
    {
        if (isGround && !jumpFlag)
        {
            gravity = GRAVITY_SCALE * Time.deltaTime;
        }
        else if (jumpFlag)
        {
            if (jumpFlag2)
            {
                gravity = jump;
            }
            else if (!jumpFlag2)
            {
                gravity -= GRAVITY_SCALE * Time.deltaTime;
            }
            gravity = jump;
        }
        else if (!isGround && !jumpFlag)
        {
            gravity -= GRAVITY_SCALE * Time.deltaTime;

        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        for (int i = 0; i < col.contacts.Length; i++)　//触れた瞬間から離れるまで地面にいる
        {
            if (col.contacts[i].normal.y > 0.5f)　//当たっているcolliderの法線が0.5よりも大きいとき
            {
                isGround = true; //地面
            }
            if (col.contacts[i].normal.y < -0.5f)
            {
                if (gravity > 0f)
                {
                    gravity = 0f;
                }
            }
        }     
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        OnCollisionEnter2D(coll); //当たっている間trueが入り続ける
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        OnCollisionEnter2D(coll);　//離れている間falseが入り続ける
    }
}
