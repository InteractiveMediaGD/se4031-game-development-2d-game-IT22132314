using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class AttractiveUI : MonoBehaviour
{
    [Header("Title Animation")]
    public RectTransform Title;
    public float TitleFloatAmount = 10f;
    public float TitleFloatSpeed = 2f;

    [Header("Menu Buttons")]
    public GameObject ButtonsContainer;
    public float SlideInDistance = 1000f;
    public float SlideInDuration = 0.8f;

    private Vector2[] originalButtonPositions;
    private RectTransform[] buttons;

    void Awake()
    {
        if (ButtonsContainer != null)
        {
            buttons = ButtonsContainer.GetComponentsInChildren<RectTransform>(true);
            // We only want the direct children that are buttons
            System.Collections.Generic.List<RectTransform> filteredButtons = new System.Collections.Generic.List<RectTransform>();
            foreach(var b in buttons) {
                if(b.parent == ButtonsContainer.transform && b.GetComponent<Button>() != null) {
                    filteredButtons.Add(b);
                }
            }
            buttons = filteredButtons.ToArray();

            originalButtonPositions = new Vector2[buttons.Length];
            for (int i = 0; i < buttons.Length; i++)
            {
                originalButtonPositions[i] = buttons[i].anchoredPosition;
                // Move them off screen initially
                buttons[i].anchoredPosition += new Vector2(SlideInDistance, 0);
                
                // Add hover effect while we are here
                AddHoverEffect(buttons[i].gameObject);
            }
        }
    }

    void Start()
    {
        if (buttons != null) {
            StartCoroutine(SlideInButtons());
        }
    }

    void Update()
    {
        if (Title != null) {
            float newY = Mathf.Sin(Time.time * TitleFloatSpeed) * TitleFloatAmount;
            Title.anchoredPosition = new Vector2(Title.anchoredPosition.x, newY);
        }
    }

    private IEnumerator SlideInButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            StartCoroutine(LerpPosition(buttons[i], originalButtonPositions[i], SlideInDuration, i * 0.1f));
        }
        yield return null;
    }

    private IEnumerator LerpPosition(RectTransform rect, Vector2 targetPos, float duration, float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector2 startPos = rect.anchoredPosition;
        float elapsed = 0;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            // Smooth step (Cubic Out)
            t = 1 - Mathf.Pow(1 - t, 3);
            rect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }
        rect.anchoredPosition = targetPos;
    }

    private void AddHoverEffect(GameObject obj)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null) trigger = obj.AddComponent<EventTrigger>();

        // On Pointer Enter (Scale Up)
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { StartCoroutine(ScaleTo(obj.transform, 1.15f)); });
        trigger.triggers.Add(entry);

        // On Pointer Exit (Scale Down)
        EventTrigger.Entry exit = new EventTrigger.Entry();
        exit.eventID = EventTriggerType.PointerExit;
        exit.callback.AddListener((data) => { StartCoroutine(ScaleTo(obj.transform, 1f)); });
        trigger.triggers.Add(exit);
    }

    private IEnumerator ScaleTo(Transform t, float targetScale)
    {
        float duration = 0.2f;
        float elapsed = 0;
        Vector3 startScale = t.localScale;
        Vector3 targetV3 = new Vector3(targetScale, targetScale, 1);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            t.localScale = Vector3.Lerp(startScale, targetV3, elapsed / duration);
            yield return null;
        }
        t.localScale = targetV3;
    }
}
