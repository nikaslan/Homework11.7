using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Homework10._8
{
    internal class Client
    {
        private string firstName;
        private string lastName;
        private string fatherName;
        private string phoneNumber;
        private string passport;
        private string lastUpdatedTime;
        private string lastUpdatedBy;
        private string lastUpdatedType;
        private string lastUpdatedData;

        #region Properties
        public string FirstName 
        { 
            get { return firstName; }
            private set { this.firstName = value; } 
        }
        public string LastName 
        {
            get { return lastName; }
            private set { this.lastName = value; }
        }
        public string FatherName 
        {
            get { return fatherName; }
            private set { this.fatherName = value; } 
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            private set { this.phoneNumber = value; }
        }
        public string Passport
        {
            get { return passport; }
            private set { this.passport = value; }
        }
        public string LastUpdatedTime
        {
            get { return lastUpdatedTime; }
            private set { this.lastUpdatedTime = value; }
        }
        public string LastUpdatedBy
        {
            get { return lastUpdatedBy; }
            private set { this.lastUpdatedBy = value; }
        }
        public string LastUpdatedType
        {
            get { return lastUpdatedType; }
            private set { this.lastUpdatedType = value; }
        }
        public string LastUpdatedData
        {
            get { return lastUpdatedData; }
            private set { this.lastUpdatedData = value; }
        }
        #endregion

        [JsonConstructor] public Client(string firstName, string lastName, string fatherName, string phoneNumber, string passport, 
            string lastUpdatedTime, string lastUpdatedBy, string lastUpdatedType, string lastUpdatedData) 
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.FatherName = fatherName;
            this.PhoneNumber = phoneNumber;
            this.Passport = passport;
            this.LastUpdatedTime = lastUpdatedTime;
            this.LastUpdatedBy = lastUpdatedBy;
            this.LastUpdatedType = lastUpdatedType;
            this.LastUpdatedData = lastUpdatedData;             
        }
        public Client(string firstName, string lastName, string fatherName, string phoneNumber, string passport) : this(firstName, 
            lastName, fatherName, phoneNumber, passport, null, null,null,null)
        {
        }

        /// <summary>
        /// Получение полного имени пользователя
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            string fullName = $"{this.LastName} {this.FirstName} {this.FatherName}";
            return fullName;
        }

        /// <summary>
        /// Обновляем данные по последнему изменению записи
        /// </summary>
        /// <param name="newBy">Кто последний изменил</param>
        /// <param name="newData">Какое поле было последним изменено</param>
        /// <param name="newType">Как было изменено поле</param>
        public void LastUpdateInfoSet(string newBy, string newData, string newType)
        {
            this.LastUpdatedBy = newBy;
            this.LastUpdatedType = newType;
            this.LastUpdatedData = newData;            
            this.LastUpdatedTime = DateTime.Now.ToString();
        }
        /// <summary>
        /// Возвращает историю последнего изменения в записи клиента 
        /// </summary>
        /// <returns>Строку с перечнем изменений</returns>
        public string GetLastUpdateLog()
        {
            if (this.lastUpdatedTime == null) return "No information about last update.";
            string[] updatedFields = this.lastUpdatedData.Split(';');
            string[] updateTypes = this.lastUpdatedType.Split(';');
                        
            string updateLog = $"Last updated on\n{this.LastUpdatedTime}\n{this.LastUpdatedBy}:\n";

            for (int i = 0;i<updatedFields.Length;i++)
            {
                updateLog += $"{updateTypes[i]} {updatedFields[i]}\n";
            }

            return updateLog;
        }

    }
}
