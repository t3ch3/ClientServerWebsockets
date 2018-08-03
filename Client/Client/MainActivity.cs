using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

using WebSocket4Net;
using Newtonsoft.Json;

namespace Client
{
    public enum MicState
    {
        On,
        Off,
        Requesitng
    }

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        WebSocket webSocket;
        Button btnSpeak;
        Button btnConnect;
        RadioButton rbMic;

        MicState _micState;

        public MicState micState
        {
            get
            {
                return _micState;
            }
            set
            {
                _micState = value;

                switch(value)
                {
                    case MicState.Off:
                        btnSpeak.Enabled = true;
                        btnSpeak.Text = "Request to speak";
                        btnSpeak.Visibility = Android.Views.ViewStates.Visible;

                        rbMic.Checked = false;
                        break;
                    case MicState.On:
                        btnSpeak.Visibility = Android.Views.ViewStates.Invisible;

                        rbMic.Checked = true;
                        break;
                    case MicState.Requesitng:
                        btnSpeak.Enabled = false;
                        btnSpeak.Text = "Requesting..";
                        btnSpeak.Visibility = Android.Views.ViewStates.Visible;
                        break;
                }
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //get controls from layout
            btnConnect = FindViewById<Button>(Resource.Id.btnConnect);
            btnSpeak = FindViewById<Button>(Resource.Id.btnSpeak);
            rbMic = FindViewById<RadioButton>(Resource.Id.rbMic);

            webSocket = new WebSocket("ws://10.92.84.183:80", "");

            //client socket connected
            webSocket.Opened += (s, e) =>
            {
                RunOnUiThread(() =>
                {
                    Toast.MakeText(this, "Connected.", ToastLength.Short).Show();
                    btnConnect.Enabled = false;
                    btnConnect.Text = "Connected.";

                    btnSpeak.Enabled = true;
                });
            };

            //client socket disconnected
            webSocket.Closed += (s, e) =>
            {
                RunOnUiThread(() =>
                {
                    Toast.MakeText(this, "Disconnected.", ToastLength.Short).Show();
                    btnConnect.Enabled = true;
                    btnConnect.Text = "Connect";

                    micState = MicState.Off;

                    btnSpeak.Enabled = false;
                });
            };

            //client socket received a message
            webSocket.MessageReceived += (s, e) =>
            {
                RootObject root = JsonConvert.DeserializeObject<RootObject>(e.Message); //deserialize json message into obj

                switch(root.Data.Type)
                {
                    case "ping":
                        RunOnUiThread(() =>
                        {
                            Toast.MakeText(this, root.Data.Message, ToastLength.Short).Show();
                        });
                        break;

                    case "mic":
                        RunOnUiThread(() =>
                        {
                            if (root.Data.On == true)
                            {
                                Toast.MakeText(this, root.Data.Message, ToastLength.Short).Show();
                                micState = MicState.On;
                            }
                            else
                            {
                                Toast.MakeText(this, root.Data.Message, ToastLength.Short).Show();
                                micState = MicState.Off;
                            }
                        });
                        break;
                }
            };

            //connect to websocket sv
            btnConnect.Click += (s, e) =>
            {
                if(webSocket.State == WebSocketState.Closed || webSocket.State == WebSocketState.None)
                    webSocket.Open();
            };

            //send request to sv to speak/turn mic on
            btnSpeak.Click += (s, e) =>
            {
                micState = MicState.Requesitng;

                RootObject root = new RootObject();
                root.Data.Type = "request";
                string json = JsonConvert.SerializeObject(root);
                webSocket.Send(json);
            };
        }
    }
}

