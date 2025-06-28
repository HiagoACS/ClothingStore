using System;

using ClothingStore.Core.Models;

namespace ClothingStore.Core.Interfaces
{
    public interface IPaymentProcess
    {
        bool PaymentProcess(Order order);
    }
}
