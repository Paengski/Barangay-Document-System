using Barangay_Document_System.CommonFunction;
using Barangay_Document_System.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Barangay_Document_System
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         string workingDirectory = Environment.CurrentDirectory;
         string projectDirectory = System.IO.Path.Combine(Directory.GetParent(workingDirectory).Parent.FullName, "DatabaseConfig.json");

         //if (!File.Exists(projectDirectory))
         //{
         //   MessageBox.Show("Missing Database configuration", "", MessageBoxButton.OK, MessageBoxImage.Warning);
         //   throw new Exception();
         //}

         //var info = JsonConvert.DeserializeObject<DbInfo>(File.ReadAllText(projectDirectory));
         try
         {
            DBOperation.Connect("bds", "bds", "server=localhost; port=5432; database=postgres;");
         }
         catch (Exception)
         {
            MessageBox.Show("Database Connection Failed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
         }

         InitializeComponent();
         LoginPage loginPage = new LoginPage();
         loginPage.Show();
         Close();
      }

      private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         DragMove();
      }
   }
}
