using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Plugin.AzurePushNotifications;
using Xamarin.Forms;


namespace TestFormsApp
{
    public class App : Application
    {
        public App()
        {

            PushNotificationCredentials.Tags = new string[] { };
            PushNotificationCredentials.GoogleApiSenderId = "google sender id";
            PushNotificationCredentials.AzureNotificationHubName = "hub name";
            PushNotificationCredentials.AzureListenConnectionString = "Endpoint";



            CrossAzurePushNotifications.Current.RegisterForAzurePushNotification();

            CrossAzurePushNotifications.Current.OnMessageReceived += (sender, ev) =>
            {
                Debug.WriteLine(ev.Content);
            };


            B_Clicked(null, null);
            var button = new Button();
            button.Text = "message";
            button.Clicked += B_Clicked;

            

            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "message"
                        },
                        button,
                    }
                }
            };
        }

        private void B_Clicked(object sender, EventArgs e)
        {
           




        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}