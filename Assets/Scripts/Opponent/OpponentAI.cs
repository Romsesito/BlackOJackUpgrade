using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    
    [Header("Player Movement")]
    public float movementSpeed = 1f;
    public float rotationSpeed = 10f;
    public CharacterController characterController;
    public Animator animator; 


    [Header("Opponent Fight")]

    public float attackCooldown = 0.5f;
    public int attackDamages = 5;
    public string[] attackAnimations = { "Attack1Animation", "Attack2Animation", "Attack3Animation", "Attack4Animation" };
    private float lastAttackTime;
    public float dodgeDistance = 2f;
    public int attackCount = 0;
    public int randomNumber;
    public float attackRadius = 2f;
    public FightingController[] fightingController;
    public Transform[] players;
    public bool isTakingDamage;

        [Header("LIFE")]
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;




    void Awake()
    {
       createRandomNumber();
       currentHealth = maxHealth;
       healthBar.GiveFullHealth(currentHealth);
    }


    
    void Update()
    {

     /*   if (attackCount == randomNumber)
        {
            attackCount = 0;
            createRandomNumber();
       } 
  */


        for (int i = 0; i < fightingController.Length; i++)
        {
            if(players[i].gameObject.activeSelf && Vector3.Distance(transform.position, players[i].position) <= attackRadius)
            {
                animator.SetBool("Walking", false); 

                if(Time.time - lastAttackTime > attackCooldown)
                {
                   int randomAttackIndex = UnityEngine.Random.Range(0, attackAnimations.Length);
                   
                   if (!isTakingDamage)
                   {
                    PerformAttack(randomAttackIndex);
                   }



                   fightingController[i].StartCoroutine(fightingController[i].PlayHitDamageAnimation(attackDamages));
                }
            }
            else
            {

            if (players[i].gameObject.activeSelf)
            {
               Vector3 direction = (players[i].position - transform.position).normalized;
               characterController.Move(direction * movementSpeed * Time.deltaTime);

               Quaternion targetrotation = Quaternion.LookRotation(direction);
               transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, rotationSpeed * Time.deltaTime);

               animator.SetBool("Walking", true);
            }
        } }


    }



    void PerformAttack(int attackIndex)
    {

           animator.Play(attackAnimations[attackIndex]);
           int damage = attackDamages;
           Debug.Log("Performed attack "+ (attackIndex+1) + " dealing " + damage + " damages");

           lastAttackTime = Time.time;

    }

    void PerformDodgeFront()
    {
        animator.Play("DodgeFrontAnimation");
        Vector3 dodgeDirection = transform.forward * dodgeDistance;
        characterController.SimpleMove(dodgeDirection);
    }

    void createRandomNumber()
    {
        randomNumber = UnityEngine.Random.Range(1, 5);
    }


    
    public IEnumerator PlayHitDamageAnimation(int takeDamage)

    {
        yield return new WaitForSeconds(0.5f);
        animator.Play("HitDamageAnimation");

        currentHealth -= takeDamage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
          
           Die();
        }
        
        animator.Play("HitDamageAnimation");
        
    }


    public void HandleRuletaResult(int valor)
    {
        if (valor == 2)
        {
            currentHealth += 50;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthBar.SetHealth(currentHealth);
            movementSpeed = 1;
            
        }

        else if (valor == 4)
        {
            attackDamages = 10;
            movementSpeed = 1;
        }

        else if (valor == 6)
        {
            attackDamages = 1;
            movementSpeed = 10;
        }
    }

    void Die()
    {
        Debug.Log("Opponent is dead");
    }
}
