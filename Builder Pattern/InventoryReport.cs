using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Builder_Pattern
{
    public class FurnitureItem
    {
        public string Name;
        public double Price;
        public double Height;
        public double Width;
        public double Weight;

        public FurnitureItem(string productName, double price, double height, double width, double weight)
        {
            this.Name = productName;
            this.Price = price;
            this.Height = height;
            this.Width = width;
            this.Weight = weight;
        }
    }

    // this is the complex object
    // it doesn't have the constructor because we are going to set the values in the concrete builder representations
    public class InventoryReport
    {
        public string TitleSection;
        public string DimensionsSection;
        public string LogisticsSection;

        public string Debug()
        {
            // this is a built-in builder pattern
            return new StringBuilder()
                .AppendLine(TitleSection)
                .AppendLine(DimensionsSection)
                .AppendLine(LogisticsSection)
                .ToString();
        }
    }

    // create a builder interface
    public interface IFurnitureInventoryBuilder
    {
        IFurnitureInventoryBuilder AddTitle();
        IFurnitureInventoryBuilder AddDimensions();
        IFurnitureInventoryBuilder AddLogistics(DateTime dateTime);

        // return the built item once we finished constructing it
        // each concrete builder class would be in charge of implementing its own method to do this
        // if the scenario is general enough, we can also add it here to make it cleaner, otherwise you should add this method in your concrete class
        InventoryReport GetDailyReport();
    }

    // concrete builder implements interface
    public class DailyReportBuilder : IFurnitureInventoryBuilder
    {
        // make sure every time the builder will have a new object to work with
        // defensive programming, not required but good to have
        private InventoryReport _report;
        private IEnumerable<FurnitureItem> _items;

        public DailyReportBuilder(IEnumerable<FurnitureItem> items)
        {
            Reset();
            _items = items;
        }

        private void Reset()
        {
            _report = new InventoryReport();
        }

        public IFurnitureInventoryBuilder AddDimensions()
        {
            _report.DimensionsSection = String.Join(Environment.NewLine, _items.Select(product =>
                $"Product: {product.Name} \nPrice: {product.Price} \n" +
                $"Height: {product.Height} x Width: {product.Width} -> Weight: {product.Weight}"
            ));
            return this;
        }

        public IFurnitureInventoryBuilder AddLogistics(DateTime dateTime)
        {
            _report.LogisticsSection = $"Report generated on {dateTime}";
            return this;
        }

        public IFurnitureInventoryBuilder AddTitle()
        {
            _report.TitleSection = "------------- Daily Inventory Report ----------- \n\n";
            return this;
        }

        public InventoryReport GetDailyReport()
        {
            InventoryReport finishedReport = _report;
            Reset();
            return finishedReport;
        }
    }

    // add a director class
    // used to execute the object's build steps in a predetermined sequence
    // director classes are not already present in builder patterns because you can technically call the methods directly from any concrete builder class
    // but this approach is not as clean as having a director class to take care of it
    public class InventoryBuilderDirector
    {
        private IFurnitureInventoryBuilder _builder;
        public InventoryBuilderDirector(IFurnitureInventoryBuilder builder)
        {
            _builder = builder;
        }

        public void CreateReport()
        {
            _builder.AddTitle();
            _builder.AddDimensions();
            _builder.AddLogistics(DateTime.UtcNow);
        }
    }
}
