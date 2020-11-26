using System.Collections.Generic;
using System.Text;

namespace GarageManagementSystem
{
     public class Car : Vehicle
     {
          private const int c_MaxNumberOfDoors = 5;
          private const int c_MinNumberOfDoors = 2;
          private eColor m_Color;
          private int m_NumberOfDoors;

          public Car
          (eEnergyType i_EnergyType, string i_LicensePlate, short i_NumberOfWheels, float i_MaxAirPressure, float i_MaxEnergy, Fuel.eFuelType i_FuelType = Fuel.eFuelType.Octan96)
               : base(i_EnergyType, i_LicensePlate, i_NumberOfWheels, i_MaxAirPressure, i_MaxEnergy, Fuel.eFuelType.Octan96)
          {
               m_Color = eColor.Red;
               m_NumberOfDoors = 2;
          }

          public int NumberOfDoors
          {
               get
               {
                    return this.m_NumberOfDoors;
               }

               set
               {
                    Validation.IsInRange(c_MinNumberOfDoors, c_MaxNumberOfDoors, value);
                    this.m_NumberOfDoors = value;
               }
          }

          public eColor Color
          {
               get
               {
                    return this.m_Color;
               }

               set
               {
                    this.m_Color = value;
               }
          }

          public enum eColor
          {
               Red, White, Black, Silver
          }

          public override List<string> GetValuesToInsert()
          {
               List<string> values = base.GetValuesToInsert();
               values.Add("Color (Red = 1, White = 2 , Black = 3, Silver = 4)");
               values.Add("Number of doors");
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
Number of doors : {4}
Color : {5}
Energy type : {6}",
this.LicensePlate,
this.ModelName,
this.Wheels[0].Manufactor,
this.Wheels[0].AirPressure,
this.NumberOfDoors,
(eColor)this.Color,
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
