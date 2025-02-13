using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Note.BLL.DTO;
using Note.Domain;
using Note.DAL;
namespace Note.BLL;

public class NoteStore(NoteContext context,IDistributedCache cache) : IRepository
{
    private readonly NoteContext _context = context;

    private readonly IDistributedCache _cache = cache;

    public async Task<ICollection<NoteResponse>> GetNotes(int status, int page, int pageSize)
    {
        var notes =await _context.Notes.AsNoTracking()
            .Where(x => x.Status == (Status)status)
            .OrderByDescending(x=>x.ChangeTime)
            .Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();
        
        

        foreach (var note in notes)
        {
            await _cache.SetStringAsync(note.Id.ToString(),JsonSerializer.Serialize(note),new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            }); 
        }

        var responses = notes.Select(x => Mapper.MapToNoteResponse(x)).ToList();
        
        return responses;
    }

    public async Task<NoteResponse> GetNote(long id)
    {
        NoteResponse response;
        
        var noteString= await _cache.GetStringAsync(id.ToString());

        if (!string.IsNullOrEmpty(noteString))
        {
            response = JsonSerializer.Deserialize<NoteResponse>(noteString)?? 
                       throw new NullReferenceException();
        }
        else
        {
            var note = await _context.Notes.FirstOrDefaultAsync(x=>x.Id==id);
            response = Mapper.MapToNoteResponse(note)??
                       throw new Exception("Note not found");
        }
        return response;
    }

    public async Task Create(NoteRequest request)
    {
        var note = Mapper.MapToNoteEntity(request);
        if (note == null)
        {
            throw new ArgumentNullException(nameof(note));
        }
        _context.Notes.Attach(note);
        await _context.SaveChangesAsync();
        
    }

    public async Task Update(NoteResponse response)
    {
        var note = await _context.Notes
            .Where(x => x.Id == response.Id)
            .ExecuteUpdateAsync(n => n
                .SetProperty(i => i.ChangeTime, DateTime.UtcNow)
                .SetProperty(i => i.Status, (Status)response.Status));
    }

    public async Task Delete(long id)
    {
        var note = await _context.Notes.Where(x=>x.Id==id)
            .ExecuteDeleteAsync();
        
    }
}