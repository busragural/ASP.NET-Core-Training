namespace Tasks.Services
{
    public class GenerateData
    {

        public static List<DummyData> dummyData = new List<DummyData>
            {
                new DummyData { Id = 1, Name = "Alair", Created_date = DateOnly.FromDateTime(DateTime.Now) },
                new DummyData { Id = 2, Name = "Demetris", Created_date =DateOnly.FromDateTime(DateTime.Now) },
                new DummyData { Id = 3, Name = "Andreas", Created_date =DateOnly.FromDateTime(DateTime.Now) },
                new DummyData { Id = 4, Name = "Malcolm", Created_date = DateOnly.FromDateTime(DateTime.Now) },
                new DummyData { Id = 5, Name = "Marmaduke", Created_date = DateOnly.FromDateTime(DateTime.Now) },
                new DummyData { Id = 6, Name = "Erek", Created_date = DateOnly.FromDateTime(DateTime.Now) },
                new DummyData { Id = 7, Name = "Carmelina", Created_date = DateOnly.FromDateTime(DateTime.Now) },
                new DummyData { Id = 8, Name = "Correna", Created_date = DateOnly.FromDateTime(DateTime.Now) },
                new DummyData { Id = 9, Name = "Sephira", Created_date = DateOnly.FromDateTime(DateTime.Now) },
                new DummyData { Id = 10, Name = "Alisa", Created_date = DateOnly.FromDateTime(DateTime.Now) }

            };

       
        public static List<DummyData> GenerateDummyData(int count)
        {
            if (count > 0)
            {
                List<DummyData> dummyData = new List<DummyData>();

                for (int i = 1; i <= count; i++)
                {
                    DummyData model = new DummyData();
                    model.Name = "Person" + i.ToString();
                    model.Id = i;
                    model.Created_date = DateOnly.FromDateTime(DateTime.Now.AddDays(i));
                    dummyData.Add(model);
                }

                return dummyData;
            }
            return null;
        }
    }
}
