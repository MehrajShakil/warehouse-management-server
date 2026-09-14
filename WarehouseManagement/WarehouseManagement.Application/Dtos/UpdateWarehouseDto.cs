using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Application.Dtos
{
    public class UpdateWarehouseDto : WarehouseBaseDto
    {
        public Guid Id { get; set; }
    }
}
