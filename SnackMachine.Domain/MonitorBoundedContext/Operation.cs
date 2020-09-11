using System;
using SnackMachine.Domain.Base;

namespace SnackMachine.Domain.MonitorBoundedContext
{
    public class BuyOperation : Entity
    {
        public BuyOperation(int slotPosition, int snackPileQuantityLeft, string snackName, DateTime dateTime)
        {
            SlotPosition = slotPosition;
            SnackPileQuantityLeft = snackPileQuantityLeft;
            SnackName = snackName;
            DateTime = dateTime;
        }

        public int SlotPosition { get; }
        public int SnackPileQuantityLeft { get; }
        public string SnackName { get; }
        public DateTime DateTime { get; }
    }
}
