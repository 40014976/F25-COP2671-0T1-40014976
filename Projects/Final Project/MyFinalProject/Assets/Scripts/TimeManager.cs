using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timeScale = 60f;
    public int hours = 6;
    public int minutes = 0;
    private float timeAccumulator = 0f;
    [Range(0f, 1f)] public float timePercent;
    public static TimeManager Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeAccumulator += Time.deltaTime * timeScale;

        if (timeAccumulator >= 60f)
        {
            timeAccumulator -= 60f;
            minutes++;

            if (minutes >= 60)
            {
                minutes = 0;

                hours++;
                if (hours >= 24)
                {
                    hours = 0;
                }
            }
        }

        timePercent = (hours + minutes / 60f) / 24f;
    }

    public float GetTimePercent()
    {
        return timePercent;
    }

    public string GetTimeString()
    {
        return string.Format("{0:00}:{1:00}", hours, minutes);
    }
}
