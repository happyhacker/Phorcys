using System.Collections.Generic;
using NUnit.Framework;
using Phorcys.Services.Contracts.Dtos;
using SharpArch.Testing.NUnit;

namespace Phorcys.Tests.Unit.Services.Contracts.Dtos
{
    [TestFixture]
    public class EnumerationDtoTests
    {
        [Test]
        public void CanCompareVendorLightDto()
        {
            EnumerationDto dto = GetEnumerationDto("");
            dto.Name.ShouldEqual("TestName");
        }

        [Test]
        public void EqualsTest()
        {
            EnumerationDto thisDto = GetEnumerationDto("");
            EnumerationDto thatDto = GetEnumerationDto("");
            thisDto.Equals(thatDto).ShouldEqual(true);
        }

        [Test]
        public void NotEqualsName()
        {
            EnumerationDto thisDto = GetEnumerationDto("");
            EnumerationDto thatDto = GetEnumerationDto("");
            thatDto.Name = thisDto.Name + "abc";

            thisDto.Equals(thatDto).ShouldBeFalse();
        }

        /// <summary>
        /// Two distinct yet equal objects should return the same hashcode.
        /// </summary>
        [Test]
        public void GetHashCodeTest()
        {
            EnumerationDto thisDto = GetEnumerationDto("");
            EnumerationDto thatDto = GetEnumerationDto("");
            thisDto.GetHashCode().ShouldEqual(thatDto.GetHashCode());
        }

        /// <summary>
        /// Check 1000 non-equal values and make sure that there are at least 990 distinct hashcode values amongst them.
        /// </summary>
        [Test]
        public void GetHashCodeDistributionTest()
        {
            const int testObjectCount = 1000;
            const float acceptableCollisionRation = 0.01f;

            List<int> hashList = new List<int>();
            int distinctHashCodeValues = 0;

            for (int i = 0; i < testObjectCount; i++)
            {
                EnumerationDto enumerationDto = GetEnumerationDto(i.ToString());
                int currHashCode = enumerationDto.GetHashCode();

                if (!hashList.Contains(currHashCode))
                {
                    distinctHashCodeValues++;
                    hashList.Add(currHashCode);
                }
            }


            Assert.Greater(distinctHashCodeValues, testObjectCount * (1 - acceptableCollisionRation),
                           string.Format(
                               "Hash does not appear to be well distributed.  With {0} values tested, there were only {1} distinct hash codes.",
                               testObjectCount, distinctHashCodeValues));
        }

        private static EnumerationDto GetEnumerationDto(string addedString)
        {
            EnumerationDto dto = new EnumerationDto { Name = "TestName" + addedString };

            return dto;
        }
    }
}
