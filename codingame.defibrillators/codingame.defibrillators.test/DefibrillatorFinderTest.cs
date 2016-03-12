namespace codingame.defibrillators.test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestClass]
    public class DefibrillatorFinderTest
    {
        private DefibrillatorFinder _finder;

        [TestMethod]
        public void Test()
        {
            var longiture = "3,879483";
            var latitude = "43,608177";
            var defibLines = new[]
            {
                "1;Maison de la Prevention Sante;6 rue Maguelone 340000 Montpellier;;3,87952263361082;43,6071285339217",
                "2;Hotel de Ville;1 place Georges Freche 34267 Montpellier;;3,89652239197876;43,5987299452849",
                "3;Zoo de Lunaret;50 avenue Agropolis 34090 Mtp;;3,87388031141133;43,6395872778854"
            };

            _finder = new DefibrillatorFinder(longiture, latitude, defibLines);

            Check.That(_finder.Find().Id).IsEqualTo("1");
        }
    }
}
