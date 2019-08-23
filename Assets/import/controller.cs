using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class controller : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController cc;
    Vector3 movement = new Vector3(0,0,0);
    public Animator animator;
    float xVelo = 0;
    float yVelo = 0;
    float zVelo = 0;
//aceleration
    float xAcel = 0;
    float yAcel = 0;
    float zAcel = 0;
//max velocity players are allowed to input
    float xPlyMax = 8;
    float yPlyMax = 8;
    float zPlyMax = 8;

   public float rotateSpeed = 5f;
   public float speed = 3f;
   public float jumpSpeed = 5f;
   public float grav = 0.25f;
   public float fullGrav = 0.5f;
    public bool isMoving;
    public bool isAttacking = false;
    public bool grounded;
    public bool holdJumping;
    public GameObject Fireball;
    bool canAttack = false;
    float attackTimer = 0.0f;
    public float attackTime = .75f;
    int hp = 3;
    float invincible = 0.0f;
    public float invincibleTime = 5f;
    public hud HUD;
    public GameObject render;
    public float blinkFrequency;
    void Start()
    {
        cc = GetComponent<CharacterController>();
        print("Hi");
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <1)
        {
            render.SetActive(true);
            bool r = Input.GetButtonDown("restart");
            if (r)
            {
                SceneManager.LoadScene("gameScene", LoadSceneMode.Single);
            }
            return;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool isAttacking = Input.GetButtonDown("Fire1");
        bool isJumping = Input.GetButtonDown("Jump");
        holdJumping = Input.GetButton("Jump");
        Vector3 moveDirection = new Vector3(h, 0.0f, v);
        invincible-= Time.deltaTime;
        bool attackAnim = this.animator.GetCurrentAnimatorStateInfo(0).IsName("Witch_Attack");
        bool hurtAnim = this.animator.GetCurrentAnimatorStateInfo(0).IsName("Witch_Damage");
        animator.SetBool("isAttacking",isAttacking);
        if (isAttacking)
        {
            animator.Play("Witch_Attack",-1,0);
            
            
            print("Attack");
            
        }
        if (attackAnim)
        {
            attackTimer+=Time.deltaTime;
            if (attackTimer>attackTime&& canAttack)
            {
                    Instantiate(Fireball, transform.position,transform.rotation);
                    canAttack = false;
            }
        }
        else
        {
            attackTimer = 0.0f;
            canAttack = true;

        }
        



        isMoving = moveDirection!= Vector3.zero;
        animator.SetBool("isMoving",isMoving);
        if (isMoving )
        {
            
            transform.rotation = Quaternion.Slerp
                (
                    transform.rotation,
                    Quaternion.LookRotation(moveDirection),
                    Time.deltaTime * rotateSpeed
                );
        }



        //dont moving if attacking or hit
        
        if (attackAnim||hurtAnim)
        {
            moveDirection = Vector3.zero;
        }

        Vector3 finalMove = moveDirection* Time.deltaTime*speed;
        finalMove.y = yVelo* Time.deltaTime;
        cc.Move(finalMove);

        //jumping and gravity
        //set current movement values then calculate vertical speed
        grounded = cc.isGrounded;
        
        if (grounded)
        {
            yVelo = -1;
            if (isJumping &&!attackAnim&&!hurtAnim)
            {
                print("Jump");
                yVelo = jumpSpeed;
            }
        }
        else
        {
            if (holdJumping)
            {
                yVelo -= grav;

            }
            else{
                yVelo -=fullGrav;
            }
            
        }
        //controlled blinking when hit
        if (invincible>0)
        {
            render.SetActive(true);
            if (Mathf.Sin(invincible*blinkFrequency)>0)
            {
                render.SetActive(false);
            }
        }
        else
        {
            render.SetActive(true);
        }
    }


   public void getHit()
    {
        if (invincible<0&&hp>0)
        {
            print("hurt");
            hp--;
            invincible = invincibleTime;
            if (hp == 0)
            {
                animator.Play("Witch_Dead",-1,0);
            }
            else
            {
                animator.Play("Witch_Damage",-1,0);
            }
            
            HUD.removeHeart();
        }
        
        
    }
    
}
