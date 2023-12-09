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

namespace Homework10._8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IReadUser userReader;
        IUpdateUser userUpdater;
        ClientsRepository clientBase;

        public MainWindow(int userRole)
        {
            InitializeComponent();
                        
            clientBase = new ClientsRepository();

            if (userRole == 1) 
            { 
                userReader = new Consultant();
                userUpdater = new Consultant();
            }
            if (userRole == 2)
            {
                userReader = new Manager();
                userUpdater = new Manager();
            }

            TextBoxClientAccess(userReader.GetClientDataEditAccess());
            ClientsLoad();

        }
        /// <summary>
        /// Задаем доступ к изменению полей в зависимости от пользователя
        /// </summary>
        /// <param name="accessList"></param>
        private void TextBoxClientAccess(Dictionary<string,bool> accessList)
        {
            TextBoxClientFirstName.IsReadOnly = !accessList["FirstName"];
            TextBoxClientLastName.IsReadOnly = !accessList["LastName"];
            TextBoxClientFatherName.IsReadOnly = !accessList["FatherName"];
            TextBoxClientPhoneNumber.IsReadOnly = !accessList["PhoneNumber"];
            TextBoxClientPassport.IsReadOnly = !accessList["Passport"];
            Button_AddNewClient.IsEnabled = accessList["NewClient"];
        }
        
        /// <summary>
        /// Загружаем список клиентов
        /// </summary>
        private void ClientsLoad()
        {
            ListBox_Clients.Items.Clear();

            clientBase.ReadClientBaseFile();
            int clientCount = clientBase.GetBaseSize();

            for (int i = 0; i < clientCount; i++)
            {
                ListBox_Clients.Items.Add(clientBase.GetClients()[i].GetFullName());
            }

        }
        /// <summary>
        /// Загружаем информацию конкретного клиента
        /// </summary>
        /// <param name="i"></param>
        private void ClientInfoLoad(int clientSelected)
        {
            Dictionary<string, string> client = userReader.ShowClientData(clientBase, clientSelected);
            TextBoxClientFirstName.Text = client["FirstName"];
            TextBoxClientLastName.Text = client["LastName"];
            TextBoxClientFatherName.Text = client["FatherName"];
            TextBoxClientPhoneNumber.Text = client["PhoneNumber"];
            TextBoxClientPassport.Text = client["Passport"];
            TextBlockClientUpdateLog.Text = client["UpdateLog"];
        }

        /// <summary>
        /// при выделении нового клиента из списка, подгружаем его данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_Clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox_Clients.SelectedIndex == -1) return; // если убрали выделение, то ничего не надо загружать
            int clientSelected = ListBox_Clients.SelectedIndex;
            ClientInfoLoad(clientSelected);
        }

        private void Button_SaveClientInfo_Click(object sender, RoutedEventArgs e)
        {
            int clientSelected = ListBox_Clients.SelectedIndex;
            UpdateClientInfo(clientSelected);            
            Console.WriteLine("Client Save button clicked");
        }

        /// <summary>
        /// Запускает проверку введенных данных, выдает ошибку или запускает запись в базу по результатам
        /// </summary>
        /// <param name="clientSelected">индекс выбранного клиента в списке</param>
        private void UpdateClientInfo(int clientSelected)
        {
            // проверка полей на соблюдение формата
            Dictionary<string,string> clientDataVerificationResults = VerifyClientData();

            // если есть ошибки, показываем текст ошибки и прекращаем добавление
            if (clientDataVerificationResults["ErrorMessage"]!="") 
            {
                MessageBox.Show(clientDataVerificationResults["ErrorMessage"]);
                return;
            }

            Client updatedClient = new Client(clientDataVerificationResults["FirstName"],
                clientDataVerificationResults["LastName"], clientDataVerificationResults["FatherName"],
                clientDataVerificationResults["PhoneNumber"],clientDataVerificationResults["Passport"]);

            userUpdater.UpdateClientData(clientBase, updatedClient, clientSelected);
            Console.WriteLine("UpdateClientInfo initiated");
            // Если не было выделено клиентов в списке, значит добавлен новый клиент и мы читаем его из конца базы
            if (clientSelected == -1) clientSelected = ListBox_Clients.Items.Count;
            
            ClientsLoad();
            ListBox_Clients.SelectedIndex = clientSelected; 
            ListBox_Clients.Focus();
            ClientInfoLoad(clientSelected);
        }
        /// <summary>
        /// Проверка формата данных пользователя
        /// </summary>
        /// <param name="clientData"></param>
        /// <returns></returns>
        private Dictionary<string,string> VerifyClientData()
        {
            Dictionary<string,string> clientDataStatus = new Dictionary<string,string>();

            clientDataStatus.Add("ErrorMessage", "");
            #region Проверка имени
            if (TextBoxClientFirstName.Text.Trim() == "")
            {
                clientDataStatus["ErrorMessage"] += "Имя не может быть пустым \n";
            }


            #endregion

            #region Проверка фамилии
            if (TextBoxClientLastName.Text.Trim() == "")
            {
                clientDataStatus["ErrorMessage"] += "Фамилия не может быть пустой \n";
            }

            #endregion

            #region Проверка отчества
            if (TextBoxClientFatherName.Text.Trim() == "")
            {
                clientDataStatus["ErrorMessage"] += "Отчество не может быть пустым \n";
            }

            #endregion

            #region Проверка паспорта

            #endregion

            #region Проверка номера телефона

            #endregion

            
            clientDataStatus.Add("FirstName", TextBoxClientFirstName.Text);
            clientDataStatus.Add("LastName", TextBoxClientLastName.Text);
            clientDataStatus.Add("FatherName", TextBoxClientFatherName.Text);
            clientDataStatus.Add("PhoneNumber", TextBoxClientPhoneNumber.Text);
            clientDataStatus.Add("Passport", TextBoxClientPassport.Text);
            
            return clientDataStatus;
        }

        private void Button_AddNewClient_Click(object sender, RoutedEventArgs e)
        {
            // при нажатии на создание нового клиента, мы убираем выделение из списка клиентов и обнуляем все текстовые поля
            ListBox_Clients.UnselectAll();
            TextBoxClientFirstName.Text = "";
            TextBoxClientLastName.Text = "";
            TextBoxClientFatherName.Text = "";
            TextBoxClientPhoneNumber.Text = "";
            TextBoxClientPassport.Text = "";
            TextBlockClientUpdateLog.Text = "Введите данные нового клиента и нажмите сохранить.";
            TextBoxClientFirstName.Focus();
            Console.WriteLine("adding new client");
        }
    }
}
