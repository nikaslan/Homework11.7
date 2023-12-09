using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10._8
{
    internal class Consultant : IReadUser, IUpdateUser
    {
        public Consultant() { }
        
        public Dictionary<string,string> ShowClientData(ClientsRepository repository, int i)
        { 
            Dictionary<string, string> client = new Dictionary<string,string>();

            client.Add("FirstName", repository.GetClients()[i].FirstName);
            client.Add("LastName", repository.GetClients()[i].LastName);
            client.Add("FatherName", repository.GetClients()[i].FatherName);
            client.Add("PhoneNumber", repository.GetClients()[i].PhoneNumber);
            string passport = "";
            if (repository.GetClients()[i].Passport!="")
            {
                passport = "**** ******";
            }
            client.Add("Passport", passport);
            client.Add("UpdateLog", "Last update info is unavailable.");

            return client;
        }

        public Dictionary<string, bool> GetClientDataEditAccess()
        {
            Dictionary<string, bool> access = new Dictionary<string, bool>
            {
                { "FirstName", false},
                { "LastName", false },
                { "FatherName", false },
                { "PhoneNumber", true },
                { "Passport", false },
                { "NewClient", false }
            };
            return access;
        }

        public void UpdateClientData(ClientsRepository repository, Client updatedClient, int clientPosition)
        {
            string updateType = GetListOfChanges(repository.GetClients()[clientPosition].PhoneNumber, updatedClient.PhoneNumber);
            if (updateType == "") return; //если не было изменений - ничего не записываем

            Client tempClient = new Client(repository.GetClients()[clientPosition].FirstName, repository.GetClients()[clientPosition].LastName,
                repository.GetClients()[clientPosition].FatherName, updatedClient.PhoneNumber,
                repository.GetClients()[clientPosition].Passport);
            
            repository.UpdateClientInfo(tempClient, clientPosition, "Consultant", "phone number", updateType);
        }

        public string GetListOfChanges(string existingClientData, string newClientData)
        {
            string updateType = "";
            if (existingClientData == newClientData.Trim()) return updateType; // если данные не изменились - отмена
                        
            if (existingClientData == "") updateType = "added"; // если в базе нет записи и мы что-то добавляем
            else  // если же в базе что-то было
            {
                if (newClientData.Trim() == "") updateType = "deleted"; // а в новой записи ничего нет - значит удалили
                else updateType = "updated"; // если в новой записи что-то есть - значит обновили
            }
            return updateType;
        }

    }
}
