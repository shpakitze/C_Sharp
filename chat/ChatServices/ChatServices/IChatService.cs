using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatServices
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(IClientCallback))]
    public interface IChatService
    {
        [OperationContract(IsOneWay = true)]
        void SendUserMess(string mess);
        [OperationContract(IsOneWay = true)]
        void SendMess(string mess);
        [OperationContract]
        string[] ConnectToChat(string name);
        [OperationContract(IsOneWay = true)]
        void DisconnectUser(string name);
        [OperationContract(IsOneWay = true)]
        void SystemBroadcastMess(string mess);
        // TODO: Добавьте здесь операции служб
    }
    public interface IClientCallback
    {
        [OperationContract(IsOneWay = true)]
        void GetMess(string mess);
        [OperationContract(IsOneWay = true)]
        void GetNewUser(string name);
        [OperationContract(IsOneWay = true)]
        void DelUser(string name);
    }
    // Используйте контракт данных, как показано на следующем примере, чтобы добавить сложные типы к сервисным операциям.
    // В проект можно добавлять XSD-файлы. После построения проекта вы можете напрямую использовать в нем определенные типы данных с пространством имен "ChatServices.ContractType".

}
