using EFTests;

await new ClientService().GetLatestOrderForEachClient(new MyDbContext());  

Console.WriteLine("Finished...");
Console.ReadLine();