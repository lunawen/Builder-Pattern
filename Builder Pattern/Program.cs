using System;
using System.Collections.Generic;

namespace Builder_Pattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new List<FurnitureItem>
            {
                new FurnitureItem("Sectional Couch", 55.5, 22.4, 78.6, 35.0),
                new FurnitureItem("Nightstand", 25.0, 12.4, 20.0, 10.0),
                new FurnitureItem("Dining Table", 105.0, 35.4, 100.6, 55.5),
            };

            // create an instance of the concrete class
            var inventoryBuilder = new DailyReportBuilder(items);
            // call methods directly to build object
            // the fluent variation doesn't require a director class
            var fluentReport = inventoryBuilder
                .AddTitle()
                .AddDimensions()
                .AddLogistics(DateTime.UtcNow)
                .GetDailyReport();

            Console.WriteLine(fluentReport.Debug());

            /*
            // set up a director class
            var director = new InventoryBuilderDirector(inventoryBuilder);
            // use director to build the item
            director.CreateReport();
            // get the report from the concrete class
            var directorReport = inventoryBuilder.GetDailyReport();
            Console.WriteLine(directorReport.Debug());

            */
        }
    }
}
