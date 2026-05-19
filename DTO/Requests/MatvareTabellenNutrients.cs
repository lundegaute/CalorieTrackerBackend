
namespace CalorieTracker.DTO.Requests;

public class Matvaretabellen_Nutrients
    {
        public string uri { get; set; }
        public string nutrientId { get; set; }
        public string name { get; set; }
        public string euroFirId { get; set; }
        public string euroFirName { get; set; }
        public string unit { get; set; }
        public int decimalPrecision { get; set; }
        public string parentId { get; set; }
    }

    public class Matvaretabellen_Nutrients_Wrapper
    {
        public List<Matvaretabellen_Nutrients> nutrients { get; set; }
        public string locale { get; set; }
    }