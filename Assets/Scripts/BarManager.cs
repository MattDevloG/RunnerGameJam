using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    // private scripts & variables
    private Slider slider;
    private Animator aim;

    // editables variables
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill_image;

    private void Start()
    {
        // get components
        slider = GetComponent<Slider>();
        aim = GetComponent<Animator>();
    }

    public void OnSetBar(float _value)
    {
        // update silder value
        slider.value = _value;
        // ajust fill color depending on gradient evaluation
        fill_image.color = gradient.Evaluate(slider.normalizedValue);
        // play animatiom
        aim.SetTrigger("on");
    }
}
