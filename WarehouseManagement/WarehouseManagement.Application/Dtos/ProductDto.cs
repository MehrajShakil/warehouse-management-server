using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseManagement.Application.Dtos
{
    public class ProductDto : ProductBaseDto
    {
        public Guid Id { get; set; }
    }
}
