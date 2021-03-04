using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Emulation_Quality_Control;
using Emulation_Quality_Control.Classes;
using Emulation_Quality_Control.Classes.Details;

namespace Tests
{
    [TestClass]
    public class CooperateMachinesTests
    {
        CheckerContainer checkerContainer = FullCheckerContainer();

        CooperateMachines CM;

        [TestMethod]
        public void StartWorkTest()
        {
            CM = new CooperateMachines(new Mock<IDisplay>().Object, checkerContainer);
        }



        //public void StartWork()
        //{
        //    Ititialize();

        //    while (AreMachinesWork() == true)
        //    {
        //        IDetail detail = machine.GetDetail();

        //        PutDetailOnConveyorAndMove1Step(detail);

        //        CheckConveyorAndDetail();

        //        Thread.Sleep(1000);
        //    }
        //}

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
