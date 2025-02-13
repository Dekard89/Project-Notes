using Note.BLL.DTO;
using Note.Domain;

namespace Note.BLL;

public interface IRepository
{
    Task<ICollection<NoteResponse>> GetNotes(int status, int page, int pageSize);
    
    Task<NoteResponse> GetNote(long id);
    
    Task Create (NoteRequest request);
    
    Task Update (NoteResponse response);
    
    Task Delete (long id);
}