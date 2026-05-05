using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Domain.Enums
{
    public enum OrderStatus
    {
        New = 0,
        Accepted = 1,
        InProgress = 2,
        Delivered = 3,
        Cancelled = 4
    }
}
