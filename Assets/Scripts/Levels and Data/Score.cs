public static class Score
{
    static int Crashes { get; set; }
    static float Time { get; set; }
    static int Flips { get; set; }

    public static void Reset()
    {
        Crashes = 0;
        Time = 0;
        Flips = 0;
    }

    public static void AddCrash()
    {
        Crashes++;
    }

    public static void SetCrashes(int count)
    {
        Crashes = count;
    }

    public static int GetCrashes()
    {
        return Crashes;
    }

    public static void AddTime(float timeSeconds)
    {
        Time += timeSeconds;
    }

    public static void SetTime(float newTime)
    {
        Time = newTime;
    }

    public static float GetTime()
    {
        return Time;
    }

    public static void AddFlip()
    {
        Flips++;
    }

    public static void SetFlips(int count)
    {
        Flips = count;
    }

    public static int GetFlips()
    {
        return Flips;
    }
}
