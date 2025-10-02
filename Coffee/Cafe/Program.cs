// See https://aka.ms/new-console-template for more information

using Cafe.Lib;
using Cafe.Lib.Condiments;
using Cafe.Lib.Implementation.CoffeeBeverages;
using Cafe.Lib.Implementation.TeaBeverages;

IBeverage coffee = new Latte(CoffeeSize.Standard);
Console.WriteLine($"{coffee.GetDescription()} {coffee.GetCost()}");

coffee = new Cappuccino(CoffeeSize.Double);
Console.WriteLine($"{coffee.GetDescription()} {coffee.GetCost()}");

coffee = new IceCubes(coffee, 10, IceCubeType.Dry);
coffee = new ChocolateCrumbs(coffee, 20);
coffee = new ChocolatePieces(coffee, 4);
coffee = new Cream(coffee);
coffee = new Liquor(coffee, LiquorType.Chocolate);
Console.WriteLine($"{coffee.GetDescription()} {coffee.GetCost()}");

coffee = new Tea(TeaType.Bergamot);
coffee = new Lemon(coffee, 150);
Console.WriteLine($"{coffee.GetDescription()} {coffee.GetCost()}");
