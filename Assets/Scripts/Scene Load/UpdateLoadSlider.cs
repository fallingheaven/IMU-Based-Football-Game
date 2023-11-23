using UnityEngine;
using UnityEngine.UI;

// 用于实现加载条不断增长的效果
public class UpdateLoadSlider : MonoBehaviour
{
    public Slider slider;
    public float targetValue;

    private void OnEnable()
    {
        slider.value = 0;
    }

    public void SetValue(float value)
    {
        targetValue = value;
    }
    
    private void FixedUpdate()
    {
        // Debug.Log(slider.value);
        slider.value = Mathf.MoveTowards(slider.value, targetValue, Time.deltaTime);
    }
}
