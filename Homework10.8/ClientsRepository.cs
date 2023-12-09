using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;


namespace Homework10._8
{
    internal class ClientsRepository
    {
        private string databasePath;
        private List<Client> clients;

        // для теста записи

        //private string updatedDatabasePath = "updatedClientsList.json";


        public ClientsRepository() : this("clientsList.json")
        {                                  
        }
        public ClientsRepository(string databasePath)
        {
            this.databasePath = databasePath; // путь до JSON файла с клиентами
        }
        
        /// <summary>
        /// Считываем список клиентов из файла базы
        /// </summary>
        public void ReadClientBaseFile()
        {
            string fileText = File.ReadAllText(this.databasePath);
            this.clients = JsonConvert.DeserializeObject<List<Client>>(fileText);
        }

        public int GetBaseSize() { return this.clients.Count; } // возвращает количество записей в базе клиентов

        public List<Client> GetClients() { return this.clients;} // возвращает список клиентов

        /// <summary>
        /// Записываем изменения в информации клиента 
        /// </summary>
        /// <param name="updatedClientInfo"></param>
        /// <param name="clientPosition"></param>
        public void UpdateClientInfo(Client updatedClientInfo, int clientPosition, string updatedBy, string updatedData, string updateType)
        {
            if (clientPosition == clients.Count) { clients.Add(updatedClientInfo); }
            else clients[clientPosition] = updatedClientInfo;
            clients[clientPosition].LastUpdateInfoSet(updatedBy, updatedData, updateType);
            UpdateClientBaseFile();
        }

        /// <summary>
        /// Записываем текущий список клиентов в файл
        /// </summary>
        private void UpdateClientBaseFile()
        {
            string fileText = JsonConvert.SerializeObject(this.clients);
            File.WriteAllText(this.databasePath, fileText);

            //File.WriteAllText(this.updatedDatabasePath, fileText); // тестовый файл записи
        }

    }
}
