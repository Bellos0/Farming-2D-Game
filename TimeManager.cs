using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// lay thoi gian hien tai cua he thong duoi dang iso, cho dong nhat vs dang time trong cropplotdata
    /// </summary>
    /// <returns></returns>
    public string GetCurrentSystemTime()
    {
        return DateTime.Now.ToString("O");
    }


    public double GetMinPassedSince(string passTimeStr)
    {
        if (string.IsNullOrEmpty(passTimeStr))
        {
            return 0;
        }

        try
        {
            DateTime passTime = DateTime.Parse(passTimeStr);
            DateTime curTime = DateTime.Now;
            TimeSpan timeDif = curTime - passTime;
            return timeDif.TotalMinutes;
        }
        catch (Exception e)
        {

            Debug.Log("loi thoi gian trong GetMinPassedSince: " + e.Message.ToString());
            return 0;
        }
    }
}
