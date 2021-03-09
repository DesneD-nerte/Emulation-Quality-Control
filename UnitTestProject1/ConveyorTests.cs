using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Emulation_Quality_Control.Classes;

namespace Tests
{
    [TestClass]
    public class ConveyorTests
    {
        Conveyor conveyor;
        [TestMethod]
        public void CheckPlacesOfConveyorsCorrectPositionsTest()
        {
            conveyor = new Conveyor();
            for (int i = 0; i < conveyor.Length; i++)
            {
                conveyor.PutDetailOn(new Bolt());
                conveyor.MoveDetails();
            }

            Assert.IsTrue(conveyor.CheckPlacesOfConveyor());
        }

        [TestMethod]
        public void CheckPlacesOfConveyorsIncorrectPositionsOfConveyorTest()
        {
            conveyor = new Conveyor();

            conveyor.MassivOfDetails[0] = new Bolt();
            conveyor.MassivOfDetails[2] = new Bolt();
            Assert.ThrowsException<ConveyorException>(() => conveyor.CheckPlacesOfConveyor());

            conveyor.MassivOfDetails[0] = null;
            Assert.ThrowsException<ConveyorException>(() => conveyor.CheckPlacesOfConveyor());

            conveyor.MassivOfDetails[0] = new Bolt();
            conveyor.MassivOfDetails[2] = null;
            Assert.ThrowsException<ConveyorException>(() => conveyor.CheckPlacesOfConveyor());
        }

        [TestMethod]
        public void CheckPlacesOfConveyorsEmptyPositionsOfConveyorTest()
        {
            conveyor = new Conveyor();

            Assert.IsFalse(conveyor.CheckPlacesOfConveyor());
        }

    }
}
