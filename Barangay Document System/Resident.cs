using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Barangay_Document_System
{
   public class Resident
   {
      public Guid? ResidentId { get; set; }
      public string LastName { get; set; }
      public string FirstName { get; set; }
      public string MiddleName { get; set; }
      public string Gender { get; set; }
      public DateTime? BirthDate { get; set; }
      public string BirthPlace { get; set; }
      public string CivilStatus { get; set; }
      public string HouseAddress { get; set; }
      public string MobileNumber { get; set; }
      public string Image { get; set; }

      private static readonly Dictionary<Guid, Resident> _resident = new Dictionary<Guid, Resident>();
      public static List<Resident> ResidentCollection
      {
         get
         {
            if (_resident == null || _resident.Count == 0)
            {
               string selectInfo = "SELECT id,firstname,middlename,lastname,gender,birthdate,birthplace,civilstatus,houseaddress,mobilenumber,image " +
                  "FROM bds.residentinfo";

               var dt = DBOperation.ExecuteToDataTable(selectInfo);
               if (dt != null)
               {
                  foreach (DataRow dr in dt.Rows)
                  {
                     Resident resident = new Resident
                     {
                        ResidentId = (Guid)dr["id"],
                        LastName = dr["lastname"] as string,
                        FirstName = dr["firstname"] as string,
                        MiddleName = dr["middlename"] as string,
                        Gender = dr["gender"] as string,
                        BirthDate = (DateTime)dr["birthdate"],
                        BirthPlace = dr["birthplace"] as string,
                        CivilStatus = dr["civilstatus"] as string,
                        HouseAddress = dr["houseaddress"] as string,
                        MobileNumber = dr["mobilenumber"] as string,
                        Image = dr["image"] as string
                     };
                     _resident[(Guid)resident.ResidentId] = resident;
                  }
               }
               return _resident.Select(x => x.Value).ToList();
            }
            return _resident.Select(x => x.Value).ToList();
         }
      }

      public static void AddResident(Resident resident)
      {
         _resident[(Guid)resident.ResidentId] = resident;
      }

      public static void UpdateResident(Guid id, Resident resident)
      {
         _resident[id] = resident;
      }

      public static void DeleteRecord(Guid id)
      {
         _resident.Remove(id);
      }
   }

   public class ResidentLog
   {
      public int ID { get; set; }
      public string Fullname { get; set; }
      public string CertificateType { get; set; }
      public DateTime? DateIssued { get; set; }

      private static readonly Dictionary<int, ResidentLog> _residentlog = new Dictionary<int, ResidentLog>();
      public static List<ResidentLog> ResidentLogCollection
      {
         get
         {
            if (_residentlog == null || _residentlog.Count == 0)
            {
               string selectInfo = "SELECT id,fullname,certtype,dateissued FROM bds.residentlog";
               var dt = DBOperation.ExecuteToDataTable(selectInfo);
               if (dt != null)
               {
                  foreach (DataRow dr in dt.Rows)
                  {
                     ResidentLog resident = new ResidentLog
                     {
                        ID = (int)dr["id"],
                        Fullname = dr["fullname"] as string,
                        CertificateType = dr["certtype"] as string,
                        DateIssued = (DateTime)dr["dateissued"],
                     };
                     _residentlog[(int)dr["id"]] = resident;
                  }
               }
               return _residentlog.Select(x => x.Value).ToList();
            }
            return _residentlog.Select(x => x.Value).ToList();
         }
      }

      public static void DeleteRecord(int id)
      {
         _residentlog.Remove(id);
      }
   }
   public class AdminAccount
   {
      public int ID { get; set; }
      public string Username { get; set; }
      public string Password { get; set; }
      public string Hint { get; set; }

      private static readonly Dictionary<int, AdminAccount> _adminAcc = new Dictionary<int, AdminAccount>();
      public static List<AdminAccount> AdminAccounts
      {
         get
         {
            if (_adminAcc == null || _adminAcc.Count == 0)
            {
               string selectInfo = "SELECT * FROM app_user";
               var dt = DBOperation.ExecuteToDataTable(selectInfo);
               if (dt != null)
               {
                  foreach (DataRow dr in dt.Rows)
                  {
                     AdminAccount resident = new AdminAccount
                     {
                        ID = (int)dr["id"],
                        Username = dr["username"] as string,
                        Password = dr["userpassword"] as string,
                        Hint = dr["hint"] as string,
                     };
                     _adminAcc[(int)dr["id"]] = resident;
                  }
               }
               return _adminAcc.Select(x => x.Value).ToList();
            }
            return _adminAcc.Select(x => x.Value).ToList();
         }
      }

      public static void DeleteRecord(int id)
      {
         _adminAcc.Remove(id);
      }
   }
}
