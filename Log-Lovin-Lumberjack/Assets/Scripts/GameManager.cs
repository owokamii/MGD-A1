using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region References
    [Header("References")]
    public GameObject gameOverMenu;
    public GameObject gameOverUI;
    public GameObject gameplayUI;
    public TMP_Text scoreText;
    public TMP_Text highscoreText;
    public Image fadeImage;
    private Axe axe;
    private Spawner spawner;
    #endregion

    #region Scores
    private int score;
    private int highscore;
    #endregion

    #region Hearts
    public int currentHearts;
    public int maxHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    #endregion

    private void Awake()
    {
        axe = FindObjectOfType<Axe>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        NewGame();
    }

    void Update()
    {
        if (currentHearts > maxHearts)
        {
            currentHearts = maxHearts;
        }

        if (currentHearts != 0)
        {
            //gameOverScreen.SetActive(false);
        }
        else
        {
            GameOver();
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHearts)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            if (i < maxHearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }

    private void NewGame()
    {
        PauseMenu.GameIsPaused = false;

        axe.enabled = true;
        spawner.enabled = true;

        score = 0;
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = "x " + score.ToString();
        highscoreText.text = highscore.ToString();

        ClearScene();
    }

    private void ClearScene()
    {
        Log[] logs = FindObjectsOfType<Log>();

        foreach (Log log in logs)
        {
            Destroy(log.gameObject);
        }

        Dynamite[] dynamites = FindObjectsOfType<Dynamite>();

        foreach (Dynamite dynamite in dynamites)
        {
            Destroy(dynamite.gameObject);
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = "x " + score.ToString();
        if (highscore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void DecreaseHeart(int amount)
    {
        currentHearts -= amount;
    }

    public void Explode()
    {
        axe.enabled = false;
        spawner.enabled = false;

        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        yield return new WaitForSecondsRealtime(1f);

        elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        GameOver();
    }

    private void GameOver()
    {
        gameplayUI.SetActive(false);
        gameOverMenu.SetActive(true);
        gameOverUI.SetActive(true);
        PauseMenu.GameIsPaused = true;
    }
}
