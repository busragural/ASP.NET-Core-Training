using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasks.Models;
using Tasks.Services;

namespace Tasks.Controllers
{
    [Route("api/")]
    [ApiController]
    public class T6_CRUD_DBController : ControllerBase
    {
        
        private readonly PostgresContext _context;
        public T6_CRUD_DBController(PostgresContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetDB", Name = "GetDBData")]
        public async Task<ActionResult<List<Person>>> GetData()
        {
            return Ok(await _context.People.ToListAsync());
        }

        [HttpGet]
        [Route("GetDB/{id}")]
        public async Task<ActionResult<Person>> GetDataById(int id)
        {
            var person = await _context.People.FindAsync(id);
            if(person != null)
            {
                return Ok(person);

            }
            return BadRequest();            
        }

        [HttpPost]
        [Route("CreateDB")]

        public async Task<ActionResult<List<Person>>> AddData(Person person)
        {
            if (person == null)
            {
                BadRequest();
            }
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return Ok(person);
        }

        [HttpPut]
        [Route("UpdateDB")]

        public async Task<ActionResult<List<Person>>> UpdateData(Person model)
        {
            if (model == null || model.Id <= 0)
            {
                BadRequest();
            }

            var person = await _context.People.FindAsync(model.Id);
            if(person == null)
            {
                return BadRequest();
            }
            
            person.Name = model.Name;
            person.CreatedDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(person);

        }


        [HttpDelete("DeleteDB/{id}", Name = "DeleteDataDB")]

        public async Task<ActionResult<List<Person>>> DeleteData(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return BadRequest();
            }
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return Ok($"{person.Id} deleted");
        }

        [HttpPost]
        [Route("BulkCreateDataDB", Name = "CreateBulkDataDB")]
        public async Task<ActionResult<List<Person>>> CreateBulkData(int count)
        {
            Person lastPerson = await _context.People.OrderByDescending(p => p.Id).FirstOrDefaultAsync();
            List<Person> newDatas;
            if (lastPerson == null)
            {
                newDatas = GenerateData.GeneratePerson(0, count);
            }
            else
            {
                newDatas = GenerateData.GeneratePerson(lastPerson.Id, count);


            }
            _context.People.AddRange(newDatas);
            await _context.SaveChangesAsync();
            return Ok(newDatas);

        }


        [HttpDelete("DeleteDB", Name = "DeleteDB")]

        public async Task<ActionResult<List<Person>>> DeleteAllData()
        {
            int count = await _context.People.CountAsync();
            if (count > 0)
            {
                _context.People.ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }


    }
}
