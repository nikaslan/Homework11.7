using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Homework10._8
{
    internal class Manager : IReadUser, IUpdateUser
    {
        public Manager() { }
                
        public Dictionary<string, bool> GetClientDataEditAccess()
        {
            Dictionary<string, bool> access = new Dictionary<string, bool>
            {
                { "FirstName", true },
                { "LastName", true },
                { "FatherName", true },
                { "PhoneNumber", true },
                { "Passport", true },
                { "NewClient", true }
            };
            return access;
        }

        public Dictionary<string, string> ShowClientData(ClientsRepository repository, int i)
        {
            Dictionary<string, string> client = new Dictionary<string, string>();

            client.Add("FirstName", repository.GetClients()[i].FirstName);
            client.Add("LastName", repository.GetClients()[i].LastName);
            client.Add("FatherName", repository.GetClients()[i].FatherName);
            client.Add("PhoneNumber", repository.GetClients()[i].PhoneNumber);
            string passport = "";
            if (repository.GetClients()[i].Passport != null && repository.GetClients()[i].Passport.Trim() != "")
            {
                passport = repository.GetClients()[i].Passport;
            }
            client.Add("Passport", passport);
            client.Add("UpdateLog",repository.GetClients()[i].GetLastUpdateLog());
            return client;
        }

        public void UpdateClientData(ClientsRepository repository, Client updatedClient, int clientPosition)
        {
            
            string fieldsUpdated = "";
            string updatesType = "";
            string tempUpdateType;

            if (clientPosition != -1)
            {
                // проверяем каждое поле на наличие изменений и их тип
                tempUpdateType = GetTypeOfChange(repository.GetClients()[clientPosition].FirstName, updatedClient.FirstName);
                if (tempUpdateType != "")
                {
                    fieldsUpdated += "first name;";
                    updatesType += tempUpdateType;
                }
                tempUpdateType = GetTypeOfChange(repository.GetClients()[clientPosition].LastName, updatedClient.LastName);
                if (tempUpdateType != "")
                {
                    fieldsUpdated += "last name;";
                    updatesType += tempUpdateType;
                }
                tempUpdateType = GetTypeOfChange(repository.GetClients()[clientPosition].FatherName, updatedClient.FatherName);
                if (tempUpdateType != "")
                {
                    fieldsUpdated += "father name;";
                    updatesType += tempUpdateType;
                }
                tempUpdateType = GetTypeOfChange(repository.GetClients()[clientPosition].PhoneNumber, updatedClient.PhoneNumber);
                if (tempUpdateType != "")
                {
                    fieldsUpdated += "phone number;";
                    updatesType += tempUpdateType;
                }
                tempUpdateType = GetTypeOfChange(repository.GetClients()[clientPosition].Passport, updatedClient.Passport);
                if (tempUpdateType != "")
                {
                    fieldsUpdated += "passport;";
                    updatesType += tempUpdateType;
                }

                if (fieldsUpdated == "") return; // если поля не изменились, то просто отменяем запись
                //удаляем последний ";" символ в каждой строке
                fieldsUpdated.Substring(0, fieldsUpdated.Length - 1);
                updatesType.Substring(0, updatesType.Length - 1);
            }
            else
            {
                fieldsUpdated = "new client";
                updatesType = "added";
                clientPosition = repository.GetBaseSize();
            }


            repository.UpdateClientInfo(updatedClient, clientPosition, "Manager", fieldsUpdated, updatesType);
        }

        public string GetTypeOfChange(string existingClientData, string newClientData)
        {
            string updateType = "";
            if (existingClientData == newClientData.Trim()) return updateType; // если данные не изменились - отмена

            if (existingClientData == "") updateType = "added;"; // если в базе нет записи и мы что-то добавляем
            else  // если же в базе что-то было
            {
                if (newClientData.Trim() == "") updateType = "deleted;"; // а в новой записи ничего нет - значит удалили
                else updateType = "updated;"; // если в новой записи что-то есть - значит обновили
            }
            return updateType;
        }
    }
}
