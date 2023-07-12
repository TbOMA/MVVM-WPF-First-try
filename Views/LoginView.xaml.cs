using MVVM_FirsTry.Controls;
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

namespace MVVM_FirsTry.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public static readonly DependencyProperty AdminLoginCommandProperty =
            DependencyProperty.Register("AdminLoginCommand", typeof(ICommand), typeof(LoginView), new PropertyMetadata(null));


        public ICommand AdminLoginCommand
        {
            get { return (ICommand)GetValue(AdminLoginCommandProperty); }
            set { SetValue(AdminLoginCommandProperty, value); }
        }
        public LoginView()
        {
            InitializeComponent();
        }
        private void AdminLogin_Click(object sender, RoutedEventArgs e)
        {
            if (AdminLoginCommand != null)
            {
                string password = pbPassword.Password;
                AdminLoginCommand.Execute(password);
            }

        }
        private void pbPassword_PasswordChanged(object sender, RoutedEventArgs e)
                => tblPasswordHint.Visibility = pbPassword.Password.Length == 0 ? Visibility.Visible : Visibility.Hidden;
    } 
}
