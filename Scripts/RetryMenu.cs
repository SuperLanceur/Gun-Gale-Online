using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class RetryMenu : MonoBehaviour
{
    public GameObject panel;
    public GameObject deathImage;
    public GameObject alertImage;
    public GameObject retryButton;
    public GameObject quitButton;
    public Color startColor;
    public Color endColor;
    public float fadeSpeed = 2f;
    public AudioSource[] audioSources;
    public bool bGameOver;
    
    private Canvas canvas;
    private float timer;
    private Image panelImage;
    private RectTransform deathIRecT;
    private RectTransform alertIRecT;
    private RectTransform retryRT;
    private RectTransform quitRT;
    private bool bAudioPlay;
    private bool bAlertAudioPlay;

    private void Start()
    {
        bGameOver = false;
        bAudioPlay = false;
        bAlertAudioPlay = false;
        Time.timeScale = 1;
        canvas = GetComponent<Canvas>();
        //canvas.enabled = false;
        deathImage.SetActive(false);
        alertImage.SetActive(false);
        retryButton.SetActive(false);
        quitButton.SetActive(false);
        panelImage = panel.GetComponent<Image>();
        deathIRecT = deathImage.GetComponent<RectTransform>();
        alertIRecT = alertImage.GetComponent<RectTransform>();
        retryRT = retryButton.GetComponent<RectTransform>();
        quitRT = quitButton.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (bGameOver)
        {
            panelImage.color = Color.Lerp(panelImage.color, endColor, fadeSpeed * Time.deltaTime);           
            if (panelImage.color == endColor)
            {
                if (!bAudioPlay)
                {
                    audioSources[1].Play();
                    bAudioPlay = true;
                }
                deathIRecT.localScale = Vector3.Lerp(deathIRecT.localScale, new Vector3(1.5f, 0.8f, 1f), 4.2f * Time.deltaTime);
            }
            if (deathIRecT.localScale == new Vector3(1.5f, 0.8f, 1f))
            {
                timer += Time.deltaTime;
                if (timer >= 2f)
                {
                    
                    alertImage.SetActive(true);
                    if (!bAlertAudioPlay)
                    {
                        audioSources[2].Play();
                        bAlertAudioPlay = true;
                    }
                    alertIRecT.localScale = Vector3.Lerp(alertIRecT.localScale, Vector3.one, 4.2f * Time.deltaTime);
                }
            }
            if (alertIRecT.localScale == Vector3.one)
            {
                retryButton.SetActive(true);
                retryRT.localScale = Vector3.one;
                quitRT.localScale = Vector3.one;
                quitButton.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
	
    public void Pause()
    {
        if (!audioSources[0].isPlaying)
            audioSources[0].Play();
        panel.SetActive(true);

        deathImage.SetActive(true);

        

        //Time.timeScale = Time.timeScale == 0 ? 1 : 0;

        Cursor.lockState = CursorLockMode.None;      
    }
    public void Quit()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    public void Retry()
    {
        Application.LoadLevel(0);
    }
}
