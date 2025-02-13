using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Note.BLL;
using Note.BLL.DTO;
using NoteApi.Filters;

namespace NoteApi.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly IValidator<NoteRequest> _validator;
    private readonly IRepository _store;

    public NotesController(IValidator<NoteRequest> validator, IRepository repository)
    {
        _validator = validator;
        _store = repository;
    }
    [HttpGet("[action]")]
    public async Task<ActionResult<ICollection<NoteResponse>>> GetAll([FromQuery] NoteFilter filter)
    {
        var notes= await _store.GetNotes(status:filter.Status, page:filter.Page, pageSize:filter.PageSize);
        
        return Ok(notes);
    }

    [HttpPost($"[action]")]
    public async Task<ActionResult> Create([FromBody] NoteRequest note)
    {
        var result = await _validator.ValidateAsync(note);

        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }
        await _store.Create(note);
        
        return Ok();
    }

    [HttpPatch("[action]")]
    public async Task<ActionResult> Update([FromBody] NoteResponse note)
    {
        await _store.Update(note);
        
        return Ok();
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        await _store.Delete(id);
        
        return Ok();
    }
}