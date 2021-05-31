using CSharpObserveVersions.PatternMatchingCanalLock;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpObserveVersions.Tests
{
    [TestFixture]
    public class CanalLockTests
    {
        private CanalLock _canalLock;

        [SetUp]
        public void Setup()
        {
            _canalLock = new CanalLock();
        }

        [TearDown]
        public void TearDown()
        {
            _canalLock = null;
        }

        [Test]
        public void OpenHighGate_CanalHasLowWaterLevel_ThrowsException()
        {
            _canalLock.SetWaterLevel(WaterLevel.Low);

            Action result = () => _canalLock.SetHighGate(true);

            result.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void OpenHighGate_CanalHasHighWater_Ok()
        {
            _canalLock.SetWaterLevel(WaterLevel.High);

            _canalLock.SetHighGate(true);

            _canalLock.HighWaterGateOpen.Should().Be(true);
        }

        [Test]
        public void OpenLowGate_CanalHasLowHighLevel_ThrowsException()
        {
            _canalLock.SetWaterLevel(WaterLevel.High);

            Action result = () => _canalLock.SetLowGate(true);

            result.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void OpenLowGate_CanalHasLowWater_Ok()
        {
            _canalLock.SetWaterLevel(WaterLevel.Low);

            _canalLock.SetLowGate(true);

            _canalLock.LowWaterGateOpen.Should().Be(true);
        }

        [Test]
        public void SetHighWater_LowGateIsOpen_ThrowsException()
        {
            _canalLock.SetLowGate(true);

            Action result = () => _canalLock.SetWaterLevel(WaterLevel.High);

            result.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void SetLowWater_HighGateIsOpen_ThrowsException()
        {
            _canalLock.SetWaterLevel(WaterLevel.High);
            _canalLock.SetHighGate(true);

            Action result = () => _canalLock.SetWaterLevel(WaterLevel.Low);

            result.Should().Throw<InvalidOperationException>();
        }


        [Test]
        public void SetLowWater_GatesAreClosed_Ok()
        {
            _canalLock.SetLowGate(false);
            _canalLock.SetHighGate(false);

            _canalLock.SetWaterLevel(WaterLevel.Low);

            _canalLock.WaterLevel.Should().Be(WaterLevel.Low);
        }
    }
}
