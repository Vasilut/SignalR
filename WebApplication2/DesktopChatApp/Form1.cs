using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopChatApp
{
    public partial class Form1 : Form
    {
        private IHubProxy chat;

        public Form1()
        {
            InitializeComponent();
        }

        private void bnConnect_Click(object sender, EventArgs e)
        {
            var hubConnection = new HubConnection("http://localhost:44251/");

            chat = hubConnection.CreateHubProxy("chat");

            chat.On<string>("newMessage",
                            msg => messages.Invoke(new Action(() =>
                                                              messages.Items.Add(msg))));
            //  do real async... ;)
            hubConnection.Start().Wait();
        }

        private void bnSend_Click(object sender, EventArgs e)
        {
            chat.Invoke<string>("sendMessage", tbMessage.Text);
        }
    }
}
