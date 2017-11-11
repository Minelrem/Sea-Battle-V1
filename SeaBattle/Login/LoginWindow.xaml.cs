using SeaBattle.Registration;
using SeaBattle.Service;
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
using System.Windows.Shapes;

namespace SeaBattle.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private bool _isSucces;

        public bool IsSucces => _isSucces;


        public LoginWindow()
        {
            InitializeComponent();
            _isSucces = false;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if ( (await UnitOfWork.Instance.UserService.VerifyUser(mailTbt.Text,passTbt.Password))!=null)
            {
                _isSucces = true;
                Close();                                
            }
            else
            {
                mailTbt.Text = "Email or pass is incorrect";
                mailTbt.FontSize = 15;
            }
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrWindow = new RegistrationWindow();
            registrWindow.ShowDialog();
            
        }
    }
}
