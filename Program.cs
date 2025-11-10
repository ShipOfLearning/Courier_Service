using Courier_Service.Model;
using System.Globalization;
namespace Courier_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("please enter : \"<base_delivery_cost> <no_of_packages>\"");
                var firstLine = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(firstLine))
                {
                    Console.WriteLine("No input detected. Exiting.");
                    return;
                }

                var firstParts = firstLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (firstParts.Length < 2)
                {
                    Console.WriteLine("Invalid first line. Expect: <base_delivery_cost> <no_of_packages>");
                    return;
                }

                decimal baseCost = decimal.Parse(firstParts[0]);
                int noOfPackages = int.Parse(firstParts[1]);

                Console.WriteLine("please enter all Package Detail in given format: \"<pkg_id1> <pkg_weight1_in_kg> <distance1_in_km> <offer_code1>\"");
                var packages = new List<Package>();
                int readCount = 0;
                while (readCount < noOfPackages)
                {
                    var line = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
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

            catch (Exception)
            {

                throw;
            }
        }
    }
}