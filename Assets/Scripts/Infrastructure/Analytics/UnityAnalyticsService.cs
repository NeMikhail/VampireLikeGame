using System.Collections.Generic;
using Unity.Services.Analytics;

namespace Infrastructure.Analytics
{
    public static class UnityAnalyticsService
    {
        public static bool IsInitialized = false;

        public static void SendEvent(string eventName)
        {
            if (IsInitialized)
            {
                AnalyticsService.Instance.CustomData(eventName);
            }
        }

        public static void SendEvent(string eventName, Dictionary<string, object> eventData)
        {
            if (IsInitialized)
            {
                AnalyticsService.Instance.CustomData(eventName, eventData);
            }
        }
    }
}