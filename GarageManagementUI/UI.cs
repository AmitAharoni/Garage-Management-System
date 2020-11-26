using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GarageManagementSystem;

namespace GarageManagementUI
{
     public class UI
     {
          private const string c_refuel = "Refeul";
          private const string c_recharge = "Recharge"; 
          private readonly Garage m_garage = new Garage();

          public void Run()
          {
               Console.WriteLine("Welcome to the Garage-Manager !");
               string input;
               int pick = 0;

               do
               {
                    try
                    {
                         printMenu();
                         input = Console.ReadLine();
                         Validation.StringToInt(input, out pick);
                         menuPick(pick);
                    }
                    catch(FormatException fe)
                    {
                         Console.WriteLine(fe.Message);
                    }
                    catch(ValueOutOfRangeException ve)
                    {
                         Console.WriteLine(string.Format("{0}, Please insert value between {1} - {2}", ve.ToString(), ve.MinValue, ve.MaxValue));
                    }

                    if(pick != 7 && pick != 2)
                    {
                         Thread.Sleep(2500);
                    }

                    Console.Clear();
               }while(pick != 8);
          }

          private void menuPick(int i_Pick) 
          {
               switch(i_Pick)
               {
                    case 1:
                         addNewVehicleToGarage();
                         break;
                    case 2:
                         showLicensePlates();
                         break;
                    case 3:
                         changeCarStatus();
                         break;
                    case 4:
                         addAirToWheels();
                         break;
                    case 5:
                         energyFill(c_refuel);
                         break;
                    case 6:
                         energyFill(c_recharge);
                         break;
                    case 7:
                         printVehicleDetails();
                         break;
                    case 8:
                         Console.WriteLine("Thank you for using Garage-Manager. BYE BYE");
                         break;
                    default:
                         throw new ValueOutOfRangeException(8, 1);
               }
          }

          private void addNewVehicleToGarage()
          {
               Console.Clear();
               getLicensePlate(out string licensePlate);

               if(m_garage.VehicleExist(licensePlate) == true)
               {
                    Console.WriteLine("This license plate is allready in the garage");
                    m_garage.ChangeCarStatus(licensePlate, VehicleDetails.eVehicleStatus.InRepair);
               }
               else
               {
                    VehicleDetails newVehicle = makeNewVehicle(licensePlate);
                    newVehicle.Owner = AddNewOwner();
                    insertAllVehicleData(newVehicle);
                    this.m_garage.AddNewVehicleDataToGarage(newVehicle);
                    Console.WriteLine("Vehicle was added!!");
               }
          }

          private void chooseVehicleType(out string io_VhicleType)
          {
               List<string> carTypes = VehicleMaker.GetCarTypes();
               Console.WriteLine("Please enter the number of the vehicle types you want :");
               printArray<string>(carTypes, true);
               io_VhicleType = Console.ReadLine();
               Validation.StringToInt(io_VhicleType, out int pick);
               Validation.IsInRange(1, carTypes.Count, pick);
          }

          private void getLicensePlate(out string io_LicensePlate)
          {
               io_LicensePlate = null;
               Console.Write("Please enter license plate number : ");
               bool isValid = false;

               do
               {
                    try
                    {
                         io_LicensePlate = Console.ReadLine();
                         Validation.IsLegalLicensePlate(io_LicensePlate);
                         isValid = true;
                    }
                    catch(FormatException fe)
                    {
                         Console.WriteLine(fe.Message);
                    }
                    catch(ValueOutOfRangeException ve)
                    {
                         Console.WriteLine(string.Format("{0}, Please insert value between {1} - {2}", ve.ToString(), ve.MinValue, ve.MaxValue));
                    }
               }while(isValid == false);
          }

          private VehicleDetails makeNewVehicle(string i_LicensePlate)
          {
               bool newVehicleWasMade = false;
               VehicleDetails newVehicle = new VehicleDetails();
               do
               {
                    try
                    {
                         chooseVehicleType(out string vehicleType);
                         newVehicle.Vehicle = VehicleMaker.MakeNewVehicle(i_LicensePlate, vehicleType);
                         newVehicleWasMade = true;
                    }
                    catch(FormatException fe)
                    {
                         Console.WriteLine(fe.Message);
                    }
                    catch(ValueOutOfRangeException ve)
                    {
                         Console.WriteLine(string.Format("{0}, Please insert value between {1} - {2}", ve.ToString(), ve.MinValue, ve.MaxValue));
                    }
               }while(newVehicleWasMade == false);

               return newVehicle;
          }

          private void insertAllVehicleData(VehicleDetails io_Vehicle)
          {
               bool isValidInput = false;
               short indexOfValueEntered = 0;
               List<string> vehicleValues = VehicleMaker.GetValuesOfSpecificVehicle(io_Vehicle.Vehicle);
               Console.WriteLine("Please enter the following details for the vehicle : ");

               foreach(string print in vehicleValues)
               {
                    do
                    {
                         try
                         {
                              Console.Write("{0} :  ", print);
                              string valueInput = Console.ReadLine();
                              VehicleMaker.ChangeValue(io_Vehicle.Vehicle, valueInput, indexOfValueEntered);
                              isValidInput = true;
                         }
                         catch(FormatException fe)
                         {
                              Console.WriteLine(fe.Message);
                         }
                         catch(ArgumentException ae)
                         {
                              Console.WriteLine(ae.Message);
                         }
                         catch(ValueOutOfRangeException ve)
                         {
                              Console.WriteLine(string.Format("{0}, Please insert value between {1} - {2}", ve.ToString(), ve.MinValue, ve.MaxValue));
                         }
                    }while(isValidInput == false);

                    isValidInput = false;
                    indexOfValueEntered++;
               }
          }

          private OwnerInfo AddNewOwner()
          {
               OwnerInfo owner = new OwnerInfo();
               string phone = "phone number";
               string name = "name";
               insertOwnerDetails(owner, name);
               insertOwnerDetails(owner, phone);
               return owner;
          }

          private void insertOwnerDetails(OwnerInfo i_Owner, string i_InsertValue)
          {
               bool legalInput = false;
               Console.Write("Please enter owner's {0} :  ", i_InsertValue);

               do
               {
                    try
                    {
                         string input = Console.ReadLine();
                         if(i_InsertValue == "name")
                         {
                              i_Owner.Name = input;
                         }
                         else
                         {
                              i_Owner.PhoneNumber = input;
                         }

                         legalInput = true;
                    }
                    catch(FormatException fe)
                    {
                         Console.WriteLine(fe.Message);
                    }
               }while(legalInput == false);
          }

          private void changeCarStatus()
          {
               Console.Clear();
               getLicensePlate(out string licensePlate);

               if(m_garage.VehicleExist(licensePlate) == false)
               {
                    Console.WriteLine("There is no Vehicle with this license plate.");
               }
               else
               {
                    Console.WriteLine("Which status would you like to change to ?");
                    VehicleDetails.eVehicleStatus status = statusPick();
                    m_garage.ChangeCarStatus(licensePlate, status);
                    Console.WriteLine("Status was changed !");
               }
          }

          private VehicleDetails.eVehicleStatus statusPick()
          {
               VehicleDetails.eVehicleStatus status = 0;
               bool isValid = false;
               string input = null;
               printStatusOptions();

               do
               {
                    try
                    {
                         input = Console.ReadLine();
                         Validation.StringToInt(input, out int statusInt);
                         isValid = Enum.IsDefined(typeof(VehicleDetails.eVehicleStatus), statusInt);
                         if(isValid == false)
                         {
                              throw new ValueOutOfRangeException(3, 1);
                         }
                    }
                    catch(FormatException fe)
                    {
                         Console.WriteLine(fe.Message);
                    }
                    catch(ValueOutOfRangeException ve)
                    {
                         Console.WriteLine(string.Format("{0}, Please insert value between {1} - {2}", ve.ToString(), ve.MinValue, ve.MaxValue));
                    }
               }while(isValid == false);

               isValid = Enum.TryParse<VehicleDetails.eVehicleStatus>(input, out status);
               return status;
          }

          private void showLicensePlates()
          {
               bool isValid = false;
               List<string> licensePlates = null;
               Console.WriteLine(
@"Please select : 
1. print all vehilces license plates
2. print vehicles license plates with specific status");

               do
               {
                    try
                    {
                         Validation.StringToInt(Console.ReadLine(), out int pick);
                         Validation.IsInRange(1, 2, pick);
                         if(pick == 2)
                         {
                              Console.WriteLine("Please select required status");
                              VehicleDetails.eVehicleStatus status = statusPick();
                              licensePlates = m_garage.GetLicensePlatesByStatus(status);
                         }
                         else
                         {
                              licensePlates = m_garage.GetAllLicensePlates();
                         }

                         isValid = true;
                    }
                    catch(FormatException fe)
                    {
                         Console.WriteLine(fe.Message);
                    }
                    catch(ValueOutOfRangeException ve)
                    {
                         Console.WriteLine(string.Format("{0}, Please insert value between {1} - {2}", ve.ToString(), ve.MinValue, ve.MaxValue));
                    }
               }
               while(isValid == false);

               Console.WriteLine("\n-----License Plates-----\n");
               printArray(licensePlates, true);
               Console.WriteLine("\n------------------------\n");
               Console.WriteLine("Press enter to continue");
               Console.ReadLine();
          }

          private void printArray<T>(List<T> i_ListToPrint, bool i_PrintWithIndex)
          {
               int index = 1;
               if(i_ListToPrint != null && i_ListToPrint.Any() == true)
               {
                    if(i_PrintWithIndex == true)
                    {
                         foreach(T key in i_ListToPrint)
                         {
                              Console.WriteLine("{0}. {1}", index++, key.ToString());
                         }
                    }
                    else
                    {
                         foreach(T key in i_ListToPrint)
                         {
                              Console.WriteLine(key.ToString());
                         }
                    }                 
               }
               else
               {
                    Console.WriteLine("No values");
               }
          }

          private void printStatusOptions()
          {
               Console.WriteLine(
@"---------------------------
1. in repair
2. repaired
3. paid for
---------------------------");
          }

          private void addAirToWheels()
          {
               Console.Clear();
               bool isValid = false;
               getLicensePlate(out string licensePlate);

               if(m_garage.VehicleExist(licensePlate) == false)
               {
                    Console.WriteLine("There is no vehicle with this license plate.");
               }
               else
               {
                    Console.WriteLine("Please enter air quantity to fill wheels");
                    do
                    {
                         try
                         {
                              Validation.StringToInt(Console.ReadLine(), out int airToAdd);

                              foreach(Wheel wheel in m_garage.GetVehicle(licensePlate).Wheels)
                              {
                                   wheel.AddAir(airToAdd);
                              }

                              isValid = true;
                              Console.WriteLine("Air filled");
                         }
                         catch(ArgumentException ae)
                         {
                              Console.WriteLine(ae.Message);
                              break;
                         }
                         catch(FormatException fe)
                         {
                              Console.WriteLine(fe.Message);
                         }
                         catch(ValueOutOfRangeException ve)
                         {
                              Console.WriteLine(string.Format("{0}, Please insert value between {1} - {2}", ve.ToString(), ve.MinValue, ve.MaxValue));
                         }
                    }while(isValid == false);
               }
          }

          private void energyFill(string i_FuelOrRecharge)
          {
               getLicensePlate(out string licensePlate);

               if(m_garage.VehicleExist(licensePlate) == false)
               {
                    Console.WriteLine("There is no vehicle with this license plate.");
               }
               else
               {
                    Vehicle vehicle = m_garage.GetVehicle(licensePlate);
                    if(vehicle.Energy is Fuel && i_FuelOrRecharge == c_refuel)
                    {
                         fuelVehicle(vehicle);
                    }
                    else if(vehicle.Energy is Electricty && i_FuelOrRecharge == c_recharge)
                    {
                         rechargeVehicle(vehicle);
                    }
                    else
                    {
                         Console.WriteLine("Vehicle energy type is {0} , and not {1}", vehicle.EnergyType, vehicle.EnergyType == Vehicle.eEnergyType.Electric ? Vehicle.eEnergyType.Fuel : Vehicle.eEnergyType.Electric);
                    }
               }
          }

          private void rechargeVehicle(Vehicle i_Vehicle)
          {
               bool isValid = false;
               int minutes = 0;
               Console.Write("Please enter number of minutes to charge battery : ");

               do
               {
                    try
                    {
                         Validation.StringToInt(Console.ReadLine(), out minutes);
                         (i_Vehicle.Energy as Electricty).RechargeBattery((float)minutes / 60f);
                         isValid = true;
                         Console.WriteLine("Vehicle was charged!");
                    }
                    catch(FormatException fe)
                    {
                         Console.WriteLine(fe.Message);
                    }
                    catch(ValueOutOfRangeException ve)
                    {
                         Console.WriteLine(string.Format("{0}, Please insert value between {1} - {2}", ve.ToString(), ve.MinValue, ve.MaxValue));
                    }
               }while(isValid == false);
          }

          private void fuelVehicle(Vehicle i_Vehicle)
          {
               bool isValid = false;
               Fuel.eFuelType fuelType;
               fuelType = getFuelType();

               do
               {
                    try
                    {
                         Console.WriteLine("Please insert amount of litters to add");
                         Validation.StringToFloat(Console.ReadLine(), out float fuel);
                         (i_Vehicle.Energy as Fuel).AddFuel(fuel, fuelType);
                         isValid = true;
                         Console.WriteLine("Fuel was added !");
                    }
                    catch(ArgumentException ae)
                    {
                         Console.WriteLine(ae.Message);
                         break;
                    }
                    catch(FormatException fe)
                    {
                         Console.WriteLine(fe.Message);
                    }
                    catch(ValueOutOfRangeException ve)
                    {
                         Console.WriteLine(string.Format("{0}, Please insert value between {1} - {2}", ve.ToString(), ve.MinValue, ve.MaxValue));
                    }
               }while(isValid == false);
          }

          private Fuel.eFuelType getFuelType()
          {
               Fuel.eFuelType fuelType;
               bool isValid = false;
               string input = null;
               Console.WriteLine("Please choose fuel type number : ");
               Console.WriteLine("--------------------------------");
               List<Fuel.eFuelType> fuelTypes = Fuel.GetFuelTypes();
               printArray<Fuel.eFuelType>(fuelTypes, true);

               while(isValid == false)
               {
                    input = Console.ReadLine();
                    Validation.StringToInt(input, out int fuelTypeInt);
                    isValid = Enum.IsDefined(typeof(Fuel.eFuelType), fuelTypeInt);

                    if(isValid == false)
                    {
                         Console.WriteLine("Wrong input please enter again");
                    }
               }

               isValid = Enum.TryParse<Fuel.eFuelType>(input, out fuelType);
               return fuelType;
          }

          private void printVehicleDetails()
          {
               Console.Clear();
               bool isValidInput = false;

               do
               {
                    try
                    {
                         getLicensePlate(out string licensePlate);
                         Validation.IsNumbersString(licensePlate);
                         if(m_garage.VehicleExist(licensePlate) == true)
                         {
                              VehicleDetails vehicleToPrint = m_garage.GetVehicleDetails(licensePlate);
                              Console.WriteLine(vehicleToPrint.ToString());
                              Console.WriteLine("Press enter to continue");
                              Console.ReadLine();
                         }
                         else
                         {
                              Console.WriteLine("There is no vehicle with that license plate");
                         }

                         isValidInput = true;
                    }
                    catch(FormatException fe)
                    {
                         Console.WriteLine(fe.Message);
                    }
               }while(isValidInput == false);
          }

          private void printMenu()
          {
               Console.WriteLine(
@"Please select :
1 : Add new vehicle to the garage.   
2 : Show license plates.
3 : Change vehicle status.
4 : Add air to wheels of specific vehicle.
5 : Refuel specific vehicle.
6 : Recharge specific vehicle.
7 : Show vehicle details.
8 : Quit the garage.");
          }
     }
}
