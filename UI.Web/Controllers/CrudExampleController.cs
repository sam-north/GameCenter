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
        public ICrudExampleLogic CrudExampleLogic { get; }
        public CrudExampleController(ICrudExampleLogic crudExampleLogic)
        {
            CrudExampleLogic = crudExampleLogic;
        }

        [HttpGet]
        public ActionResult<List<CRUDExample>> Get()
        {
            return CrudExampleLogic.Get();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<CRUDExample> Get(int id)
        {
            return Ok(CrudExampleLogic.Get(id));
        }

        [HttpPost]
        public ActionResult<int> Create(CRUDExample model)
        {
            var entity = CrudExampleLogic.Save(model);
            return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<CRUDExample> Put(int id, CRUDExample model)
        {
            model.Id = id;
            return Ok(CrudExampleLogic.Save(model));
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            CrudExampleLogic.Delete(id);
            return Ok();
        }
    }
}
