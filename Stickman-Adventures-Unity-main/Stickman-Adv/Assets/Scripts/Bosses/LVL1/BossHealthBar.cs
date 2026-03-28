using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Image mask;
    float originalSize;

    public static BossHealthBar BossHealthbarSingleton {get; private set;}

    void Awake()
    {
        BossHealthbarSingleton = this;        
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
