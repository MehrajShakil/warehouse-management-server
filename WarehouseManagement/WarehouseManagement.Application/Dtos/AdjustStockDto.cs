using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Application.Dtos
{
    public class AdjustStockDto
    {
        public required int QuantityDelta { get; set; }

        public string? Reason { get; set; }
    }
}
