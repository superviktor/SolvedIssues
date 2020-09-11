using System;
using SnackMachine.Domain.Base;

namespace SnackMachine.Domain.SnackMachineBoundedContext
{
    public class SnackBought : IDomainEvent
    {
        public SnackBought(int slotPosition, int snackPileQuantityLeft, string snackName)
        {
            SlotPosition = slotPosition;
            SnackPileQuantityLeft = snackPileQuantityLeft;
            SnackName = snackName;
            DateTime = DateTime.UtcNow;
        }

        public int SlotPosition { get; }
        public int SnackPileQuantityLeft { get; }
        public string SnackName { get; }
        public DateTime DateTime { get; }
    }
}
