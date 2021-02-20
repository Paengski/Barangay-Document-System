using System;
using System.Windows;
using System.Windows.Input;

namespace Barangay_Document_System.DBManager
{
   /// <summary>
   /// Interaction logic for Insert.xaml
   /// </summary>
   public partial class Insert : Window
   {
      public static Guid ResidentId;
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
         string isVoter = v_voterstatus.Text;
         var btnStr = ResidentButton.Content as string;
         if (btnStr.Contains("Update"))
         {
            var updateInfo = $"UPDATE bds.residentinfo SET lastname = '{lastname}'," +
               $"firstname = '{firstname}', middlename = '{middlename}', gender = '{gender}'," +
               $"birthdate = '{birthdate}',birthplace = '{birthplace}',civilstatus = '{civil}',voterstatus = '{isVoter}' " +
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
                  VoterStatus = isVoter
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
               $"(id,firstname,middlename,lastname,gender,birthdate,birthplace,civilstatus,voterstatus) VALUES " +
               $"('{guid}','{firstname}','{middlename}','{lastname}','{gender}'" +
               $",'{birthdate}','{birthplace}','{civil}','{isVoter}')";

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
                  VoterStatus = isVoter
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
   }
}
