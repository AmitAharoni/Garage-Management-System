using System;

namespace GarageManagementSystem
{
     public class OwnerInfo
     {
          private string m_Name;
          private string m_PhoneNumber;

          public string Name
          {
               get
               {
                    return this.m_Name;
               }

               set
               {
                    if(value != null)
                    {
                         Validation.IsLegalName(value);
                         this.m_Name = value;
                    }
                    else
                    {
                         throw new FormatException("Invalid input, must insert at least one character");
                    }
               }
          }

          public string PhoneNumber
          {
               get
               {
                    return this.m_PhoneNumber;
               }

               set
               {
                    if(value != null)
                    {
                         Validation.IsLegalPhoneNumber(value);
                         this.m_PhoneNumber = value;
                    }
                    else
                    {
                         throw new FormatException("Invalid input, must insert at least one character");
                    }
               }
          }

          public override string ToString()
          {
               return string.Format(
@"Owner's name : {0}",
this.Name);
          }
     }
}
