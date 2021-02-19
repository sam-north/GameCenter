using Core.Logic.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace UI.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrudExampleController : ControllerBase
    {
        public ICrudExampleLogic logic { get; }
        public CrudExampleController(ICrudExampleLogic crudExampleLogic)
        {
            logic = crudExampleLogic;
        }

        [HttpGet]
        public ActionResult<List<CRUDExample>> Get()
        {
            return logic.Get();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<CRUDExample> Get(int id)
        {
            return Ok(logic.Get(id));
        }

        [HttpPost]
        public ActionResult<int> Post(CRUDExample model)
        {
            var entity = logic.Save(model);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<CRUDExample> Put(int id, CRUDExample model)
        {
            model.Id = id;
            return Ok(logic.Save(model));
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            logic.Delete(id);
            return Ok();
        }
    }
}
