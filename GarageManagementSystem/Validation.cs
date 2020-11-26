using System;

namespace GarageManagementSystem
{
     public static class Validation
     {
          public static void IsLegalLicensePlate(string i_licensePlate)
          {
               Validation.IsNumbersString(i_licensePlate);
               if(i_licensePlate.Length != 7)
               {
                    throw new FormatException("Invalid input, length must be 7");
               }
          }

          public static void IsLegalName(string i_Name)
          {
               Validation.IsLettersString(i_Name);

               if(i_Name.Length < 3)
               {
                    throw new FormatException("Invalid input, length must be at least 3");
               }
          }

          public static void IsLegalPhoneNumber(string i_PhoneNumber)
          {
               Validation.IsNumbersString(i_PhoneNumber);

               if(i_PhoneNumber.Length < 7)
               {
                    throw new FormatException("Invalid input, length must be at least 7");
               }
          }

          public static bool IsNumbersString(string i_check)
          {
               bool isValid = true;

               for(int i = 0; i < i_check.Length; i++)
               {
                    if(char.IsDigit(i_check[i]) == false)
                    {
                         isValid = false;
                         break;
                    }
               }

               if(isValid == false)
               {
                    throw new FormatException("Invalid input, must insert only digits");
               }

               return isValid;
          }

          public static bool IsLettersString(string i_check)
          {
               bool isValid = true;

               for(int i = 0; i < i_check.Length; i++)
               {
                    if(char.IsLetter(i_check[i]) == false && i_check[i] != ' ')
                    {
                         isValid = false;
                         break;
                    }
               }

               if(isValid == false)
               {
                    throw new FormatException("Invalid input, must enter only letters");
               }

               return isValid;
          }

          public static void IsInRange(object i_Min, object i_Max, object i_ValueToCheck)
          {
               float minValue = Convert.ToSingle(i_Min);
               float maxValue = Convert.ToSingle(i_Max);
               float check = Convert.ToSingle(i_ValueToCheck);

               if(check < minValue || check > maxValue)
               {
                    throw new ValueOutOfRangeException(maxValue, minValue);
               }
          }

          public static void StringToBool(string i_Value, out bool o_BoolValue)
          {
               StringToInt(i_Value, out int value);
               string stringToCheck = null;
               
               if(value == 1)
               {
                    stringToCheck = "True";
               }
               else if(value == 0)
               {
                    stringToCheck = "False";
               }

               if(bool.TryParse(stringToCheck, out o_BoolValue) == false)
               {
                    throw new FormatException("Invalid input, must enter 1 or 0");
               }
          }

          public static void StringToFloat(string i_Value, out float o_FloatValue)
          {
               if(float.TryParse(i_Value, out o_FloatValue) == false)
               {
                    throw new FormatException("Invalid input, must enter only digits");
               }
          }

          public static void StringToInt(string i_Value, out int o_IntValue) 
          {
               if(int.TryParse(i_Value, out o_IntValue) == false)
               {
                    throw new FormatException("Invalid input, must enter only digits");
               }
          }
     }
}
