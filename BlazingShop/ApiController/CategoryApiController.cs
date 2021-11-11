using BlazingShop.DomainModel;
using BlazingShop.ServiceContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingShop.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryApiController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;

        }

        [HttpGet]
        public IActionResult Get()
        {
            var cato = _categoryRepo.AllCategories.ToList();
            return Ok(cato);
        }

        [HttpGet("{id}")]
        public ActionResult GetByid(int id)
        {
            var Byid = _categoryRepo.GetByid(id);
            if (Byid != null)
            {
                return Ok(Byid);

            }
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            else
            {
                var res = _categoryRepo.Create(category);
                return Ok(category);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Category category)
        {
            var res = _categoryRepo.Edit(id, category);
            if (res == null)
            {
                return BadRequest($"Not Availabel{id.ToString()}");
            }
            else
            {

                return Ok(res);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var del = _categoryRepo.Delete(id);
            if (del == null)
            {
                return BadRequest("Not Found");
            }
            else
            {

                return Ok(del);
            }
        }
    }
}
