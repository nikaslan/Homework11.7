using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10._8
{
    internal interface IDeleteUser
    {
        /// <summary>
        /// Удаление клиента из базы
        /// </summary>
        /// <param name="repository">Репозиторий</param>
        /// <param name="clientPosition">Позиция клиента в репозитории</param>
        string DeleteClientFromBase(ClientsRepository repository, int clientPosition);
    }
}
