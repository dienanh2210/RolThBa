using UnityEngine;
using System.Collections;
using System;
using System.Globalization;

namespace ToolsExtend
{
    public enum NotificationType { Once, Repeat }
    public enum NotificationTimeType { DelayTime, DelayDate, DelayDateInWeek, SpecificDate }
    [System.Serializable]
    public class NotificationItem
    {
        public int id;
        public string title;
        public string []content;
        public string small_icon;
        public string big_icon;
        public bool hasSound;
        public bool hasVibrate;

        //purpose specific
        public NotificationType notificationType;
        public NotificationTimeType notificationTimeType;

        public int delayTime;

        public int delayDate;
        public DayOfWeek dateInWeek;

        public int exactHour;
        public int exactMinute;
        public int exactSecond;

        public string exactDate;

        public int repeatTimes;
    }

    public class NotificationSystem : MonoBehaviour
    {

        [SerializeField]
        private bool _enableNotify;
        [SerializeField]
        private int repeatitionID;
        [SerializeField]
        private NotificationItem[] notifyItems;

        INotification notification;

        public INotification CurrentNotification
        {
            get
            {
                return notification;
            }
        }

        public bool enableNotify
        {
            get
            {
                return _enableNotify;
            }
        }

        private static NotificationSystem instance;
        public static NotificationSystem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<NotificationSystem>();
                    instance.Init();
                }
                return instance;
            }
        }

        void Awake()
        {
            if (instance == null)
            {
                Init();
            }
        }


        public void Init()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(this);
            Debug.Log("try to create notification crossplatform");

#if !UNITY_EDITOR
#if UNITY_ANDROID
			notification = new CNotificationAndroid ();

            if (notification != null)
            {
                notification.Init();
            }

            if (enableNotify)
            {
                Debug.Log("enable notification");
                CancelAllNotification();
                SetupNewNotification();
            }

            Debug.Log("Created notification crossplatform android");
#endif
#if UNITY_IOS
            RegisterForNotificationIOS();

            Debug.Log("Register notification crossplatform on IOS");
#endif
#endif
        }

        public void SendNotification(int id, string title, string content, string small_icon, string big_icon, 
            long delay, bool hasSound = false, bool hasVibrate = false)
        {
            if (!enableNotify)
            {
                return;
            }
            if (delay <= 0)
            {
                Debug.Log("can't start a notification with time <=0");
                return;
            }
            if (notification != null)
            {
                notification.SendNotification(id, title, content, small_icon, big_icon, delay, hasSound, hasVibrate);
                Debug.Log("Send Notification content : " + content);
            }
            else
            {
                Debug.Log("Send Notification content : " + content);
            }
        }

        public void CancelNotification(int id)
        {
            if (notification != null)
            {
                notification.CancelNotification(id);
            }
            else
            {
                Debug.Log("Cancel notification");
            }
        }

        public void SetupNewNotification()
        {
#if !UNITY_EDITOR
#if UNITY_ANDROID
            if (notification == null)
            {
                return;
            }
#endif
#endif
            foreach (NotificationItem item in notifyItems)
            {
                SetupNotify(item);
            }
        }

        public void SetupNotify(NotificationItem item)
        {
            switch (item.notificationType)
            {
                case NotificationType.Once:
                    SetupNotificationPlayOnce(item);
                    break;
                case NotificationType.Repeat:
                    SetupNotificationPlayMultipleTime(item);
                    break;
            }

        }

        private void SetupNotificationPlayOnce(NotificationItem item)
        {
            long time = 0;
            TimeSpan span;
            DateTime today = DateTime.Today;
            switch (item.notificationTimeType)
            {
                case NotificationTimeType.DelayDate:
                    //DateTime currentDate = DateTime.Today;
                    //span = new TimeSpan(item.delayDate, item.exactHour, item.exactMinute, item.exactSecond);				
                    today = today.AddDays(item.delayDate).AddHours(item.exactHour).AddMinutes(item.exactMinute).AddSeconds(item.exactSecond);
                    time = (long)today.Subtract(DateTime.Now).TotalMilliseconds;
                    Debug.Log(time);
                    break;
                case NotificationTimeType.DelayTime:
                    time = item.delayTime * 1000L;
                    break;
                case NotificationTimeType.SpecificDate:
                    DateTime targetDate = DateTime.ParseExact(item.exactDate, "dd/mm/yyyy", CultureInfo.CurrentCulture);
                    span = new TimeSpan(0, item.exactHour, item.exactMinute, item.exactSecond);
                    targetDate = targetDate.Add(span);
                    span = targetDate.Subtract(DateTime.Now);
                    time = (long)span.TotalMilliseconds;
                    break;
                case NotificationTimeType.DelayDateInWeek:
                    DateTime currentDate = DateTime.Today;
                    int diff = item.dateInWeek - currentDate.DayOfWeek;
                    if (diff < 0)
                    {
                        diff += 7;
                    }
                    today = today.AddDays(diff).AddHours(item.exactHour).AddMinutes(item.exactMinute).AddSeconds(item.exactSecond);
                    span = today.Subtract(DateTime.Now);
                    time = (long)span.TotalMilliseconds;
                    break;
            }
#if UNITY_ANDROID
            SendNotification(item.id, item.title, item.content[UnityEngine.Random.Range(0, item.content.Length)], 
                item.small_icon, item.big_icon, time, item.hasSound);
#endif

#if UNITY_IOS
            DateTime dateTime = DateTime.Now.AddMilliseconds(time); // new DateTime(time * 10L * 1000L);
            //Debug.Log("dateTime: " + dateTime.ToString());
            ScheduleNotificationIOS(item.title, item.content[UnityEngine.Random.Range(0, item.content.Length)], 
                dateTime, (item.hasSound && item.hasVibrate));
#endif
        }

        private void SetupNotificationPlayMultipleTime(NotificationItem item)
        {
            long time = 0;
            TimeSpan span;
            DateTime today = DateTime.Today;
            switch (item.notificationTimeType)
            {
                case NotificationTimeType.DelayDate:
                    for (int i = 0; i < item.repeatTimes; i++)
                    {
                        today = DateTime.Today;
                        today = today.AddDays(item.delayDate * (i + 1)).AddHours(item.exactHour).AddMinutes(item.exactMinute).AddSeconds(item.exactSecond);
                        time = (long)today.Subtract(DateTime.Now).TotalMilliseconds;
#if UNITY_ANDROID
                        SendNotification(item.id + repeatitionID * i, item.title, item.content[UnityEngine.Random.Range(0, item.content.Length)], 
                            item.small_icon, item.big_icon, time, item.hasSound,item.hasVibrate);
#endif

#if UNITY_IOS
                        DateTime dateTime = DateTime.Now.AddMilliseconds(time); // new DateTime(time * 10L * 1000L);
                        //Debug.Log("dateTime: " + dateTime.ToString());
                        ScheduleNotificationIOS(item.title, item.content[UnityEngine.Random.Range(0, item.content.Length)], 
                            dateTime, (item.hasSound && item.hasVibrate));
#endif
                    }
                    break;
                case NotificationTimeType.DelayTime:
                    for (int i = 0; i < item.repeatTimes; i++)
                    {
                        time = item.delayTime * 1000L * (i + 1);

                        Debug.Log("item.content[UnityEngine.Random.Range(0, item.content.Length)]: " + item.content[UnityEngine.Random.Range(0, item.content.Length)]);

#if UNITY_ANDROID
                        SendNotification(item.id + repeatitionID * i, item.title, item.content[UnityEngine.Random.Range(0, item.content.Length)], 
                            item.small_icon, item.big_icon, time, item.hasSound, item.hasVibrate);
#endif

#if UNITY_IOS
                        DateTime dateTime = DateTime.Now.AddMilliseconds(time); // new DateTime(time * 10L * 1000L);
                        //Debug.Log("dateTime: " + dateTime.ToString());
                        ScheduleNotificationIOS(item.title, item.content[UnityEngine.Random.Range(0, item.content.Length)], 
                            dateTime, (item.hasSound && item.hasVibrate));
#endif
                    }
                    break;
                case NotificationTimeType.DelayDateInWeek:
                    DateTime currentDate = DateTime.Today;
                    int diff = item.dateInWeek - currentDate.DayOfWeek;
                    int remain = diff;
                    if (diff < 0)
                    {
                        // For next week
                        diff += 7;
                    }
                    for (int i = 0; i < item.repeatTimes; i++)
                    {
                        // For next week
                        if ((remain % 3) == 0)
                        {
                            remain += 7;
                            continue;
                        }

                        today = DateTime.Today;
                        today = today.AddDays(diff + i * 7).AddHours(item.exactHour).AddMinutes(item.exactMinute).AddSeconds(item.exactSecond);
                        span = today.Subtract(DateTime.Now);
                        time = (long)span.TotalMilliseconds;

#if UNITY_ANDROID
                        SendNotification(item.id + repeatitionID * i, item.title, item.content[UnityEngine.Random.Range(0, item.content.Length)], 
                            item.small_icon, item.big_icon, time, item.hasSound, item.hasVibrate);
#endif

#if UNITY_IOS
                        DateTime dateTime = DateTime.Now.AddMilliseconds(time); // new DateTime(time * 10L * 1000L);
                        //Debug.Log("dateTime: " + dateTime.ToString());
                        ScheduleNotificationIOS(item.title, item.content[UnityEngine.Random.Range(0, item.content.Length)], 
                            dateTime, (item.hasSound && item.hasVibrate));
#endif
                    }
                    break;
            }
        }

        public void OnApplicationPause(bool paused)
        {
            Debug.Log("paused is: " + paused);

#if UNITY_ANDROID
            if (notification != null)
            {
                notification.OnApplicationPause(paused);
            }
#endif

#if UNITY_IOS
            if (paused)
            {
                if (enableNotify)
                {
                    CancelAllNotificationIOS();
                    SetupNewNotification();
                }
            }
            else
            {
                CancelAllNotificationIOS();
            }
#endif
        }

        public void CancelAllNotification()
        {
            if (notification != null)
            {
                notification.CancelAllNotification();
            }

#if UNITY_IOS
            CancelAllNotificationIOS();
#endif
        }

        #region For IOS
        #if UNITY_IOS
        void RegisterForNotificationIOS()
        {
            UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Badge);
        }

        void ScheduleNotificationIOS(string title, string content, DateTime dateTime,
            bool hasAction = false, bool hasSound = false, bool hasVibrate = false)
        {
            UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();

            notif.alertAction = title;
            notif.alertBody = content;
            notif.fireDate = dateTime;
            notif.hasAction = hasAction;

            UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notif);
        }

        void CancelAllNotificationIOS()
        {
            UnityEngine.iOS.NotificationServices.ClearLocalNotifications();

            UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications();
        }
        #endif
        #endregion
    }

    public interface INotification
    {
        void Init();
        void SendNotification(int id, string title, string content, string small_icon, string big_icon,
            long delay, bool hasSound = false, bool hasVibrate = false);
        void CancelNotification(int id);
        void CancelAllNotification();
        void OnApplicationPause(bool paused);
    }

    public class CNotificationAndroid : INotification
    {
        private string pluginpackage = "enet.utils.unity.LocalNotification";
        private string unityPackage = "com.unity3d.player.UnityPlayerActivity";

        void SetAppActivity(bool active)
        {
            using (AndroidJavaClass cl = new AndroidJavaClass(pluginpackage))
            {
                cl.CallStatic("setAppActivity", active ? 1 : 0);
            }
        }

        public void Init()
        {
            try
            {
                SetAppActivity(true);
            }
            catch (System.Exception e)
            {
                Debug.Log("error on Init Android " + e.Message);
            }
        }

        public void SendNotification(int id, string title, string content, string small_icon, string big_icon,
            long delay, bool hasSound = false, bool hasVibrate = false)
        {
            using (AndroidJavaClass cl = new AndroidJavaClass(pluginpackage))
            {
                cl.CallStatic("startNotify", title, content, small_icon, big_icon, hasSound ? 1 : 0, hasVibrate ? 1 : 0, delay, id, unityPackage);
            }
        }

        public void CancelNotification(int id)
        {
            using (AndroidJavaClass cl = new AndroidJavaClass(pluginpackage))
            {
                cl.CallStatic("cancelNotify", id);
            }
        }

        public void CancelAllNotification()
        {
            using (AndroidJavaClass cl = new AndroidJavaClass(pluginpackage))
            {
                cl.CallStatic("cancelAllNotify");
            }
        }

        public void OnApplicationPause(bool paused)
        {
            if (paused)
            {
                SetAppActivity(false);
            }
            else
            {
                SetAppActivity(true);
            }
        }
    }
}

