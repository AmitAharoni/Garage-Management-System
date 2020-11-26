using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagementSystem
{
     public class MotorCycle : Vehicle
     {
          private const int c_MaxEngineCapacity = 150;
          private const int c_MinEngineCapacity = 0;
          private eLicenseType m_LicenseType;
          private int m_EngineCpacity;

          public MotorCycle(eEnergyType i_EnergyType, string i_LicensePlate, short i_NumberOfWheels, float i_MaxAirPressure, float i_MaxEnergy, Fuel.eFuelType i_FuelType = Fuel.eFuelType.Octan95) 
               : base(i_EnergyType, i_LicensePlate, i_NumberOfWheels, i_MaxAirPressure, i_MaxEnergy)
          {
               this.EngineCapacity = 0;
               this.LicenseType = eLicenseType.A;
          }

          public int EngineCapacity
          {
               get
               {
                    return this.m_EngineCpacity;
               }

               set
               {
                    Validation.IsInRange(c_MinEngineCapacity, c_MaxEngineCapacity, value);
                    this.m_EngineCpacity = value;
               }
          }

          public eLicenseType LicenseType
          {
               get
               {
                    return this.m_LicenseType;
               }

               set
               {
                    this.m_LicenseType = value;
               }
          }

          public enum eLicenseType
          {
               A = 1, A1, AA, B
          }

          public override List<string> GetValuesToInsert()
          {
               List<string> values = base.GetValuesToInsert();
               values.Add("License type (A = 1, A1 = 2, AA = 3, B = 4)");
               values.Add("Engine capacity");
               return values;
          }

          public override string ToString()
          {
               StringBuilder details = new StringBuilder();
               details.AppendFormat(
@"License plate number : {0}
Model : {1}
Wheels manufactor : {2}
Wheels air pressure : {3}
License type : {4}
Engine capacity : {5}
Energy type : {6}",
this.LicensePlate,
this.ModelName,
this.Wheels[0].Manufactor,
this.Wheels[0].AirPressure,
this.LicenseType,
this.EngineCapacity,
this.EnergyType);

               if(this.EnergyType == Vehicle.eEnergyType.Electric)
               {
                    details.AppendFormat(
@"
Minutes left in battery : {0}",
this.Energy.EnergyLeft);
               }
               else
               {
                    details.AppendFormat(
@"
Litters left in tank : {0}",
this.Energy.EnergyLeft);
               }

               return details.ToString();
          }
     }
}
