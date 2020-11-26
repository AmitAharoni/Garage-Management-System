using System;
using System.Text;

namespace GarageManagementSystem
{
     public class VehicleDetails
     {
          private Vehicle m_Vehicle;
          private OwnerInfo m_Owner;
          private eVehicleStatus m_Status;

          public VehicleDetails(Vehicle i_Vehicle = null, OwnerInfo i_Owner = null, eVehicleStatus i_Status = eVehicleStatus.InRepair)
          {
               this.m_Status = i_Status;
               this.m_Vehicle = i_Vehicle;
               this.m_Owner = i_Owner;
          }

          public eVehicleStatus Status
          {
               get
               {
                    return this.m_Status;
               }

               set
               {
                    this.m_Status = value;
               }
          }

          public Vehicle Vehicle
          {
               get
               {
                    return this.m_Vehicle;
               }

               set
               {
                    this.m_Vehicle = value;
               }
          }

          public OwnerInfo Owner
          {
               get
               {
                    return this.m_Owner;
               }

               set
               {
                    this.m_Owner = value;
               }
          }

          public enum eVehicleStatus
          {
               InRepair = 1, Repaired, Paid
          }

          public override string ToString()
          {
               string status;

               if(this.Status == eVehicleStatus.InRepair)
               {
                    status = "In repair";
               }
               else
               {
                    status = this.Status.ToString();
               }

               StringBuilder details = new StringBuilder(this.Owner.ToString());
               details.AppendFormat(string.Format(
@"
Vehicle : {0}
Vehicle status : {1}",
this.Vehicle.GetType().Name,
status));
               details.AppendFormat(Environment.NewLine);
               details.AppendFormat(this.Vehicle.ToString());
               return details.ToString();
          }
     }
}
