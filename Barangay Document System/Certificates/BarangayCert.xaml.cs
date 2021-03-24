using System;
using System.Printing;
using System.Windows;
using System.Windows.Controls;

namespace Barangay_Document_System.Certificates
{
   /// <summary>
   /// Interaction logic for BarangayCert.xaml
   /// </summary>
   public partial class BarangayCert : Window
   {
      public BarangayCert()
      {
         InitializeComponent();
      }

      private void Print_Button(object sender, RoutedEventArgs e)
      {
         try
         {
            IsEnabled = false;
            Visibility = Visibility.Hidden;
            PrintDialog pd = new PrintDialog
            {
               PrintQueue = new PrintQueue(new PrintServer(), "Microsoft Print to PDF")
            };
            pd.PrintVisual(print, "Barangay Clearance");
            string query = $"INSERT INTO BDS.RESIDENTLOG (fullname,certtype,dateissued) VALUES ('{v_name1.Text}','Barangay Clearance','{DateTime.Now}')";
            _ = DBOperation.ExecuteNonQuery(query);
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
