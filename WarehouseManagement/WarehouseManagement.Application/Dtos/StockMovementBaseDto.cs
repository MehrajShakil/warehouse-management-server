using System;
using System.Collections.Generic;
using System.Text;
using WarehouseManagement.Domain.Enum;

namespace WarehouseManagement.Application.Dtos
{
    public class StockMovementBaseDto
    {
        public required StockMovementType MovementType { get; set; }

        public required Guid ProductId { get; set; }

        public required Guid WarehouseId { get; set; }

        public required int Quantity { get; set; }
    }
}
