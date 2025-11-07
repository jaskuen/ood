// See https://aka.ms/new-console-template for more information

using Proxy.Lib.CoW;

CoW<IList<int>> list1 = new CoW<IList<int>>(new SharedPtr<IList<int>>(new List<int>() { 1, 2, 3 }));
CoW<IList<int>> list2 = list1.Clone();
CoW<IList<int>> list3 = list2.Clone();

Console.WriteLine(list1.Value.Count);
Console.WriteLine(list2.Value.Count);

list2.Value = [1, 2, 3, 4];

Console.WriteLine(list1.Value.Count);
Console.WriteLine(list2.Value.Count);

list3.Value = [1, 2, 3];