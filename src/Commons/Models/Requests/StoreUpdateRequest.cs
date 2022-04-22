﻿using Models.Core;
using System.Collections.Generic;

namespace Models.Requests
{
    public class StoreUpdateRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public List<Product> Products { get; set; }

        public bool AutoAccept { get; set; }

        public StoreLocation StoreLocation { get; set; }
    }
}
