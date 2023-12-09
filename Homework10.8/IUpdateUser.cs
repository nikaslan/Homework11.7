using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10._8
{
    internal interface IUpdateUser
    {
        /// <summary>
        /// Запись изменений клиента в базу
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="updatedClient"></param>
        /// <param name="clientPosition"></param>
        void UpdateClientData(ClientsRepository repository, Client updatedClient, int clientPosition);
    }
}
