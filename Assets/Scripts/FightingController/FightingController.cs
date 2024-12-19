using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingController : MonoBehaviour
{

    [Header("Player Movement")]
    public float movementSpeed = 1f;
    public float rotationSpeed = 10f;
    private CharacterController characterController;
    private Animator animator;


    [Header("Player Fight")]

    public float attackcooldown = 0.5f;
    public int attackDamages = 5;
    public string[] attackAnimations = { "Attack1Animation", "Attack2Animation", "Attack3Animation", "Attack4Animation" };
    private float lastAttackTime;
    public float dodgeDistance = 2f;
    public Transform[] opponents;
    public float attackRadius = 2.2f;


    [Header("VFX")]

    public ParticleSystem attack1Effect;
    public ParticleSystem attack2Effect;
    public ParticleSystem attack3Effect;
    public ParticleSystem attack4Effect;


    [Header("LIFE")]
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    void Awake ()   
    {
        currentHealth = maxHealth;
        characterController = GetComponent<CharacterController>();
        healthBar.GiveFullHealth(currentHealth);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        PerformMovement();
        PerformDodgeFront();

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PerformAttack(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            PerformAttack(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            PerformAttack(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            PerformAttack(3);
        }
    }

    void PerformMovement ()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        characterController.Move(movement * movementSpeed * Time.deltaTime);
    }

    void PerformAttack(int attackIndex)
    {
        if (Time.time - lastAttackTime > attackcooldown)
        {
           animator.Play(attackAnimations[attackIndex]);
           int damage = attackDamages;
           Debug.Log("Performed attack "+ (attackIndex+1) + " dealing " + damage + " damages");

           lastAttackTime = Time.time;


           foreach(Transform opponent in opponents)
           {
               if(Vector3.Distance(transform.position, opponent.position) <= attackRadius)
               {
                   opponent.GetComponent<OpponentAI>().StartCoroutine(opponent.GetComponent<OpponentAI>().PlayHitDamageAnimation(attackDamages));
               }
           }



        }
        else
        {
            Debug.Log("Attack on cooldown");
        }
    }

    void PerformDodgeFront()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            animator.Play("DodgeFrontAnimation");
            Vector3 dodgeDirection = transform.forward * dodgeDistance;
            characterController.Move(dodgeDirection);
        }
    }


    public void Attack1Effect()
    {
        attack1Effect.Play();
    }

        public void Attack2Effect()
    {
        attack2Effect.Play();
    }

        public void Attack3Effect()
    {
        attack3Effect.Play();
    }

        public void Attack4Effect()
    {
        attack4Effect.Play();
    }




    public IEnumerator PlayHitDamageAnimation(int takeDamage)

    {
        yield return new WaitForSeconds(0.5f);
        animator.Play("HitDamageAnimation");

        currentHealth -= takeDamage;
        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
        animator.Play("HitDamageAnimation");

    }


    public void HandleRuletaResult(int valor)
    {
        if (valor == 1)
        {
            currentHealth += 50;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthBar.SetHealth(currentHealth);

            movementSpeed = 1;
        }

        else if (valor == 3)
        {
            attackDamages = 10;
            movementSpeed = 1;
        }

        else if (valor == 5)
        {
            attackDamages = 1;
            movementSpeed = 10;
        }

    }

    void Die()
    {
        Debug.Log("Player died");

    }
}
