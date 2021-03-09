using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Emulation_Quality_Control.Classes;
using Emulation_Quality_Control.Classes.Details;

namespace Tests
{
    [TestClass]
    public class CheckMachineTests
    {
        CheckerContainer checkerContainer = FullCheckerContainer();

        [TestMethod]
        public void CheckDetailTest()
        {
            IDetail bolt = new Bolt(0.5, 0.5, 3);

            Assert.IsTrue(checkerContainer.CheckDetail(bolt));

            IDetail screw = new Screw(0.2, 0.3, 4);

            Assert.IsTrue(checkerContainer.CheckDetail(screw));

            IDetail nail = new Nail(0.2, 0.3, 4);

            Assert.IsTrue(checkerContainer.CheckDetail(nail));

            IDetail wheel = new Wheel(10, 4, 10);

            Assert.IsTrue(checkerContainer.CheckDetail(wheel));
        }

        //100 попыток чтобы создать плохую деталь (при каждой попытке шанс 0.1 => ¬ итогевсегда создаетс€)
        [TestMethod]
        public void CheckDetailTrashTest()
        {
            IDetail bolt = new Bolt(1, 1, 2);
            for (int i = 0; i < 100; i++)
            {
                if(checkerContainer.CheckDetail(bolt))
                {
                    Assert.IsTrue(checkerContainer.CheckDetail(bolt));
                    break;
                }
            }

            IDetail screw = new Screw(1, 1, 2);
            for (int i = 0; i < 100; i++)
            {
                if (checkerContainer.CheckDetail(bolt))
                {
                    Assert.IsTrue(checkerContainer.CheckDetail(screw));
                    break;
                }
            }

            IDetail nail = new Nail(1, 1, 2);
            for (int i = 0; i < 100; i++)
            {
                if (checkerContainer.CheckDetail(bolt))
                {
                    Assert.IsTrue(checkerContainer.CheckDetail(nail));
                    break;
                }
            }
            
            IDetail wheel = new Wheel(1, 1, 2);
            for (int i = 0; i < 100; i++)
            {
                if (checkerContainer.CheckDetail(bolt))
                {
                    Assert.IsTrue(checkerContainer.CheckDetail(wheel));
                    break;
                }
            }
            
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
