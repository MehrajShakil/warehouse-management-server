using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Application.Dtos
{
    public class StockBaseDto
    {
        public required Guid ProductId { get; set; }

        public required Guid WarehouseId { get; set; }

        public required int Quantity { get; set; }
    }
}
