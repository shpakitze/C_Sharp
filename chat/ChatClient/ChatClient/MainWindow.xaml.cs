using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatClient.ServiceReference1;
using System.ServiceModel;
namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public class CallbackHandler : IChatServiceCallback
    {
        public delegate void SenderHandler(string val);
        public event SenderHandler onGetMess;
        public event SenderHandler onNewUser;
        public event SenderHandler onDelUser;
        public CallbackHandler Handler;
        //static InstanceContext site = new InstanceContext(Handler);
        //public static ChatServiceClient proxy = new ChatServiceClient(site);
        public void GetMess(string mess)
        {
            onGetMess(mess);
        }
        public void Init()
        {
            Handler = new CallbackHandler();
        }
        public void GetNewUser(string name)
        {
            onNewUser(name);
        }
        public void DelUser(string name)
        {
            onDelUser(name);
        }
    }
    public class ChatProxy
    {
        public ChatServiceClient proxy;
        private InstanceContext site;
        public ChatProxy(CallbackHandler handler)
        {
            site = new InstanceContext(handler);
            proxy = new ChatServiceClient(site);
        }
    }
    public static class Chat
    {
        public static string name { set; get; }
    }
    public partial class MainWindow : Window
    {
        public ChatServiceClient currentProxy;
        public MainWindow()
        {
            InitializeComponent();
            //ChatProxy proxy = new ChatProxy(handler);
            //ChatProxy.Init();
            //ChatProxy.Current.SendMessage();
            CallbackHandler callBackHandler = new CallbackHandler();
            callBackHandler.Init();
            ChatProxy chatProxy = new ChatProxy(callBackHandler.Handler);
            currentProxy = chatProxy.proxy;
            DisconnectButton.IsEnabled = false;
            SendButton.IsEnabled = false;
            callBackHandler.Handler.onGetMess += this.AddMessToWindow;
            callBackHandler.Handler.onNewUser += this.AddNewUser;
            callBackHandler.Handler.onDelUser += this.DelUser;
        }
        public void AddMessToWindow(string mess)
        {
            ChatText.Items.Add(mess);
        }
        public void AddNewUser(string name)
        {
            UsersList.Items.Add(name);
        }
        public void DelUser(string name)
        {
            UsersList.Items.Remove(name);
        }
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            currentProxy.SendMess(MessageTextBox.Text);
            ChatText.Items.Add(MessageTextBox.Text);
            MessageTextBox.Text = "";
        }
        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectWindow connectWindow = new ConnectWindow();
            connectWindow.Owner = this;
            connectWindow.ShowDialog();
            string[] usersList = currentProxy.ConnectToChat(Chat.name);
            ConnectButton.IsEnabled = false;
            DisconnectButton.IsEnabled = true;
            foreach (var item in usersList)
            {
                UsersList.Items.Add(item);
            }
            UsersList.Items.Add("you " + Chat.name);
            SendButton.IsEnabled = true;
        }
        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            currentProxy.DisconnectUser(Chat.name);
            DisconnectButton.IsEnabled = false;
            ConnectButton.IsEnabled = true;
            SendButton.IsEnabled = false;
            UsersList.Items.Clear();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            currentProxy.DisconnectUser(Chat.name);
        }
    }
}
