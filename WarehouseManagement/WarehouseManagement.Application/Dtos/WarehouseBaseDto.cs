using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Application.Dtos
{
    public class WarehouseBaseDto
    {
        public required string Name { get; set; }

        public string? Location { get; set; }
    }
}
