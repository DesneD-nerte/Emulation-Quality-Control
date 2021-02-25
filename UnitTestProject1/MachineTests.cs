using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Эмуляция_контроля_качества.Classes;
using Эмуляция_контроля_качества.Classes.Details;

namespace Tests
{
    [TestClass]
    class MachineTests
    {
        CheckerContainer checkerContainer = FullCheckerContainer();

        [TestMethod]
        public void CreateDetailBoltTest()
        {
            Bolt bolt = new Bolt();
            Assert.AreEqual(bolt.GetType(), checkerContainer.CreateDetail(new Bolt()));
        }


        //private IDetail CreateDetail()
        //{
        //    IDetail bolt = new Bolt();
        //    IDetail nail = new Nail();
        //    IDetail screw = new Screw();
        //    IDetail wheel = new Wheel();

        //    IDetail[] details = new IDetail[] { bolt, nail, screw, wheel };

        //    int index = rnd.Next(4);

        //    return checkerContainer.CreateDetail(details[index]);
        //}

        [TestMethod]
        public void GetDetailTest()
        {
            
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
