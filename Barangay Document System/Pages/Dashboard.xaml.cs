using Barangay_Document_System.Certificates;
using Barangay_Document_System.DBManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
         insert.v_homeaddress.Text = rowSelected.HouseAddress;
         insert.v_mobilenumber.Text = rowSelected.MobileNumber;

         if (!string.IsNullOrEmpty(rowSelected.Image))
         {
            Stream StreamObj = new MemoryStream(DBOperation.ToHexBytes(rowSelected.Image));
            BitmapImage BitObj = new BitmapImage();
            BitObj.BeginInit();
            BitObj.StreamSource = StreamObj;
            BitObj.EndInit();
            insert.v_profilepic.Source = BitObj;
         }

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
         residency.v_age.Text = ComputeAge((DateTime)rowSelected.BirthDate).ToString();
         residency.Show();
      }

      private void BarangayClearance_Button(object sender, RoutedEventArgs e)
      {
         var rowSelected = v_data_grid.SelectedCells[0].Item as Resident;
         BarangayCert cert = new BarangayCert();
         cert.v_civilstat.Text = rowSelected.CivilStatus;
         cert.v_name1.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         cert.v_name3.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         cert.v_age.Text = ComputeAge((DateTime)rowSelected.BirthDate).ToString();
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
         indgcy.v_age.Text = ComputeAge((DateTime)rowSelected.BirthDate).ToString();
         indgcy.Show();
      }

      private void Search(object sender, KeyEventArgs e)
      {
         if(e.Key == Key.Enter)
         {
            var search = v_search.Text.Split(' ');
            if(search.Length > 1)
            {
               v_data_grid.ItemsSource = Resident.ResidentCollection.Where(x => x.LastName.Contains(search.FirstOrDefault())
            && x.FirstName.Contains(search.LastOrDefault())).ToList();
            }
            else
            {
               v_data_grid.ItemsSource = Resident.ResidentCollection.Where(x => x.LastName.Contains(search.FirstOrDefault())).ToList();
            }
         }
      }

      private void Farmer_Button(object sender, RoutedEventArgs e)
      {
         var rowSelected = v_data_grid.SelectedCells[0].Item as Resident;
         FarmerCert farmer = new FarmerCert();
         farmer.v_name1.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         farmer.v_name2.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         farmer.v_civilstat.Text = rowSelected.CivilStatus;
         farmer.v_day.Text = DateTime.Now.Day.ToString();
         farmer.v_month.Text = $"{DateTime.Now.Month} {DateTime.Now.Year}";
         farmer.v_age.Text = ComputeAge((DateTime)rowSelected.BirthDate).ToString();
         farmer.Show();
      }

      private void LiveIn_Button(object sender, RoutedEventArgs e)
      {
         var rowSelected = v_data_grid.SelectedCells[0].Item as Resident;
         LiveIn li = new LiveIn();
         li.v_name1.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         li.v_name2.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         li.v_day.Text = DateTime.Now.Day.ToString();
         li.v_month.Text = $"{DateTime.Now.Month} {DateTime.Now.Year}";
         li.v_age.Text = ComputeAge((DateTime)rowSelected.BirthDate).ToString();
         li.Show();
      }

      private void GoodMoral_Button(object sender, RoutedEventArgs e)
      {
         var rowSelected = v_data_grid.SelectedCells[0].Item as Resident;
         GoodMoral gm = new GoodMoral();
         gm.v_name1.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         gm.v_name2.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         gm.v_name3.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         gm.v_age.Text = ComputeAge((DateTime)rowSelected.BirthDate).ToString();
         gm.Show();
      }

      private int ComputeAge(DateTime birthdate)
      {
         return DateTime.Now.Year - birthdate.Year;
      }

      private DataGridColumnHeader GetColumnHeaderFromColumn(DataGridColumn column)
      {
         // dataGrid is the name of your DataGrid. In this case Name="dataGrid"
         List<DataGridColumnHeader> columnHeaders = GetVisualChildCollection<DataGridColumnHeader>(v_data_grid);
         foreach (DataGridColumnHeader columnHeader in columnHeaders)
         {
            if (columnHeader.Column == column)
            {
               return columnHeader;
            }
         }
         return null;
      }

      public List<T> GetVisualChildCollection<T>(object parent) where T : Visual
      {
         List<T> visualCollection = new List<T>();
         GetVisualChildCollection(parent as DependencyObject, visualCollection);
         return visualCollection;
      }

      private void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection) where T : Visual
      {
         int count = VisualTreeHelper.GetChildrenCount(parent);
         for (int i = 0; i < count; i++)
         {
            DependencyObject child = VisualTreeHelper.GetChild(parent, i);
            if (child is T)
            {
               visualCollection.Add(child as T);
            }
            else if (child != null)
            {
               GetVisualChildCollection(child, visualCollection);
            }
         }
      }

      private void Cert_Button(object sender, RoutedEventArgs e)
      {
         var rowSelected = v_data_grid.SelectedCells[0].Item as Resident;
         Certification c = new Certification();
         c.v_name1.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         c.v_day.Text = DateTime.Now.Day.ToString();
         c.v_month.Text = $"{DateTime.Now.Month} {DateTime.Now.Year}";
         c.Show();
      }

      private void CuttingTrees_Button(object sender, RoutedEventArgs e)
      {
         var rowSelected = v_data_grid.SelectedCells[0].Item as Resident;
         CuttingTrees ct = new CuttingTrees();
         ct.v_name1.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         ct.v_day.Text = DateTime.Now.Day.ToString();
         ct.v_month.Text = $"{DateTime.Now.Month} {DateTime.Now.Year}";
         ct.Show();
      }

      private void JobSeeker_Button(object sender, RoutedEventArgs e)
      {
         var rowSelected = v_data_grid.SelectedCells[0].Item as Resident;
         JobSeekers js = new JobSeekers();
         js.v_name1.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         js.v_day.Text = DateTime.Now.Day.ToString();
         js.v_month.Text = $"{DateTime.Now.Month} {DateTime.Now.Year}";
         js.Show();
      }

      private void Pigs_Button(object sender, RoutedEventArgs e)
      {
         var rowSelected = v_data_grid.SelectedCells[0].Item as Resident;
         Pigs li = new Pigs();
         li.v_name1.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         li.v_name2.Text = $"{rowSelected.FirstName} {rowSelected.MiddleName} {rowSelected.LastName}";
         li.Show();
      }
   }
}
