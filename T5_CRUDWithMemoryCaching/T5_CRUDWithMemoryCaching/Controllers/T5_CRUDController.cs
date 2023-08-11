using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Tasks.Services;

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class T5_CRUDController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        public T5_CRUDController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        [Route("All", Name = "GetData")]
        public IActionResult GetAllData()
        {
            if (!_cache.TryGetValue("dummyData", out var cachedData)) //dummyData key'ine sahip bir oge var mi?
            {
                cachedData = GenerateData.dummyData;
                var cacheEntryOptions = new MemoryCacheEntryOptions // datanin ne kadar sure tutulacagini belirleyen MemoryCacheEntryOptions nesnesi
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) //10 dakika boyunca veri tutulacak
                };
                _cache.Set("dummyData", cachedData, cacheEntryOptions);
            }

            return Ok(cachedData);
        }

        [HttpDelete("Delete/{id}", Name = "DeleteData")]
        public bool DeleteData(int id)
        {
           
            var cachedData = _cache.Get("dummyData") as List<DummyData>; //dummyData key'ine sahip veriyi List<DummyData> türüne dönüştürür, as donusup donusmedigini kontrol eder. 
            if (cachedData != null)
            {
                var deleted = cachedData.FirstOrDefault(i => i.Id == id);
                if (deleted != null)
                {
                    cachedData.Remove(deleted);
                    _cache.Set("dummyData", cachedData);
                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<DummyData> CreateData([FromBody] DummyData model)
        {

            if (model == null)
            {
                return BadRequest();
            }

            int newId = GenerateData.dummyData.LastOrDefault().Id + 1;
            DummyData newData = new DummyData
            {
                Id = newId,
                Name = model.Name,
                Created_date = DateOnly.FromDateTime(DateTime.Now)
            };

            var cachedData = _cache.Get("dummyData") as List<DummyData>;
            if (cachedData != null)
            {
                cachedData.Add(newData);
                _cache.Set("dummyData", cachedData);
            }

            model.Id = newData.Id;
            return Ok(model);

        }

        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateData([FromBody] DummyData model)
        {
            if (model == null || model.Id <= 0)
            {
                BadRequest();
            }

            var cachedData = _cache.Get("dummyData") as List<DummyData>;
            if (cachedData != null)
            {
                var existing = cachedData.FirstOrDefault(i => i.Id == model.Id);
                if (existing != null)
                {
                    existing.Name = model.Name;
                    existing.Created_date = model.Created_date;
                    _cache.Set("dummyData", cachedData);
                    return NoContent();
                }
            }

            return NotFound();
        }

    }
}
