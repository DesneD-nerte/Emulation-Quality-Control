using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Emulation_Quality_Control.Classes;
using Emulation_Quality_Control.Classes.Details;

namespace Tests
{
    [TestClass]
    public class MachineTests
    {
        CheckerContainer checkerContainer = FullCheckerContainer();

        [TestMethod]
        public void GetDetailEqualOfTypesTest()
        {
            Bolt bolt = new Bolt();
            Assert.AreEqual(bolt.GetType(), checkerContainer.CreateDetail(new Bolt()).GetType());

            Screw screw = new Screw();
            Assert.AreEqual(screw.GetType(), checkerContainer.CreateDetail(new Screw()).GetType());

            Nail nail = new Nail();
            Assert.AreEqual(nail.GetType(), checkerContainer.CreateDetail(new Nail()).GetType());

            Wheel wheel = new Wheel();
            Assert.AreEqual(wheel.GetType(), checkerContainer.CreateDetail(new Wheel()).GetType());
        }

        [TestMethod]
        public void GetNullDetailTest()
        {
            IDetail bolt = null;

            Assert.ThrowsException<NullReferenceException>(() => checkerContainer.CreateDetail(bolt));
        }

        static CheckerContainer FullCheckerContainer()
        {
            CheckerContainer checkerContainer = new CheckerContainer();

            checkerContainer.Register(new Bolt().GetType(), new BoltChecker());
            checkerContainer.Register(new Nail().GetType(), new NailChecker());
            checkerContainer.Register(new Screw().GetType(), new ScrewChecker());
            checkerContainer.Register(new Wheel().GetType(), new WheelChecker());

            return checkerContainer;
        }
    }
}
