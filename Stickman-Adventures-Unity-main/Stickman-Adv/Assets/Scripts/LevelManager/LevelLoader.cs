using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transitionAnimatorController;
    public float transitionDuration;    

    private static LevelLoader instance;

    public static LevelLoader Instance { get { return instance; }}

    public float TimerUntilFadeIn;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(TimerUntilFadeIn);

        transitionAnimatorController.SetTrigger("Exit");
        float animationDuration = AnimationUtilities.GetAnimationLength(transitionAnimatorController);

        yield return new WaitForSeconds(animationDuration);

        SceneManager.LoadScene("LevelProgressScene");
    }

}
