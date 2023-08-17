using Tasks.Models;

namespace Tasks.Services
{
    public class GenerateData
    {

        public static List<Person> GeneratePerson(int first, int count)
        {
            if (count > 0)
            {
                List<Person> dummyData = new List<Person>();

                for (int i = first+1; i <= first +count; i++)
                {
                    Person model = new Person();
                    model.Name = "Person" + i.ToString();
                    model.Id = i;
                    model.CreatedDate  = DateTime.Now.AddDays(i);
                    dummyData.Add(model);
                    
                }
                return dummyData;
            }
            return null;
        }

    }
}
