using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;
    public AnimationCurve JumpCurve;

    [Header("Car effects")]
    public int HP = 100;
    public string CurrentEffect = null;

    private bool jumping;

    //Local variables
    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    float velocityVsUp = 0;

    //Components
    Rigidbody2D carRigidbody2D;

    PlayInput playerInput;

    SpriteRenderer carSpriteRenderer;
    SpriteRenderer colorSpriteRenderer;
    SpriteRenderer lightsSpriteRenderer;
    SpriteRenderer windowsSpriteRenderer;

    BoxCollider2D carCollider;


    public void Damage(int amount)
    {
        HP -= amount;
        if (HP < 100)
        {
            //Todo: kill
        }
    }

    public void Heal(int amount)
    {
            HP += amount;
        if (HP > 100)
        {
            HP = 100;
        }
    }

    public void Push(Vector3 direction, float scale)
    {
        carRigidbody2D.AddForce(direction * scale, ForceMode2D.Impulse);
        Debug.Log("Pushed");
    }

    public void Jump(float jumpHeightScale, float jumpPushScale)
    {
        if (jumping == false)
        {
            StartCoroutine(JumpCoroutine(jumpHeightScale, jumpPushScale));
        }
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        if (jumping)
            return false;

        //Check if we are moving forward and if the player is hitting the brakes. In that case the tires should screech.
        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        //If we have a lot of side movement then the tires should be screeching
        if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
            return true;

        return false;
    }

    public float GetVelocityMagnitude()
    {
        return carRigidbody2D.velocity.magnitude;
    }


    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        playerInput = GameObject.Find("**PLAYER**/Player1Controller").GetComponent<PlayInput>();

        carSpriteRenderer = this.transform.Find("car").GetComponent<SpriteRenderer>();
        colorSpriteRenderer = this.transform.Find("color").GetComponent<SpriteRenderer>();
        lightsSpriteRenderer = this.transform.Find("lights").GetComponent<SpriteRenderer>();
        windowsSpriteRenderer = this.transform.Find("windows").GetComponent<SpriteRenderer>();

        carCollider = this.GetComponent<BoxCollider2D>();

    }


    void Update()
    {
        this.steeringInput = playerInput.SteeringInput;
        this.accelerationInput = playerInput.AccelerationInput;

        if (playerInput.SpaceAction.Click)
        {
            //this.Push(Vector3.up, 10f);
            //this.Jump(1.0f, 1.0f);
        }

    }

    //Frame-rate independent for physics calculations.
    void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    void ApplyEngineForce()
    {

        // if (jumping && accelerationInput > 0)
        // {
        //     accelerationInput = 0;
        // }

        //Apply drag if there is no accelerationInput so the car stops when the player lets go of the accelerator
        if (accelerationInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidbody2D.drag = 0;

        //Caculate how much "forward" we are going in terms of the direction of our velocity
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //Limit so we cannot go faster than the max speed in the "forward" direction
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        //Limit so we cannot go faster than the 50% of max speed in the "reverse" direction
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;

        //Limit so we cannot go faster in any direction while accelerating
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0 && jumping == false)
            return;

        //Create a force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        //Apply force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {

        if (jumping)
        {
            return;
        }

        //Limit the cars ability to turn when moving slowly
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 2);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        //Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        //Apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        //Get forward and right velocity of the car
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        //Kill the orthogonal velocity (side velocity) based on how much the car should drift. 
        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    float GetLateralVelocity()
    {
        //Returns how how fast the car is moving sideways. 
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }

    private IEnumerator JumpCoroutine(float jumpHeightScale, float jumpPushScale)
    {
        jumping = true;

        var jumpStartTime = Time.time;
        var jumpDuration = carRigidbody2D.velocity.magnitude * 0.05f;

        jumpHeightScale = jumpHeightScale * carRigidbody2D.velocity.magnitude * 0.05f;
        jumpHeightScale = Mathf.Clamp(jumpHeightScale, 0f, 1f);

        this.carCollider.enabled = false;

        carRigidbody2D.AddForce(carRigidbody2D.velocity.normalized * jumpPushScale * 10, ForceMode2D.Impulse);

        while (jumping)
        {

            var completedPercentange = (Time.time - jumpStartTime) / jumpDuration;
            completedPercentange = Mathf.Clamp01(completedPercentange);

            this.transform.localScale = Vector3.one + Vector3.one * JumpCurve.Evaluate(completedPercentange) * jumpHeightScale;

            if (completedPercentange == 1.0f)
            {
                break;
            }

            yield return null;
        }

        if (Physics2D.OverlapCircle(transform.position, 1f))
        {
            jumping = false;
            Jump(jumpHeightScale / 2, jumpPushScale / 2);
        }
        else
        {
            this.transform.localScale = Vector3.one;
            this.carCollider.enabled = true;

            jumping = false;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var effectData = other.gameObject.GetComponent<EffectData>();
        if (effectData != null)
        {
            if (effectData.Jump)
            {
                this.Jump(effectData.JumpHeightScale, effectData.JumpPushScale);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var effectData = other.gameObject.GetComponent<EffectData>();
        if (effectData != null)
        {
            if (effectData.Boost)
            {
                this.Push(other.gameObject.transform.up, effectData.BoostScale);
            }

            if (effectData.Damage)
            {
                this.Damage(effectData.DamageValue);
            }

        }
    }

}
