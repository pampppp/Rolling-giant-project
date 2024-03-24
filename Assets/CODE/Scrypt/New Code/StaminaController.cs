using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaController : MonoBehaviour
{
    public float stamina;
    public float staminaMax;

    public float speed;
    public float runSpeed;
    public float walkSpeed;

    public float drainRate;
    public float rechargeTime;

    public float fatigueTimer;
    public bool isFatigued;

    public bool isRunning;

    public float exponentialPenalty;

    public void ReduceStamina(float stamina, float staminaMax, float speed, float runSpeed, float walkSpeed, float drainRate, float rechargeTime, float fatigueTimer, float exponentialPenalty)
    {
        this.stamina = stamina;
        this.staminaMax = staminaMax;
        this.speed = speed;
        this.runSpeed = runSpeed;
        this.walkSpeed = walkSpeed;
        this.drainRate = drainRate;
        this.rechargeTime = rechargeTime;
        this.fatigueTimer = fatigueTimer;
        this.exponentialPenalty = exponentialPenalty;

        StaminaEquation();
    }

    public void StaminaEquation()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(stamina > 0 && !isFatigued)
            {
                speed = runSpeed;
                isRunning = true;
            }
            else if (isRunning || isFatigued)
            {
                speed = walkSpeed;
                isRunning = false;

                exponentialPenalty = 1;
            }

            exponentialPenalty += Time.deltaTime / 20f;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && isRunning || isFatigued)
        { 
                speed = walkSpeed;
                isRunning = false;
        }

        if (!Input.GetKey(KeyCode.LeftShift) && exponentialPenalty > 1)
        {
            exponentialPenalty -= Time.deltaTime / 20f;

            if(exponentialPenalty < 1) exponentialPenalty = 1f;
        }

        if(isRunning)
        {
            stamina -= (Time.deltaTime * drainRate * exponentialPenalty);
            stamina += Time.deltaTime * rechargeTime;
        }
        else if (!isFatigued)
        {
            stamina += Time.deltaTime * rechargeTime;
        }

        if(stamina <= 0f && fatigueTimer <= 3)
        {
            fatigueTimer += Time.deltaTime;
            isFatigued = true;
        }
        else if(fatigueTimer >= 3)
        {
            stamina += Time.deltaTime * rechargeTime;
            isFatigued = false;
            fatigueTimer = 0;
        }

        if(stamina < 0f) stamina = 0f;

        if(stamina > staminaMax) stamina = staminaMax;
    }
}
