using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Application.Dtos
{
    public class UpdateStockDto : StockBaseDto
    {
        public Guid Id { get; set; }
    }
}
