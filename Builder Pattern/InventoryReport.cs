using System;
using System.Collections.Generic;
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
            return new StringBuilder()
                .AppendLine(TitleSection)
                .AppendLine(DimensionsSection)
                .AppendLine(LogisticsSection)
                .ToString();
        }
    }

    public interface IFurnitureInventoryBuilder
    {
        void AddTitle();
        void AddDimensions();
        void AddLogistics();

        // return the built item once we finished constructing it
        // each concrete builder class would be in charge of implementing its own method to do this
        // if the scenario is general enough, we can also add it here to make it cleaner, otherwise you should add this method in your concrete class
        InventoryReport GetDailyReport();
    }
}
