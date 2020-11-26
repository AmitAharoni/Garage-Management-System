using System;
using System.Collections.Generic;

namespace GarageManagementSystem
{
     public abstract class Vehicle
     {
          private readonly List<Wheel> m_Wheels;
          private eEnergyType m_EnergyType;
          private string m_ModelName;
          private string m_LicensePlate;
          private Energy m_Energy;  

          public Vehicle(eEnergyType i_EnergyType, string i_LicensePlate, short i_NumberOfWheels, float i_MaxAirPressure, float i_MaxEnergy = 100, Fuel.eFuelType i_FuelType = Fuel.eFuelType.Octan95)
          {
               this.LicensePlate = i_LicensePlate;
               this.ModelName = "None";
               EnergyType = i_EnergyType;

               if(i_EnergyType.Equals(eEnergyType.Fuel))
               {
                    this.Energy = new Fuel(i_FuelType, i_MaxEnergy);
               }
               else
               {
                    this.Energy = new Electricty(i_MaxEnergy);
               }

               m_Wheels = new List<Wheel>(i_NumberOfWheels);     
               for(short i = 0; i < i_NumberOfWheels; i++)
               {
                    m_Wheels.Add(new Wheel(i_MaxAirPressure));
               }
          }

          public Energy Energy
          {
               get
               {
                    return this.m_Energy;
               }

               set
               {
                    this.m_Energy = value;
               }
          }

          public eEnergyType EnergyType
          {
               get
               {
                    return this.m_EnergyType;
               }

               set
               {
                    this.m_EnergyType = value;
               }
          }

          public int NumberOfWheels
          {
               get
               {
                    return this.m_Wheels.Count;
               }

               set
               {
                    this.m_Wheels.Capacity = value;
               }
          }

          public List<Wheel> Wheels
          {
               get
               {
                    return this.m_Wheels;
               }
          }

          public string ModelName
          {
               get
               {
                    return this.m_ModelName;
               }

               set
               {
                    if(value != null && value.Length > 1)
                    {
                         this.m_ModelName = value;
                    }
                    else
                    {
                         throw new FormatException("Invalid input, must enter at least one character");
                    }
               }
          }

          public string LicensePlate
          {
               get
               {
                    return m_LicensePlate;
               }

               set
               {
                    Validation.IsNumbersString(value);
                    m_LicensePlate = value;
               }
          }

          public virtual List<string> GetValuesToInsert()
          {
               List<string> values = new List<string>();
               values.Add("Model");
               if(this.Energy is Fuel)
               {
                    values.Add("Litters in tank");
               }
               else
               {
                    values.Add("Minutes left in battery");
               }

               values.Add("Wheels manufactor");
               values.Add("Wheels air pressure");
               return values;
          }

          public enum eEnergyType
          {
               Fuel = 1, Electric
          }

          public abstract string ToString();
     }
}
