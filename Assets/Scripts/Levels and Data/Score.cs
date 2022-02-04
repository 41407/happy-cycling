using UnityEngine;
using System.Collections;

public static class Score
{
    private static int crashes = 0;
    private static float time = 0;
    static int flips = 0;

    public static void Reset()
    {
        crashes = 0;
        time = 0;
        flips = 0;
    }

    public static void AddCrash()
    {
        crashes++;
    }

    public static void SetCrashes(int newCrashes)
    {
        crashes = newCrashes;
    }

    public static int GetCrashes()
    {
        return crashes;
    }

    public static void AddTime(float elapsed)
    {
        time += elapsed;
    }

    public static void SetTime(float newTime)
    {
        time = newTime;
    }

    public static float GetTime()
    {
        return time;
    }

    public static void AddFlip()
    {
        flips++;
    }

    public static void SetFlips(int count)
    {
        flips = count;
    }

    public static int GetFlips()
    {
        return flips;
    }
}
