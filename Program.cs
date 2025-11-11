using Courier_Service.Interface;
using Courier_Service.Model;
using Courier_Service.Services.Scheduling;
using System.Globalization;
namespace Courier_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //get input from user - base cost and number of packages
                Console.WriteLine("Please enter : \"<base_delivery_cost> <no_of_packages>\"");
                var firstLine = Console.ReadLine()?.Trim();

                //Validate if the user entered anything
                if (string.IsNullOrWhiteSpace(firstLine))
                {
                    Console.WriteLine("No input detected. Exiting.");
                    return;
                }

                //Split the first line input into separate value of base cost and number of packages
                var firstParts = firstLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                //Check if user entered both values correctly
                if (firstParts.Length < 2)
                {
                    Console.WriteLine("Invalid first line. Expect: <base_delivery_cost> <no_of_packages>");
                    return;
                }

                //Convert the entered strings to decimal using TryParse for safe parsing
                if (!decimal.TryParse(firstParts[0], out decimal baseCost))
                {
                    Console.WriteLine("Invalid base cost. Please enter a numeric value like 100 or 50.5");
                    return;
                }

                //Convert the entered strings to decimal using TryParse for safe parsing
                if (!int.TryParse(firstParts[1], out int noOfPackages))
                {
                    Console.WriteLine("Invalid package count. Please enter a whole number like 3 or 5.");
                    return;
                }


                // get each package's detail from user  
                Console.WriteLine("Please enter all Package Detail in given format: \"<pkg_id1> <pkg_weight1_in_kg> <distance1_in_km> <offer_code1>\"");
                var packages = new List<Package>();
                int readCount = 0;

                //read input lines until all packages are entered
                while (readCount < noOfPackages)
                {
                    var line = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        //Split line with space to get individual parts
                        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        //Validate that we got exactly 4 parts (id, weight, distance, offercode)
                        if (parts.Length == 4)
                        {
                            // Create and add the Package object to the list
                            var pkg = new Package
                            {
                                Id = parts[0],
                                Weight = decimal.Parse(parts[1], CultureInfo.InvariantCulture),
                                Distance = decimal.Parse(parts[2], CultureInfo.InvariantCulture),
                                OfferCode = parts.Length > 3 ? parts[3] : "NA"
                            };
                            packages.Add(pkg);
                            readCount++;
                        }
                    }
                }

                var lastLine = Console.ReadLine();
                int noOfVehicles = 1;
                decimal maxSpeed = 1;
                decimal maxCarriableWeight = 0;
                if (!string.IsNullOrWhiteSpace(lastLine))
                {
                    var lp = lastLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (lp.Length >= 3)
                    {
                        noOfVehicles = int.Parse(lp[0]);
                        maxSpeed = decimal.Parse(lp[1], CultureInfo.InvariantCulture);
                        maxCarriableWeight = decimal.Parse(lp[2], CultureInfo.InvariantCulture);
                    }
                }

                //Initialize the Offer Provider and Delivery Cost Calculator
                IOfferProvider offerProvider = new Services.Offers.OfferProvider();
                var costCalculator = new Services.Delivery.DeliveryCostCalculator(offerProvider);

                var SP = new List<ScheduledPackage>();

                ////Print the calculated discount amount and total cost for each package
                //foreach (var pkg in packages)
                //{
                //    var deliveryCost = costCalculator.CalculateDeliveryCost(baseCost, pkg);
                //    var discount = costCalculator.CalculateDiscount(baseCost, pkg);
                //    var totalCost = deliveryCost - discount;
                //    //Console.WriteLine($"{pkg.Id} {discount:F2} {totalCost:F2}"); // Format output to 2 decimal places                    
                //}

                //Schedule the packages for delivery
                if (noOfVehicles > 0 && maxSpeed > 0 && maxCarriableWeight > 0)
                {
                    var scheduler = new Scheduler(noOfVehicles, maxSpeed, maxCarriableWeight);
                    var schedPkgs = new List<ScheduledPackage>();

                    foreach (var pkg in packages)
                    {
                        var deliveryCost = costCalculator.CalculateDeliveryCost(baseCost, pkg);
                        var discount = costCalculator.CalculateDiscount(baseCost, pkg);
                        var total = deliveryCost - discount;
                        schedPkgs.Add(new ScheduledPackage(pkg.Id, pkg.Weight, pkg.Distance, discount, total, 0m));
                    }

                    var scheduledPackagesResult = scheduler.Schedule(schedPkgs);

                    Console.WriteLine("\nFinal Output (PackageID | Discount | TotalCost | ETA in Hours):");
                    foreach (var pkg in scheduledPackagesResult)
                    {
                        Console.WriteLine($"{pkg.Id.ToUpper()} {pkg.Discout:F2} {pkg.TotalCost:F2} {pkg.DeliveryTimeInHours:F2}");
                    }
                }
            }

            catch (Exception ex)
            {

                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}