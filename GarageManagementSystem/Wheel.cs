using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagementSystem
{
     public class Wheel
     {
          private readonly float r_MaxAirPressure;
          private string m_Manufactor;
          private float m_AirPressure;

          public Wheel(float i_MaxPressure, string i_Manufactor = "None")
          {
               r_MaxAirPressure = i_MaxPressure;
               m_Manufactor = i_Manufactor;
               AirPressure = 0;
          }

          public string Manufactor
          {
               get
               {
                    return this.m_Manufactor;
               }

               set
               {
                    if(value != null && value.Length > 1)
                    {
                         Validation.IsLettersString(value);
                         this.m_Manufactor = value;
                    }
                    else
                    {
                         throw new FormatException("Invalid input, must insert at least one character");
                    }
               }
          }

          public float MaxPressure
          {
               get
               {
                    return r_MaxAirPressure;
               }
          }

          public float AirPressure
          {
               get
               {
                    return m_AirPressure;
               }

               set
               {
                    Validation.IsInRange(0, r_MaxAirPressure - this.AirPressure, value);
                    this.m_AirPressure += value;
               }
          }

          public void AddAir(float i_AirToAdd)
          {
               if(this.AirPressure != this.MaxPressure)
               {
                    this.AirPressure = i_AirToAdd;
               }
               else
               {
                    throw new ArgumentException("Invalid input, air pressure allready at maximum");
               }
          }
     }
}
