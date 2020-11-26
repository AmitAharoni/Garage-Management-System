using System;

namespace GarageManagementSystem
{
     public class Electricty : Energy
     {
          public Electricty(float i_MaxEnergy, float i_DriveTimeLeft = 0) : base(i_MaxEnergy, i_DriveTimeLeft)
          {
          }

          public void RechargeBattery(float i_NumOfHours)
          {
               if(this.EnergyLeft != this.MaxEnergy)
               {
                    this.EnergyLeft = i_NumOfHours * 60;
               }
               else
               {
                    throw new ArgumentException("Invalid action, battery is fully charged");
               }
          }
     }
}
