﻿namespace Models.Core;

public class Store
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public List<Product> Products { get; set; }

    public bool AutoAccept { get; set; }

    public StoreLocation StoreLocation { get; set; }
}