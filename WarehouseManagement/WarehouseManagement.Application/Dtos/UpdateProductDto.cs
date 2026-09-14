using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Application.Dtos
{
    public class UpdateProductDto : ProductBaseDto
    {
        public Guid Id { get; set; }
    }
}
