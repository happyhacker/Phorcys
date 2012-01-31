using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Phorcys.Core.Domains;
using SharpArch.Testing.NUnit;

namespace Phorcys.Tests.Unit.Core.Domains
{
    [TestFixture]
    public class EnumerationTests
    {
        [Test]
        public void CanCompareEnumerations()
        {
            Enumeration instance = new Enumeration("Sample Name");

            Enumeration instanceToCompareTo = new Enumeration("Sample Name");

            instance.ShouldEqual(instanceToCompareTo);
        }

        [Test]
        public void CanChangeName()
        {
            //Establish Context
            Enumeration enumeration = new Enumeration("Sample Name");

            //Act
            enumeration.ChangeName("Changed Name");

            //Assert
            enumeration.Name.ShouldEqual("Changed Name");
        }

        [Test]
        public void CanDeactivateEnumeration()
        {
            //Establish Context
            Enumeration enumeration = new Enumeration("Sample Name");
            enumeration.Activate();
            enumeration.Active.ShouldBeTrue();

            //Act
            enumeration.Deactivate();

            //Assert
            enumeration.Active.ShouldBeFalse();
        }

        [Test]
        public void CanActivateEnumeration()
        {
            //Establish Context
            Enumeration enumeration = new Enumeration("SampleName");
            enumeration.Deactivate();
            enumeration.Active.ShouldBeFalse();

            //Act
            enumeration.Activate();

            //Assert
            enumeration.Active.ShouldBeTrue();
        }
    }
}
