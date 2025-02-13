using Note.BLL.DTO;
using Note.Domain;

namespace Note.BLL;

public static class Mapper
{
    public static NoteResponse MapToNoteResponse(NoteEntity entity)
    {
        return new NoteResponse(entity.Id, entity.Title, entity.Description,entity.ChangeTime.ToString("yyyy-MM-dd HH:mm"),(int)entity.Status);
    }

    public static NoteEntity MapToNoteEntity(NoteRequest request)
    {
        var entity = new NoteEntity
        {
            Title = request.Title,

            Description = request.Description,

            ChangeTime = DateTime.UtcNow.ToLocalTime(),

            Status = Status.InWork
        };
        return entity;
    }
}