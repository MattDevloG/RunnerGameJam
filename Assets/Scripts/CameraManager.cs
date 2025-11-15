using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    // privates variables
    bool is_shake_enabled;
    // editable variable
    [SerializeField] private float horizontal_movement_speed;
    [SerializeField] private Transform view_point;

    // singleton
    public static CameraManager instance;

    private void Awake()
    {
        // assign
        instance = this;

        // active shake by default
        is_shake_enabled = true;
    }

    private void Update()
    {
        // always move camera forward
        transform.Translate(Vector3.right * horizontal_movement_speed * Time.deltaTime);
    }

    // handle shaking effect
    public void OnShakeCamera()
    {
        // check a shake
        if(!is_shake_enabled)
            return;

        // call ienumerator type
        StartCoroutine(ShakeCamera(0.15f,0.3f)); // unvariable parameters
    }

    IEnumerator ShakeCamera(float magnitude, float duration)
    {
        // disable shake
        is_shake_enabled = false;
        // get current local camera position
        Vector3 original_pos = view_point.localPosition;

        // save elapsed time
        float elapsed = 0.0f;

        // shake camera while duration is greater than elapsed
        while(elapsed < duration)
        {
            // get random x,y coordinate
            float x = Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-1f,1f) * magnitude;

            // apply new coordinates
            view_point.localPosition = new Vector3(x,y,original_pos.z);
            // increase elapsed
            elapsed += Time.deltaTime;
            // delay
            yield return null;
        }

        // return to normal
        view_point.localPosition = original_pos;
        // enabled shake
        is_shake_enabled = true;
    }

    /*********** here remove exceeds sprites ***************/
    private void OnTriggerEnter2D(Collider2D coll)
    {
        // check tag
        if(coll.CompareTag("Ground"))
        {
            // destroy after 10s (optimisation)
            Destroy(coll.transform.gameObject, 10f);
        }
    }
}
