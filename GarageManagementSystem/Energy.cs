using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagementSystem
{
     public class Energy
     {
          private readonly float m_MaxEnergy;
          private float m_EnergyLeft;

          public Energy(float i_MaxEnergy = 200, float i_EnergyLeft = 0)
          {
               m_MaxEnergy = i_MaxEnergy;
               m_EnergyLeft = i_EnergyLeft;
          }

          public float EnergyLeft
          {
               get
               {
                    return this.m_EnergyLeft;
               }

               set
               {
                    Validation.IsInRange(0, this.MaxEnergy - m_EnergyLeft, value);
                    this.m_EnergyLeft = this.m_EnergyLeft + value;
               }
          }

          public float MaxEnergy
          {
               get
               {
                    return this.m_MaxEnergy;
               }
          }
     }
}
