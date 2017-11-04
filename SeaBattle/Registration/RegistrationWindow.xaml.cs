using SeaBattle.Api.Model.Model;
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

namespace SeaBattle.Registration
{
    /// <summary>
    /// Interaction logic for RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (passTbt.Password == confpassTbt.Password)
            {

                var res = await UnitOfWork.Instance.UserService.AddUser(new UserModel
                {
                    Email = mailTbt.Text,
                    Login = loginTbt.Text,
                    Name = nameTbt.Text,
                    Password = passTbt.Password
                });

                if (!res)
                {
                    mailTbt.Text = "The user with this email exist";
                    passTbt.Clear();
                    confpassTbt.Clear();

                }
                else
                {
                    //UnitOfWork.Instance.MailService.SendMail(mailTbt.Text); //sends email about registration confirmation
                    Close();
                }
            }
            else
            {
                confLbl.Content = "Passwords are not the same";
            }
}

    }
}
