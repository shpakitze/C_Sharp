using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Collections;
namespace ChatServices
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatService : IChatService
    {
        public IClientCallback callback = null;
        //public List<IClientCallback> callbackClient = new List<IClientCallback>();
        public Dictionary<string, IClientCallback> callbackClient = new Dictionary<string, IClientCallback>();
        public void SendUserMess(string mess)
        {
            throw new NotImplementedException();
        }
        public void SendMess(string mess)
        {
            IClientCallback currCallback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            string currUser = "";
            foreach (var item in callbackClient)
            {
                if (item.Value == currCallback) currUser = item.Key;
            }
            foreach (var item in callbackClient)
            {
                if ((item.Value != null) && (item.Key != currUser))
                {
                    try
                    {
                        item.Value.GetMess("<" + currUser + " " + DateTime.Now.ToShortTimeString() + " > " + mess);
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
        }
        public string[] ConnectToChat(string name)
        {
            SystemBroadcastMess("user " + name + " is online");
            foreach (var item in callbackClient)
            {
                item.Value.GetNewUser(name);

            }
            string[] res = callbackClient.Keys.ToArray<string>();
            callbackClient.Add(name, OperationContext.Current.GetCallbackChannel<IClientCallback>());
            return res;
        }
        public void DisconnectUser(string name)
        {
            callbackClient.Remove(name);
            foreach (var item in callbackClient)
            {
                try
                {
                    item.Value.DelUser(name);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            SystemBroadcastMess("user " + name + " is offline");
        }
        public void SystemBroadcastMess(string mess)
        {
            foreach (var item in callbackClient)
            {
                try
                {
                    item.Value.GetMess(mess);
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
    }
}
