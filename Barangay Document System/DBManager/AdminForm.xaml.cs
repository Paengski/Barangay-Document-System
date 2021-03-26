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

namespace Barangay_Document_System.DBManager
{
   /// <summary>
   /// Interaction logic for AdminForm.xaml
   /// </summary>
   public partial class AdminForm : Window
   {
      public AdminForm()
      {
         InitializeComponent();
      }

      private void Close_Button(object sender, RoutedEventArgs e)
      {
         Close();
      }

      private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         DragMove();
      }

      private void Add_Button(object sender, RoutedEventArgs e)
      {
         string user = v_user.Text;
         string pass = v_password.Text;
         string hint = v_hint.Text;

         string query = $"INSERT INTO APP_USER (username,userpassword,hint) VALUES ('{user}',md5('{pass}'),'{hint}')";
         try
         {
            var count = DBOperation.ExecuteNonQuery(query);
            if(count > 0)
            {
               MessageBox.Show("Done", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
               Close();
               return;
            }
         }
         catch (Npgsql.PostgresException)
         {
            MessageBox.Show("Failed", "Username must be unique", MessageBoxButton.OK, MessageBoxImage.Warning);
         }
         MessageBox.Show("Failed", "Username must be unique", MessageBoxButton.OK, MessageBoxImage.Warning);
      }
   }
}
