using UnityEngine;

public class DayNightEvents : MonoBehaviour
{
    private TimeManager time;
    public int sunriseHour = 6;
    public int sunsetHour = 18;
    private int lastHour = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = TimeManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (time == null) return;

        if (time.hours != lastHour)
        {
            lastHour = time.hours;

            if (time.hours == sunriseHour)
            {
                SunriseEvent();
            }
            else if (time.hours == sunsetHour)
            {
                SunsetEvent();
            }
        }
    }

    public void SunriseEvent()
    {
        Debug.Log("sunrise...");
    }

    public void SunsetEvent()
    {
        Debug.Log("sunset...");
    }
}
