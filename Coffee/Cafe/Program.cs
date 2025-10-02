// See https://aka.ms/new-console-template for more information

using Cafe.Lib.Implementation.CoffeeBeverages;

Coffee coffee = new Latte(CoffeeSize.Standard);
Console.WriteLine(coffee.GetDescription(), coffee.GetCost());

coffee = new Cappuccino(CoffeeSize.Double);
Console.WriteLine(coffee.GetDescription(), coffee.GetCost());
