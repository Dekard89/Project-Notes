namespace Note.BLL.DTO;

public record NoteResponse(long Id, string Title, string Description, string ChangeTime, int Status);