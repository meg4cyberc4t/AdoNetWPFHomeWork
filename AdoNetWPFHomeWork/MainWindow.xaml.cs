using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Configuration;

namespace AdoNetWPFHomeWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DatabaseController dbController = new DatabaseController();


        public MainWindow()
        {
            InitializeComponent();
            GetCountUsers() ;
            GetMaxIdUsers();
            GetMinIdUsers();
        }

        public async void GetCountUsers()
        {
            int value = await dbController.CountUsers();
            TBUsersCount.Text = value.ToString();
        }


        public async void GetMaxIdUsers()
        {
            int value = await dbController.MaxIdUsers();
            TBUsersMax.Text = value.ToString();
        }

        public async void GetMinIdUsers()
        {
            int value = await dbController.MinIdUsers();
            TBUsersMin.Text = value.ToString();
        }


        private async void  mainDataGrid_Initialized(object sender, EventArgs e)
        {
            mainDataGrid.ItemsSource = await dbController.SelectAll();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await dbController.Insert(
              new User(
               null,
               TBF.Text, 
               TBI.Text, 
               TBO.Text, 
               TBPassNumber.Text, 
               TBPassSeries.Text, 
               TBPassIssues.Text, 
               TBPhone.Text, 
               TBEmail.Text
               )
              );
            mainDataGrid.Items.Refresh();
        }

    }
}
