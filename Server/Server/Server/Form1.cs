using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Fleck;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Server
{
    public partial class Form1 : Form
    {
        WebSocketServer server;
        List<IWebSocketConnection> sockets = new List<IWebSocketConnection>(); //list of client sockets connected
        //List<IWebSocketConnection> requestingSockets = new List<IWebSocketConnection>(); //list of client sockets requesting to speak
        ObservableCollection<IWebSocketConnection> requestingSockets = new ObservableCollection<IWebSocketConnection>(); //list of client sockets requesting to speak

        IWebSocketConnection _currentSpeaker;

        IWebSocketConnection currentSpeaker
        {
            get
            {
                return _currentSpeaker;
            }
            set
            {
                if(_currentSpeaker != null)
                {
                    //send message to current speaker to turn off mic
                    RootObject root = new RootObject();
                    root.Data.Type = "mic";
                    root.Data.On = false;
                    root.Data.Message = "mic off";
                    string json = JsonConvert.SerializeObject(root);
                    _currentSpeaker.Send(json);
                }

                _currentSpeaker = value;

                if (value != null)
                    Invoke(new Action(() => { lblCurrentSpeaker.Text = value.ConnectionInfo.Id.ToString(); }));
                
                else
                    Invoke(new Action(() => { lblCurrentSpeaker.Text = "None"; }));                
            }
        }

        public Form1()
        {
            InitializeComponent();

            //when requesting sockets change, clear and display requests
            requestingSockets.CollectionChanged += (s, e) =>
            {
                //remove all requesting controls
                //foreach (Control item in Controls)
                //{
                    //if (item.Name.Equals("request"))
                        //Invoke(new Action(() => { Controls.Remove(item); }));
                //}

                for(int i = 0; i < Controls.Count; i++)
                {
                    if(Controls[i].Name.Equals("request"))
                    {
                        Invoke(new Action(() => { Controls.Remove(Controls[i]); }));
                        i--; //subtract one because by removing a control, just decreased the controls list count, could create list of controls to remove then remove after instead
                    }
                }

                //displays requesting controls
                DisplayRequestingSockets();
            };

            server = new WebSocketServer("ws://10.92.84.183:80");

            server.Start((socket) =>
           {
               //when a client socket connects
               socket.OnOpen = () =>
               {
                   Invoke(new Action(() => { rtbMessages.AppendText(String.Format("{0} connected.", socket.ConnectionInfo.Id) + Environment.NewLine); }));
                   sockets.Add(socket); //add client socket new list
               };

               //when a client socket disconnects
               socket.OnClose = () =>
               {
                   Invoke(new Action(() => { rtbMessages.AppendText(String.Format("{0} disconnected.", socket.ConnectionInfo.Id) + Environment.NewLine); }));
                   sockets.Remove(socket); //remove client socket from list
                   requestingSockets.Remove(socket); //remove client socket from requesitng list too

                   //if disconnected socket is current speaker, set current speaker to null (none)
                   if (currentSpeaker == socket)
                       currentSpeaker = null;
               };

               //when the sv receives a message from a client
               socket.OnMessage = (message) =>
               {
                   RootObject root = JsonConvert.DeserializeObject<RootObject>(message); //deserialize json message into obj

                   switch(root.Data.Type)
                   {
                       case "request":
                           Invoke(new Action(() => { rtbMessages.AppendText(socket.ConnectionInfo.Id + " wants to speak." + Environment.NewLine); }));

                           //add socket to requesting to speak
                           requestingSockets.Add(socket);
                           break;
                   }
               };
           });

            //sends message to all clients
            btnPing.Click += (s, e) =>
            {
                foreach(var socket in sockets)
                {
                    RootObject root = new RootObject();
                    root.Data.Type = "ping";
                    root.Data.Message = "Server ping.";
                    string json = JsonConvert.SerializeObject(root);
                    socket.Send(json);
                }
            };

            //turn off current speaker mic = no current speaker
            btnTurnOffCurrentSpeaker.Click += (s, e) =>
            {
                currentSpeaker = null;
            };

            rtbMessages.AppendText("Server started." + Environment.NewLine);
        }

        //display id, yes and no buttons for client requests to speak
        void DisplayRequestingSockets()
        {
            int counter = 0; //used for positioning of controls

            foreach(var socket in requestingSockets)
            {
                //id
                Label lbl = new Label();
                lbl.Name = "request";
                lbl.Text = socket.ConnectionInfo.Id.ToString();
                lbl.Location = new Point(13, 225 + (40*counter));

                //yes btn
                Button btnYes = new Button();
                btnYes.Name = "request";
                btnYes.Text = "Yes";
                btnYes.Location = new Point(125, 225 + (40 * counter));
                btnYes.Click += (s, e) =>
                {
                    RootObject root = new RootObject();
                    root.Data.Type = "mic";
                    root.Data.On = true;
                    root.Data.Message = "mic on";
                    string json = JsonConvert.SerializeObject(root);
                    socket.Send(json);

                    //remove client socket from requesting
                    requestingSockets.Remove(socket);

                    //assign socket as current speaker socket
                    currentSpeaker = socket;
                };

                //no btn
                Button btnNo = new Button();
                btnNo.Name = "request";
                btnNo.Text = "No";
                btnNo.Location = new Point(225, 225 + (40 * counter));
                btnNo.Click += (s, e) =>
                {
                    RootObject root = new RootObject();
                    root.Data.Type = "mic";
                    root.Data.On = false;
                    root.Data.Message = "mic off";
                    string json = JsonConvert.SerializeObject(root);
                    socket.Send(json);

                    //remove client socket from requesting
                    requestingSockets.Remove(socket);
                };

                //add controls to view
                Invoke(new Action(() => {
                    Controls.Add(lbl);
                    Controls.Add(btnYes);
                    Controls.Add(btnNo);
                }));                

                //increment counter
                counter++;
            }
        }
    }
}
