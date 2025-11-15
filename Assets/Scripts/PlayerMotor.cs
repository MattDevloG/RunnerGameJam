using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    // privates variables & scripts
    private float current_flame_value;
    private bool can_take_damage;
    private Rigidbody2D rb;

    // editable variables
    [Header("Movement & Physics")]
    [SerializeField] private float movement_speed;
    [SerializeField] private float jump_height;
    [SerializeField] private float ground_radius;
    [SerializeField] private Transform ground_point;
    [SerializeField] private LayerMask ground_layer;

    [Header("Flame & UI")]
    [SerializeField] private BarManager flame_bar;
    [SerializeField] private float max_flame_value;
    [SerializeField] private Transform visual_flamme;
    [SerializeField] private GameObject smoke_prefab;
    [SerializeField] private Transform smoke_point;
    [SerializeField] private GameObject game_over_panel;
    [SerializeField] private ObstacleManager obstacle_manager;

    private void Start()
    {
        // get components
        rb = GetComponent<Rigidbody2D>();

        // init varaibles
        current_flame_value = max_flame_value;
        can_take_damage = true;
    }

    private void Update()
    {
        // get horizontal axis cmd (left,right/a,d)
        float horizontal_axis = Input.GetAxis("Horizontal") * movement_speed;
        // apply moevemnt by using rigidbody velocity func
        rb.velocity = new Vector2(horizontal_axis, rb.velocity.y);

        // check a jump
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            // add up force by perserving x velocity
            rb.velocity = new Vector2(rb.velocity.x, jump_height);
        }

        // check an end
        if(current_flame_value <= 0)
        {
            // active game over panel
            game_over_panel.SetActive(true);
            // disable obstacle spawn sys
            obstacle_manager.enabled = false;
            // destroy player
            Destroy(gameObject);
        }
    }

    //// ON GROUNDED ?? ////
    private bool IsGrounded()
    {
        // check with a cercle
        if(Physics2D.OverlapCircle(ground_point.position, ground_radius, ground_layer))
        {
            return true;
        }
        else{return false;}
    }

    // on take damage
    public void TakeDamage(float amount)
    {
        // check if the player is alive and he's able to take damage
        if(current_flame_value <= 0 || !can_take_damage)
            return;

        // camera shake
        CameraManager.instance.OnShakeCamera();

        current_flame_value -= amount; // decrease flame intensity
        flame_bar.OnSetBar(current_flame_value);
        float decreasing = current_flame_value/100f;
        // limit value
        decreasing = Mathf.Clamp(decreasing, 0f, 1f);
        visual_flamme.localScale = new Vector3(decreasing, decreasing, decreasing);
        Instantiate(smoke_prefab, smoke_point.position, Quaternion.identity);
        // call damage type (efx)
        StartCoroutine(DamageType());
    }

    private IEnumerator DamageType()
    {
        // sprite blinking
        can_take_damage = false;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().enabled = true;
        can_take_damage = true;
    }
}
