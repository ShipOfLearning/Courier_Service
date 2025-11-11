using Courier_Service.Model;

namespace Courier_Service.Services.Scheduling
{
    public class Scheduler
    {
        private readonly int _vehicleCount;
        private readonly decimal _maxSpeed;
        private readonly decimal _maxCarryWeight;

        /// <summary>
        /// Constructor to initialize Scheduler with vehicle count, max speed and max carry weight
        /// </summary>
        /// <param name="vehicleCount"></param>
        /// <param name="maxSpeed"></param>
        /// <param name="maxCarryWeight"></param>
        public Scheduler(int vehicleCount, decimal maxSpeed, decimal maxCarryWeight)
        {
            _vehicleCount = vehicleCount;
            _maxSpeed = maxSpeed;
            _maxCarryWeight = maxCarryWeight;
        }


        public IList<ScheduledPackage> Schedule(List<ScheduledPackage> packages)
        {
            var vehicleAvailableTimes = new decimal[_vehicleCount];

            var queue = new Queue<ScheduledPackage>(packages);
            while (queue.Count > 0)
            {
                int vehicleIndex = IndexOfMin(vehicleAvailableTimes);
                decimal startTime = vehicleAvailableTimes[vehicleIndex];

                var trip = new List<ScheduledPackage>();
                decimal currentWeight = 0m;

                foreach (var pkg in queue.ToList())
                {
                    if (currentWeight + pkg.Weight <= _maxCarryWeight)
                    {
                        trip.Add(pkg);
                        currentWeight += pkg.Weight;
                    }
                    if (currentWeight >= _maxCarryWeight)
                        break;
                }

                if (trip.Count == 0)
                {
                    var tooHeavy = queue.Dequeue();
                    tooHeavy.DeliveryTimeInHours = -1;
                    continue;
                }

                decimal farthestDistance = trip.Max(p => p.Distance);
                decimal tripTime = farthestDistance / _maxSpeed;

                foreach (var pkg in trip)
                {
                    pkg.DeliveryTimeInHours = startTime + tripTime;
                    queue = new Queue<ScheduledPackage>(queue.Where(q => q.Id != pkg.Id));
                }

                vehicleAvailableTimes[vehicleIndex] = startTime + (2 * tripTime);
            }
            return packages;
        }

        private int IndexOfMin(decimal[] arr)
        {
            int idx = 0;
            decimal min = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                    idx = i;
                }
            }
            return idx;
        }
    }
}
