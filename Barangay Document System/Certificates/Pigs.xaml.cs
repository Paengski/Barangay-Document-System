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

namespace Barangay_Document_System.Certificates
{
   /// <summary>
   /// Interaction logic for Pigs.xaml
   /// </summary>
   public partial class Pigs : Window
   {
      public Pigs()
      {
         InitializeComponent();
      }

      private void Print_Button(object sender, RoutedEventArgs e)
      {
         try
         {
            IsEnabled = false;
            Visibility = Visibility.Hidden;
            PrintDialog printDlg = new PrintDialog();
            if (printDlg.ShowDialog() == true)
            {
               printDlg.PrintVisual(print, "Pigs");
            }
         }
         finally
         {
            IsEnabled = true;
            Visibility = Visibility.Visible;
         }
         Close();
      }
   }
}
