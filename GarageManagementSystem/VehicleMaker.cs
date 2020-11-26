using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GarageManagementSystem
{
     public class VehicleMaker
     {
          private static readonly List<string> m_ValuesByIndex = new List<string>(); 

          public enum eCarTypes
          {
               ElectricMotorCycle = 1,
               FuelMotorCycle,
               ElectricCar,
               FuelCar,
               Truck
          }

          public static List<string> GetCarTypes()
          {
               List<string> carTypesToPrint = new List<string>();
               List<eCarTypes> carTypes = new List<eCarTypes>();
               carTypes = Enum.GetValues(typeof(eCarTypes)).Cast<eCarTypes>().ToList();

               foreach(eCarTypes carType in carTypes)
               {
                    carTypesToPrint.Add(enumToString(carType));
               }

               return carTypesToPrint;
          }

          private static string enumToString(Enum eType)
          {
               StringBuilder newWord = new StringBuilder();
               string type = eType.ToString();

               for(int i = 0; i < type.Length; i++)
               {
                    if(i != 0)
                    {
                         if(char.IsUpper(type[i]))
                         {
                              newWord.Append(" ");
                         }
                    }

                    newWord.Append(type[i]);
               }

               return newWord.ToString();
          }

          public static Vehicle MakeNewVehicle(string i_LicensePlate, string i_CarType)
          {
               Vehicle vehicle = null;
               Enum.TryParse<eCarTypes>(i_CarType, out eCarTypes carType);

               switch(carType)
               {
                    case eCarTypes.ElectricMotorCycle:
                         vehicle = new MotorCycle(Vehicle.eEnergyType.Electric, i_LicensePlate, 2, 30, 80);
                         break;
                    case eCarTypes.FuelMotorCycle:
                         vehicle = new MotorCycle(Vehicle.eEnergyType.Fuel, i_LicensePlate, 2, 30, 7, Fuel.eFuelType.Octan95);
                         break;
                    case eCarTypes.ElectricCar:
                         vehicle = new Car(Vehicle.eEnergyType.Electric, i_LicensePlate, 4, 32, 130);
                         break;
                    case eCarTypes.FuelCar:
                         vehicle = new Car(Vehicle.eEnergyType.Fuel, i_LicensePlate, 4, 32, 60, Fuel.eFuelType.Octan96);
                         break;
                    case eCarTypes.Truck:
                         vehicle = new Truck(Vehicle.eEnergyType.Fuel, i_LicensePlate, 16, 28, 120, Fuel.eFuelType.Soler);
                         break;
                    default:
                         break;
               }

               return vehicle;
          }

          public static void ChangeValue(Vehicle i_Vehicle, string i_ValueInput, short i_IndexOfValueEntered)
          {
               switch(i_IndexOfValueEntered)
               {
                    case 0:
                         i_Vehicle.ModelName = i_ValueInput;
                         break;
                    case 1:
                         Validation.StringToFloat(i_ValueInput, out float floatValue1);
                         i_Vehicle.Energy.EnergyLeft = floatValue1;
                         break;
                    case 2:
                         foreach(Wheel wheel in i_Vehicle.Wheels)
                         {
                              wheel.Manufactor = i_ValueInput;
                         }

                         break;
                    case 3:
                         Validation.StringToFloat(i_ValueInput, out float floatValue2);
                         foreach(Wheel wheel in i_Vehicle.Wheels)
                         { 
                              wheel.AirPressure = floatValue2;
                         }

                         break;
                    default:
                         if(i_Vehicle is Truck)
                         {
                              ChanegeTruckValues(i_Vehicle, i_ValueInput, i_IndexOfValueEntered);
                         }
                         else if(i_Vehicle is Car)
                         {
                              ChangeCarValues(i_Vehicle, i_ValueInput, i_IndexOfValueEntered);
                         }
                         else
                         {
                              ChangeMotorValues(i_Vehicle, i_ValueInput, i_IndexOfValueEntered);
                         }

                         break;
               }
          }

          public static void ChangeMotorValues(Vehicle i_Vehicle, string i_ValueInput, short i_IndexOfValueEntered)
          {
               MotorCycle.eLicenseType licenseType;

               switch(i_IndexOfValueEntered)
               {
                    case 4:
                         bool isValid = Enum.TryParse<MotorCycle.eLicenseType>(i_ValueInput, out licenseType);
                         Validation.IsInRange(1, 4, (float)licenseType);
                         if(isValid)
                         {
                              (i_Vehicle as MotorCycle).LicenseType = licenseType;
                         }
                         else
                         {
                              throw new ArgumentException("Invalid input, this is not a license type (A, AA, A1, B)");
                         }

                         break;
                    case 5:
                         Validation.StringToInt(i_ValueInput, out int value);
                         (i_Vehicle as MotorCycle).EngineCapacity = value;
                         break;
                    default: 
                         break;
               }
          }

          public static void ChangeCarValues(Vehicle i_Vehicle, string i_ValueInput, short i_IndexOfValueEntered)
          {
               Car.eColor color;

               switch(i_IndexOfValueEntered)
               {
                    case 4:
                         bool isValid = Enum.TryParse<Car.eColor>(i_ValueInput, out color);
                         Validation.IsInRange(1, 4, (float)color);
                         if(isValid)
                         {
                              (i_Vehicle as Car).Color = color;
                         }
                         else
                         {
                              throw new ArgumentException("Invalid input, not a valid color");
                         }

                         break;
                    case 5:
                         Validation.StringToInt(i_ValueInput, out int value);
                         (i_Vehicle as Car).NumberOfDoors = value;
                         break;
                    default:
                         break;
               }
          }

          public static void ChanegeTruckValues(Vehicle i_Vehicle, string i_ValueInput, short i_IndexOfValueEntered)
          {
               switch(i_IndexOfValueEntered)
               {
                    case 4:
                         Validation.StringToBool(i_ValueInput, out bool i_BoolValue);
                         (i_Vehicle as Truck).MoveHazardMaterial = i_BoolValue;
                         break;
                    case 5:
                         Validation.StringToFloat(i_ValueInput, out float value);
                         (i_Vehicle as Truck).CargoSpace = value;
                         break;
                    default:
                         break;
               }
          }

          public static List<string> GetValuesOfSpecificVehicle(Vehicle io_Vehicle)
          {
               List<string> values = io_Vehicle.GetValuesToInsert();
               return values;
          }
     }
}
