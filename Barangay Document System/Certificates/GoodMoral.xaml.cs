﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
   /// Interaction logic for GoodMoral.xaml
   /// </summary>
   public partial class GoodMoral : Window
   {
      public GoodMoral()
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
            pd.PrintVisual(print, "Good Moral");
            string query = $"INSERT INTO BDS.RESIDENTLOG (fullname,certtype,dateissued) VALUES ('{v_name1.Text}','Good Moral','{DateTime.Now}')";
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
