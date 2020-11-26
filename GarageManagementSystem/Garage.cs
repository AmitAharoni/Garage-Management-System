using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagementSystem
{
     public class Garage
     {
          private readonly Dictionary<string, VehicleDetails> m_Vehicles;

          public Garage()
          {
               m_Vehicles = new Dictionary<string, VehicleDetails>();
          }

          public Dictionary<string, VehicleDetails> Vehicles
          {
               get
               {
                    return this.m_Vehicles;
               }
          }

          public bool VehicleExist(string i_LicensePlate)
          {
               bool exist = false;

               if(i_LicensePlate != null)
               {
                    exist = m_Vehicles.ContainsKey(i_LicensePlate);
               }

               return exist;
          }

          public Vehicle GetVehicle(string i_LicensePlate)
          {
               VehicleDetails vehicle = GetVehicleDetails(i_LicensePlate);
               return vehicle.Vehicle;
          }

          public VehicleDetails GetVehicleDetails(string i_LicensePlate)
          {
               VehicleDetails vehicle;
               Vehicles.TryGetValue(i_LicensePlate, out vehicle);
               return vehicle;
          }

          public List<string> GetAllLicensePlates()
          {
                return new List<string>(this.Vehicles.Keys);
          }

          public void AddNewVehicleDataToGarage(VehicleDetails i_NewVehicle)
          {
               this.Vehicles.Add(i_NewVehicle.Vehicle.LicensePlate, i_NewVehicle);
          }

          public List<string> GetLicensePlatesByStatus(VehicleDetails.eVehicleStatus i_Status)
          {
               List<string> licensePlates = licensePlates = new List<string>();
               List<string> keysToCheck = new List<string>(this.Vehicles.Keys);
               VehicleDetails addToList;

               foreach(string key in keysToCheck)
               {
                    this.Vehicles.TryGetValue(key, out addToList);
                    if(addToList.Status == i_Status)
                    {
                         licensePlates.Add(key);
                    }
               }

               return licensePlates;
          }

          public void ChangeCarStatus(string i_key, VehicleDetails.eVehicleStatus i_Pick)
          {
               VehicleDetails vehicleToChange = this.GetVehicleDetails(i_key);
               vehicleToChange.Status = i_Pick;
          }
     }
}
