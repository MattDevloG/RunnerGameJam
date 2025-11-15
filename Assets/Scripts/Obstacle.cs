using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    // private variables & scripts
    private float current_movement_speed;
    private Rigidbody2D rb;
    private AudioSource source;

    // editable variables
    [SerializeField] private float obstacle_damage;
    [SerializeField] private UnityEvent effect_event;
    [SerializeField] private float min_movement_speed;
    [SerializeField] private float max_movement_speed;
    [SerializeField] private AudioClip damage_sound_efx;

    private void Start()
    {
        // get private component
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();

        // redifine an empty event
        if(effect_event == null)
            effect_event = new UnityEvent();

        // always move the ball at the opposite dir (only for ball)
        rb.velocity = new Vector2(Random.Range(min_movement_speed, max_movement_speed)*-1f, rb.velocity.y);

        // destroy ball after 15s (optimisation)
        Destroy(gameObject, 15f);
    }

    // detection
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player")) // the player himself
        {
            // decrease flame value
            coll.GetComponent<PlayerMotor>().TakeDamage(obstacle_damage);
            // do an effect
            effect_event.Invoke();
            // play damage clip
            source.clip = damage_sound_efx;
            source.Play();
            this.enabled = false;
        }

        // do bounce (only for a ball)
        if(coll.CompareTag("Ground"))
        {
            // bounce intensity
            rb.velocity = new Vector2(rb.velocity.x, Random.Range(10f, 20f));
            // play bounce sound
            if(transform.GetChild(0).GetComponent<AudioSource>() != null)
                transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
    }
}
