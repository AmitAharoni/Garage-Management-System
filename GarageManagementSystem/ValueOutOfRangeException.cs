using System;

namespace GarageManagementSystem
{
     public class ValueOutOfRangeException : Exception
     {
          private readonly float m_MaxValue;
          private readonly float m_MinValue;

          public ValueOutOfRangeException(float i_MaxValue, float i_MinValue) : base()
          {
               this.m_MinValue = i_MinValue;
               this.m_MaxValue = i_MaxValue;
          }

          public float MaxValue
          {
               get
               {
                    return this.m_MaxValue;
               }
          }

          public float MinValue
          {
               get
               {
                    return this.m_MinValue;
               }
          }

          public override string ToString()
          {
               return "Value out of Range";
          }
     }
}
