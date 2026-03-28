using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image mask;
    float originalSize;

    public static Healthbar HealthbarSingleton {get; private set;}

    void Awake()
    {
        HealthbarSingleton = this;        
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;                
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
