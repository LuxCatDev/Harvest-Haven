using Godot;
using System;

namespace Items;

public enum ProductCategory
{
	Product,
	Consumable
}

[GlobalClass]
public partial class Product : ItemData
{
	[Export]
	public ProductCategory Type;

	[Export]
	public ProductData Data;

	Product(): this(ProductCategory.Product, null) {}

	public Product(ProductCategory type, ProductData data)
	{
		Type = type;
		Data = data;
	}
}
