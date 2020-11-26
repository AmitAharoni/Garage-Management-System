using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagementSystem
{
     public class Truck : Vehicle
     {
          private const float c_MaxCargoSpace = 400;
          private const float c_MinCargoSpace = 0;
          private bool m_MoveHazardMaterial;
          private float m_CargoSpace;

          public Truck(eEnergyType i_EnergyType, string i_LicensePlate, short i_NumberOfWheels, float i_MaxAirPressure, float i_MaxEnergy, Fuel.eFuelType i_FuelType = Fuel.eFuelType.Soler) 
               : base(i_EnergyType, i_LicensePlate, i_NumberOfWheels, i_MaxAirPressure, i_MaxEnergy, i_FuelType)
          {
               this.m_CargoSpace = 0;
               this.m_MoveHazardMaterial = false;
          }

          public bool MoveHazardMaterial
          {
               get
               {
                    return this.m_MoveHazardMaterial;
               }

               set
               {
                    this.m_MoveHazardMaterial = value;
               }
          }

          public float CargoSpace
          {
               get
               {
                    return this.m_CargoSpace;
               }

               set
               {
                    Validation.IsInRange(c_MinCargoSpace, c_MaxCargoSpace, value);
                    this.m_CargoSpace = value;
               }
          }

          public override List<string> GetValuesToInsert()
          {
               List<string> values = base.GetValuesToInsert();
               values.Add("Move Hazard Material (Yes = 1, No = 0)");
               values.Add("Cargo space");
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
Truck moves hazard material : {4}
Cargo space : {5}
Energy type : {6}
Litters left in tank : {7}",
this.LicensePlate,
this.ModelName,
this.Wheels[0].Manufactor,
this.Wheels[0].AirPressure,
this.MoveHazardMaterial == false ? "No" : "Yes",
this.CargoSpace,
this.EnergyType,
this.Energy.EnergyLeft);

               return details.ToString();
          }
     }
}
