using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timeScale = 60f; //60 means 1 second is 1 in game minute
    public int hours = 6;
    public int minutes = 0;
    private float timeAccumulator = 0f;
    [Range(0f, 1f)] public float timePercent; //0 is midnight, 1 is start of night
    public static TimeManager Instance { get; private set; } //basic singleton

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
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
}
