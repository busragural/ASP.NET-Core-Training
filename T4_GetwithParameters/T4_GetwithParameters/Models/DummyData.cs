namespace T3_Case_Get
{
    public class DummyData
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public DateOnly Created_date { get; set; }

    }


    public class DataList
    {
        public List<DummyData> Data { get; set;}
        public DummyData Element { get; set; }
    }
}
