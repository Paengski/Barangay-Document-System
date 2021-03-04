using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Barangay_Document_System.DBManager
{
   /// <summary>
   /// Interaction logic for Insert.xaml
   /// </summary>
   public partial class Insert : Window
   {
      public static Guid ResidentId;
      private static string ImageStr = string.Empty;
      public Insert()
      {
         InitializeComponent();
      }

      private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         DragMove();
      }

      private void Close_Button(object sender, RoutedEventArgs e)
      {
         Close();
      }

      private void Save_Button(object sender, RoutedEventArgs e)
      {
         string lastname = v_lastname.Text;
         string firstname = v_firstname.Text;
         string middlename = v_middlename.Text;
         string gender = v_gender.Text;
         string birthdate = v_birthdate.Text;
         string birthplace = v_birthplace.Text;
         string civil = v_civilstatus.Text;
         string address = v_homeaddress.Text;
         string mobile = v_mobilenumber.Text;
         string image = ImageStr;
         var btnStr = ResidentButton.Content as string;
         if (btnStr.Contains("Update"))
         {
            var updateInfo = $"UPDATE bds.residentinfo SET lastname = '{lastname}'," +
               $"firstname = '{firstname}', middlename = '{middlename}', gender = '{gender}'," +
               $"birthdate = '{birthdate}',birthplace = '{birthplace}',civilstatus = '{civil}',houseaddress = '{address}',mobilenumber = '{mobile}' ,image = '{image}'" +
               $"WHERE id = '{ResidentId}'";

            var count = DBOperation.ExecuteNonQuery(updateInfo);
            if (count > 0)
            {
               Resident resident = new Resident()
               {
                  ResidentId = ResidentId,
                  LastName = lastname,
                  FirstName = firstname,
                  MiddleName = middlename,
                  Gender = gender,
                  BirthDate = DateTime.Parse(birthdate),
                  BirthPlace = birthplace,
                  CivilStatus = civil,
                  HouseAddress = address,
                  MobileNumber = mobile,
                  Image = image
               };
               Resident.UpdateResident(ResidentId, resident);
               MessageBox.Show("Updated", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
               Close();
            }
            else
            {
               MessageBox.Show("Failed, Check all the fields.");
            }
         }
         else
         {
            Guid guid = Guid.NewGuid();
            string insertQuery = $"INSERT INTO bds.residentinfo " +
               $"(id,firstname,middlename,lastname,gender,birthdate,birthplace,civilstatus,houseaddress,mobilenumber,image) VALUES " +
               $"('{guid}','{firstname}','{middlename}','{lastname}','{gender}'" +
               $",'{birthdate}','{birthplace}','{civil}','{address}','{mobile}','{image}')";

            var count = DBOperation.ExecuteNonQuery(insertQuery);
            if (count > 0)
            {
               Resident resident = new Resident()
               {
                  ResidentId = guid,
                  LastName = lastname,
                  FirstName = firstname,
                  MiddleName = middlename,
                  Gender = gender,
                  BirthDate = v_birthdate.DisplayDate,
                  BirthPlace = birthplace,
                  CivilStatus = civil,
                  HouseAddress = address,
                  MobileNumber = mobile,
                  Image = image
               };
               Resident.AddResident(resident);
               MessageBox.Show("Saved", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
               Close();
            }
            else
            {
               MessageBox.Show("Failed, Check all the fields.");
            }
         }
      }

      private void Delete_Button(object sender, RoutedEventArgs e)
      {
         string deleteQry = $"DELETE FROM bds.residentinfo WHERE id = '{ResidentId}'";
         var count = DBOperation.ExecuteNonQuery(deleteQry);
         if (count > 0)
         {
            Resident.DeleteRecord(ResidentId);
            MessageBox.Show("Deleted", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
         }
      }

      private void V_mobilenumber_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
      {
         v_mobilenumber.Text = Regex.Replace(v_mobilenumber.Text, "[^0-9]+", "");
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         OpenFileDialog openfiledialog = new OpenFileDialog
         {
            Title = "Open Image",
            Filter = "Image File|*.bmp; *.gif; *.jpg; *.jpeg; *.png;"
         };
         if (openfiledialog.ShowDialog() == true)
         {
            ImageSource imageSource = new BitmapImage(new Uri(openfiledialog.FileName));
            var bytes = File.ReadAllBytes(openfiledialog.FileName);
            ImageStr = DBOperation.ToHexString(bytes);
            v_profilepic.Source = imageSource;
         }
      }
   }
}
