using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataTutorial.Entities;
using ODataTutorial.EntityFramework;
using ODataTutorial.Models;
using System.Linq;

namespace ODataTutorial.Controllers
{
    public class NotesController : ODataController
    {
        private readonly db_sfsyncContext _db;

        private readonly ILogger<NotesController> _logger;

        public NotesController(db_sfsyncContext dbContext, ILogger<NotesController> logger)
        {
            _logger = logger;
            _db = dbContext;
        }


        //[EnableQuery(PageSize = 15)]
        [EnableQuery]
        public IQueryable<UserActivityLog> Get()
        {
            return (IQueryable<UserActivityLog>)_db.UserActivityLogs;
        }


        [EnableQuery]
        public SingleResult<UserActivityLog> Get([FromODataUri] int key)
        {
            var result = _db.UserActivityLog.Where(c => c.Id == key);
            return SingleResult.Create(result);
        }

        /*[EnableQuery]
        public IQueryable<PbisInteraction> Get()
        {
            return (IQueryable<PbisInteraction>)_db.PbisInteraction;
        }*/

        /*[EnableQuery]
        public async Task<IActionResult> Post([FromBody] UserActivityLog note)
        {
            _db.UserActivityLog.Add(note);
            await _db.SaveChangesAsync();
            return Created(note);
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<UserActivityLog> note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingNote = await _db.UserActivityLog.MinAsync(key);
            if (existingNote == null)
            {
                return NotFound();
            }

            note.Patch(existingNote);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(existingNote);
        }

        private bool NoteExists(string key)
        {
            throw new NotImplementedException();
        }

        [EnableQuery]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var existingNote = await _db.UserActivityLog.MinAsync(key);
            if (existingNote == null)
            {
                return NotFound();
            }

            _db.UserActivityLog.Remove(existingNote);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }*/

        [HttpGet]
        public IActionResult GetNewData()
        {
            var result = _db.UserActivityLog.ToList();
            return Ok(result);
        }
    }
}
