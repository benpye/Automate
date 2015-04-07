using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;

using DukSharp;
using System.Collections.Generic;

namespace Automate
{
    public static class UIModule
    {
        public static void showToast(string message, bool l)
        {
            Toast.MakeText(AutoApp.Context, message, l ? ToastLength.Long : ToastLength.Short).Show();
        }

        public static void showNotification(string title, string text)
        {
            Notification notification = new Notification.Builder(AutoApp.Context)
                .SetContentTitle(title)
                .SetContentText(text)
                .SetSmallIcon(Resource.Drawable.Icon)
                .Build();

            NotificationManager nm = AutoApp.Context.GetSystemService(Context.NotificationService) as NotificationManager;
            nm.Notify(0, notification);
        }
    }

    [Activity(Label = "Automate", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ActionBarActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.Toolbar);
            SetSupportActionBar(toolbar);

            ScriptEngine se = new ScriptEngine();
            se.AddModule(typeof(UIModule));
            se.EvalString(@"
            UI.showToast('Hello world from JavaScript', true);
            UI.showNotification('JavaScript', 'Can also show notifications');
            ");
        }
    }
}


