namespace codingame.defibrillators
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Solution
    {
        static void Main(string[] args)
        {
            string userLongitude = Console.ReadLine();
            string userLatitude = Console.ReadLine();
            int N = int.Parse(Console.ReadLine());
            var debrilillatorLines = new List<string>();
            for (int i = 0; i < N; i++)
            {
                debrilillatorLines.Add(Console.ReadLine().Trim());
            }

            var debrilillatorFinder = new DefibrillatorFinder(userLongitude, userLatitude, debrilillatorLines);

            Console.WriteLine(debrilillatorFinder.Find().Name);
        }

        public static double ParseDouble(string userLongitude)
        {
            return double.Parse(userLongitude, new CultureInfo("fr-FR"));
        }
    }

    public class DefibrillatorFinder
    {
        private readonly double _userLongitude;
        private readonly double _userLatitude;
        private readonly IEnumerable<Defibrillator> _debrilillators;

        public DefibrillatorFinder(
            string userLongitude,
            string userLatitude,
            IEnumerable<string> debrilillatorLines)
        {
            _userLongitude = Solution.ParseDouble(userLongitude);
            _userLatitude = Solution.ParseDouble(userLatitude);
            _debrilillators = debrilillatorLines.Select(Defibrillator.Parse);
        }

        public Defibrillator Find()
        {
            return _debrilillators.OrderBy(d => d.GetDistance(_userLongitude, _userLatitude)).First();
        }
    }

    public class Defibrillator
    {
        protected Defibrillator(string[] splits)
        {
            Id = splits[0];
            Name = splits[1];
            Address = splits[2];
            PhoneNumber = splits[3];
            Longitude = Solution.ParseDouble(splits[4]);
            Latitude = Solution.ParseDouble(splits[5]);
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public string Address { get; private set; }

        public string PhoneNumber { get; private set; }

        public double Longitude { get; private set; }

        public double Latitude { get; private set; }

        public static Defibrillator Parse(string defibrillatorLine)
        {
            var splits = defibrillatorLine.Split(';').Select(s => s.Trim());
            return new Defibrillator(splits.ToArray());
        }

        public double GetDistance(double userLongitude, double userLatitude)
        {
            //x = (longB - longA)*cos((latA + latB)/2)
            //y = latB - latA
            //d = sqrt(x^2 + y^2)*6371
            // b -> user
            var x = (userLongitude.ToRadians() - Longitude.ToRadians()) * Math.Cos((userLatitude + Latitude) / 2);
            var y = userLatitude.ToRadians() - Latitude.ToRadians();
            return Math.Sqrt(x * x + y * y) * 6371;
        }
    }

    public static class NumericExtensions
    {
        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}
