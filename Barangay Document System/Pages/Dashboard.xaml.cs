using Barangay_Document_System.Certificates;
using Barangay_Document_System.DBManager;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Barangay_Document_System.Pages
{
   /// <summary>
   /// Interaction logic for Dashboard.xaml
   /// </summary>
   public partial class Dashboard : Window
   {
      public Dashboard()
      {
         InitializeComponent();
         TotalResident();
      }

      private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         DragMove();
      }

      private void LogoutButton(object sender, RoutedEventArgs e)
      {
         LoginPage loginPage = new LoginPage();
         loginPage.Show();
         Close();
      }

      private void Dashboard_Button(object sender, RoutedEventArgs e)
      {
         v_dboard_grid.Visibility = Visibility.Visible;
         v_resident_info_grid.Visibility = Visibility.Hidden;
         v_certificate_issuance_grid.Visibility = Visibility.Hidden;
         v_accounts_grid.Visibility = Visibility.Hidden;
         v_barangay_config_grid.Visibility = Visibility.Hidden;
         TotalResident();
      }

      public void Resident_Info_Button(object sender, RoutedEventArgs e)
      {
         v_resident_info_grid.Visibility = Visibility.Visible;
         v_dboard_grid.Visibility = Visibility.Hidden;
         v_certificate_issuance_grid.Visibility = Visibility.Hidden;
         v_accounts_grid.Visibility = Visibility.Hidden;
         v_barangay_config_grid.Visibility = Visibility.Hidden;
         v_data_grid.ItemsSource = Resident.ResidentCollection;
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         Insert insert = new Insert();
         insert.ResidentButton.Content = "Save Information";
         insert.ResidentButton.ToolTip = "Save Resident Information";
         insert.Show();
      }

      private void Edit_Button(object sender, RoutedEventArgs e)
      {
         var rowSelected = v_data_grid.SelectedCells[0].Item as Resident;
         Insert insert = new Insert();
         insert.v_lastname.Text = rowSelected.LastName;
         insert.v_firstname.Text = rowSelected.FirstName;
         insert.v_middlename.Text = rowSelected.MiddleName;
         insert.v_gender.Text = rowSelected.Gender;
         insert.v_birthdate.Text = rowSelected.BirthDate.ToString();
         insert.v_birthplace.Text = rowSelected.BirthPlace;
         insert.v_civilstatus.Text = rowSelected.CivilStatus;
         insert.v_voterstatus.Text = rowSelected.VoterStatus;
         insert.ResidentButton.Content = "Update Information";
         insert.ResidentButton.ToolTip = "Update Resident Information";
         Insert.ResidentId = (Guid)rowSelected.ResidentId;

         insert.v_delete_btn.Visibility = Visibility.Visible;
         insert.v_delete_btn.ToolTip = "Delete Information";
         insert.Show();
      }

      private void TotalResident()
      {
         v_total_residents.Text = $"Total resident registered : {Resident.ResidentCollection.Count}";
      }

      private void Residency_Button(object sender, RoutedEventArgs e)
      {
         var rowSelected = v_data_grid.SelectedCells[0].Item as Resident;
         Residency residency = new Residency();
         residency.v_name1.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         residency.v_name2.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         residency.v_name3.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         residency.v_civilstat.Text = rowSelected.CivilStatus;
         residency.v_day.Text = DateTime.Now.Day.ToString();
         residency.v_month.Text = $"{DateTime.Now.Month} {DateTime.Now.Year}";
         residency.Show();
      }

      private void BarangayClearance_Button(object sender, RoutedEventArgs e)
      {
         var rowSelected = v_data_grid.SelectedCells[0].Item as Resident;
         BarangayCert cert = new BarangayCert();
         cert.v_civilstat.Text = rowSelected.CivilStatus;
         cert.v_name1.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         cert.v_name3.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         cert.Show();
      }

      private void Indigency_Button(object sender, RoutedEventArgs e)
      {
         var rowSelected = v_data_grid.SelectedCells[0].Item as Resident;
         Indigent indgcy = new Indigent();
         indgcy.v_name1.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         indgcy.v_name2.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         indgcy.v_name3.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         indgcy.v_civilstat.Text = rowSelected.CivilStatus;
         indgcy.v_day.Text = DateTime.Now.Day.ToString();
         indgcy.v_month.Text = $"{DateTime.Now.Month} {DateTime.Now.Year}";
         indgcy.Show();
      }

      private void Search(object sender, KeyEventArgs e)
      {
         if(e.Key == Key.Enter)
         {
            string search = v_search.Text;
            v_data_grid.ItemsSource = Resident.ResidentCollection.Where(x => x.LastName.Contains(search)).ToList();
         }
      }
   }
}
