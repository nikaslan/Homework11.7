using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10._8
{
    internal interface IReadUser
    {
        /// <summary>
        /// Получение записи одного клиента
        /// </summary>
        /// <param name="repository">ссылка на базу клиентов</param>
        /// <param name="i">порядковый номер клиента в базе</param>
        /// <returns></returns>
        Dictionary<string, string> ShowClientData(ClientsRepository repository, int i);

        /// <summary>
        /// список полей, доступных для редактирования
        /// </summary>
        /// <returns></returns>
        Dictionary<string, bool> GetClientDataEditAccess();
    }
}
