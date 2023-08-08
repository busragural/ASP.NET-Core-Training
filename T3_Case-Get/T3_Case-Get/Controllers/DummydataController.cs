using Microsoft.AspNetCore.Mvc;

namespace T3_Case_Get.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DummydataController : ControllerBase
    {
        private readonly ILogger<DummydataController> _logger;

        public DummydataController(ILogger<DummydataController> logger)
        {
            _logger = logger;
        }

        private List<DummyData> GenerateDummyData()
        {
            List<DummyData> dummyData = new List<DummyData>
            {
                new DummyData { id = 1, name = "Alair", created_date = "01/08/2023" },
                new DummyData { id = 2, name = "Demetris", created_date = "02/08/2023" },
                new DummyData { id = 3, name = "Andreas", created_date = "03/08/2023" },
                new DummyData { id = 4, name = "Malcolm", created_date = "04/08/2023" },
                new DummyData { id = 5, name = "Marmaduke", created_date = "05/08/2023" },
                new DummyData { id = 6, name = "Erek", created_date = "06/08/2023" },
                new DummyData { id = 7, name = "Carmelina", created_date = "07/08/2023" },
                new DummyData { id = 8, name = "Correna", created_date = "08/08/2023" },
                new DummyData { id = 9, name = "Sephira", created_date = "09/08/2023" },
                new DummyData { id = 10, name = "Alisa", created_date = "10/08/2023" }

            };

            return dummyData;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<DummyData> dummyDataList = GenerateDummyData();
            return Ok(dummyDataList);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List<DummyData> dummyDataList = GenerateDummyData();
            DummyData data = dummyDataList.FirstOrDefault(item => item.id == id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
    }
}