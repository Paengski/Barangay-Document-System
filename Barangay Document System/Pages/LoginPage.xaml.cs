using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Input;

namespace Barangay_Document_System.Pages
{
   /// <summary>
   /// Interaction logic for LoginPage.xaml
   /// </summary>
   public partial class LoginPage : Window
   {
      public LoginPage()
      {
         InitializeComponent();
      }

      private void V_passwordbox_KeyDown(object sender, KeyEventArgs e)
      {
         if(e.Key == Key.Enter)
         {
            Login();
         }
      }

      private void LoginButton(object sender, RoutedEventArgs e)
      {
         Login();
      }

      private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         DragMove();
      }

      private void Login()
      {
         string userInput = v_username.Text;
         string passInput = v_passwordbox.Password;
         if (!string.IsNullOrEmpty(userInput) && !string.IsNullOrEmpty(passInput))
         {
            string queryUser = $"SELECT USERNAME FROM APP_USER WHERE USERNAME = '{userInput}' AND USERPASSWORD = md5('{passInput}')";
            var dt = DBOperation.ExecuteToDataTable(queryUser);
            if (dt != null)
            {
               Dashboard dashboard = new Dashboard();
               dashboard.Show();
               Close();
            }
            else
            {
               string queryuser = $"SELECT DISTINCT * FROM APP_USER WHERE USERNAME = '{userInput}'";
               var d = DBOperation.ExecuteToDataTable(queryuser);
               if (d != null)
               {
                  v_passwordbox.Password = "";
                  v_hint_text.Text = $"HINT: {d.Rows[0]["hint"]}";
               }
            }
         }
      }

      private void CloseButton(object sender, RoutedEventArgs e)
      {
         Close();
      }
   }
}
