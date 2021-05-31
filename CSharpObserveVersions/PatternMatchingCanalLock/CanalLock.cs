using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpObserveVersions.PatternMatchingCanalLock
{
    public enum WaterLevel
    {
        Low,
        High
    }
    public class CanalLock
    {
        public WaterLevel WaterLevel { get; private set; } = WaterLevel.Low;
        public bool LowWaterGateOpen { get; private set; } = false;
        public bool HighWaterGateOpen { get; private set; } = false;

        public void SetLowGate(bool open)
        {
            LowWaterGateOpen = (open, LowWaterGateOpen, WaterLevel) switch
            {
                (false, _, _) => false,
                (true, _, WaterLevel.Low) => true,
                (true, false, WaterLevel.High) => throw new InvalidOperationException("Cannot open low gate when the water is high"),
                _ => throw new InvalidOperationException("Invalid internal state"),
            };
        }

        public void SetHighGate(bool open)
        {
            HighWaterGateOpen = (open, HighWaterGateOpen, WaterLevel) switch
            {
                (false, _, _) => false,
                (true, _, WaterLevel.High) => true,
                (true, false, WaterLevel.Low) => throw new InvalidOperationException("Cannot open high gate when the water is low"),
                _ => throw new InvalidOperationException("Invalid internal state"),
            };
        }

        public void SetWaterLevel(WaterLevel newLevel)
        {
            WaterLevel = (newLevel, WaterLevel, LowWaterGateOpen, HighWaterGateOpen) switch
            {
                (WaterLevel.Low, WaterLevel.Low, true, false) => WaterLevel.Low,
                (WaterLevel.High, WaterLevel.High, false, true) => WaterLevel.High,
                (WaterLevel.Low, _, false, false) => WaterLevel.Low,
                (WaterLevel.High, _, false, false) => WaterLevel.High,
                (WaterLevel.Low, WaterLevel.High, false, true) => throw new InvalidOperationException("Cannot lower water when the high gate is open"),
                (WaterLevel.High, WaterLevel.Low, true, false) => throw new InvalidOperationException("Cannot raise water when the low gate is open"),
                _ => throw new InvalidOperationException("Invalid internal state"),
            };
        }
    }
}
