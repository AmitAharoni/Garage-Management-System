using System;
using System.Collections.Generic;
using System.Linq;

namespace GarageManagementSystem
{
     public class Fuel : Energy
     {
          private readonly eFuelType m_FuelType;

          public Fuel(eFuelType i_FuelType, float i_MaxEnergy, float i_FuelLevel = 0) : base(i_MaxEnergy, i_FuelLevel)
          {
               this.m_FuelType = i_FuelType;
          }

          public static List<eFuelType> GetFuelTypes()
          {
               List<eFuelType> fuelTypes = new List<eFuelType>();
               fuelTypes = Enum.GetValues(typeof(eFuelType)).Cast<eFuelType>().ToList();
               return fuelTypes;
          }

          public eFuelType FuelType
          {
               get
               {
                    return this.m_FuelType;
               }
          }

          public enum eFuelType
          {
               Soler = 1, Octan95, Octan96, Octan98
          }

          public void AddFuel(float i_LittersToAdd, eFuelType i_FuelType)
          {
               if(i_FuelType == (eFuelType)this.FuelType)
               {
                    if(this.EnergyLeft == this.MaxEnergy)
                    {
                         throw new ArgumentException("Invalid action , tank is full");
                    }

                    this.EnergyLeft = i_LittersToAdd;
               }
               else
               {
                    throw new ArgumentException("Invalid input, fuel type is wrong");
               }
          }
     }
}
