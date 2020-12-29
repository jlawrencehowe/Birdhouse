using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class NotificationHandler : MonoBehaviour
{

    static GameObject nh;

    private int tenantNotifID;
    private int adNotifID;
    private GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        if (nh == null)
        {
            nh = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
        }
        AndroidNotificationCenter.CancelAllNotifications();
        if(GameObject.Find("GameController") != null)
            gc = GameObject.Find("GameController").GetComponent<GameController>();

        var c = new AndroidNotificationChannel()
        {
            Id = "1",
            Name = "TenantCollectReady",
            Importance = Importance.High,
            Description = "Tenant Rent is ready to collect",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);

        var d = new AndroidNotificationChannel()
        {
            Id = "2",
            Name = "AdCountDown",
            Importance = Importance.High,
            Description = "Ad timer has runout",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(d);

        var t = new AndroidNotificationChannel()
        {
            Id = "3",
            Name = "Test",
            Importance = Importance.High,
            Description = "DescrTest",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(t);
        /*
        var notification = new AndroidNotification();
        notification.Title = "Test Notification";
        notification.Text = "This is nothing more than a test to make sure the icon loads properly";
        notification.FireTime = System.DateTime.Now.AddSeconds(5);
        notification.SmallIcon = "smallicon";
        notification.LargeIcon = "largeicon";
        tenantNotifID = AndroidNotificationCenter.SendNotification(notification, "3");
        */

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call<bool>("moveTaskToBack", true);
            }
            else
            {
                Application.Quit();
            }
        }
    }
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && PlayerPrefHandler.GetNotification())
        {
            if (gc == null && GameObject.Find("GameController") != null)
            {
                gc = GameObject.Find("GameController").GetComponent<GameController>();
            }
            else {
                int timer = gc.GenerateMaxTime();
                if (timer > 30)
                {

                    var notification = new AndroidNotification();
                    notification.Title = "Rent is ready!";
                    notification.Text = "Tenant rent is ready for collection. Come collect it so you can generate more!";
                    notification.FireTime = System.DateTime.Now.AddMinutes(gc.GenerateMaxTime());
                    notification.SmallIcon = "smallicon";
                    notification.LargeIcon = "largeicon";
                    tenantNotifID = AndroidNotificationCenter.SendNotification(notification, "1");
                }
            }
            if (Tenant.GetIsRequestActive())
            {
                double timer = 0;
                bool requestFound = false;
                for(int i = 0; i < gc.tenants.Count; i++)
                {
                    if (gc.tenants[i].GetComponent<Tenant>().GetIsTenantRequestActive())
                    {
                        timer = gc.tenants[i].GetComponent<Tenant>().GetRequestCompletionTime();
                        requestFound = true;
                        break;
                    }
                }
                if (requestFound)
                {
                    var notification = new AndroidNotification();
                    notification.Title = "Request finished!";
                    notification.Text = "A request has finished. See what the tenant has to say!";
                    notification.FireTime = System.DateTime.Now.AddMinutes(timer);
                    notification.SmallIcon = "smallicon";
                    notification.LargeIcon = "largeicon";
                    AndroidNotificationCenter.SendNotification(notification, "2");
                }
            }

            //savedData.saveData("save1");
        }
        else
        {
            AndroidNotificationCenter.CancelAllNotifications();
            

        }
    }
        public void RequestNotificationCreate(int time)
        {
            var notification = new AndroidNotification();
            notification.Title = "Request finished!";
            notification.Text = "A request has finished. See what the tenant has to say!";
            notification.FireTime = System.DateTime.Now.AddMinutes(time);
            notification.SmallIcon = "smallicon";
            notification.LargeIcon = "largeicon";
        AndroidNotificationCenter.SendNotification(notification, "2");
        }

        public void ClearRequestNotification()
        {
            AndroidNotificationCenter.CancelNotification(2);
        }
    }
