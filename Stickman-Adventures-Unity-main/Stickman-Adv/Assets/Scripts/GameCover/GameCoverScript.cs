using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCoverScript : MonoBehaviour
{
    private Animator animator;
    public float SceneDuration = 5f;
    private bool dispose;

    [Header("Visual Effects")]
    public RectTransform titleRect;
    public float floatSpeed = 2f;
    public float floatAmount = 10f;
    public float pulseSpeed = 1f;
    public float pulseAmount = 0.05f;

    private Vector2 startPos;
    private Vector3 startScale;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator != null) animator.Play("Fade_Out");        
    }

    void Start()
    {
        // Auto-assign title if not set
        if (titleRect == null)
        {
            GameObject obj = GameObject.Find("Cover");
            if (obj != null) titleRect = obj.GetComponent<RectTransform>();
        }

        if (titleRect != null)
        {
            startPos = titleRect.anchoredPosition;
            startScale = titleRect.localScale;
        }
    }

    void Update()
    {
        if (dispose) 
        {
            return;
        }

        // Attractive Floating/Pulsing Animation
        if (titleRect != null)
        {
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
            titleRect.anchoredPosition = new Vector2(startPos.x, newY);

            float scaleOffset = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            titleRect.localScale = startScale + new Vector3(scaleOffset, scaleOffset, 0);
        }

        SceneDuration -= Time.deltaTime;
        if (SceneDuration <= 0)
        {
            dispose = true;
            animator.Play("Fade_In");
            StartCoroutine(LoadMenu());
        }        
    }

    public IEnumerator LoadMenu()
    {
        float animationDuration = AnimationUtilities.GetAnimationLength(animator);
        yield return new WaitForSeconds(animationDuration);
        SceneManager.LoadScene("Menu");
    }
}
