using Microsoft.AspNetCore.Mvc;
using System;
using T3_Case_Get;

namespace T4_GetwithParameters.Controllers
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
        private List<DummyData> GenerateDummyData(int count)
        {
            if(count > 0)
            {
                List<DummyData> dummyData = new List<DummyData>();

                for(int i=1; i<=count; i++)
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

        [HttpGet("{count}")]
  
        public IActionResult Get(int count)
        {
            List<DummyData> dList = GenerateDummyData(count);
            if (dList == null)
            {
                return BadRequest("Count must be greater than 0");
            }

            return Ok(dList);
        }

        [HttpGet("{count}/{index}")]
        public IActionResult Get(int count, int index)
        {
            List<DummyData> dList = GenerateDummyData(count);
            if (index >= 0 && index < dList.Count)
            {
                DummyData element = dList[index-1];
                DataList dataList = new DataList
                {
                    Data = dList,
                    Element = element
                };

                return Ok(dataList);
            }
            
             return BadRequest();
       
        }


    }
}
