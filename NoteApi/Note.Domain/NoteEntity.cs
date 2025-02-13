namespace Note.Domain;

public class NoteEntity
{
    public long Id { get; set; }
    
    public string Title { get; set; }=String.Empty;
    
    public string Description { get; set; } = String.Empty;
    
    public DateTime ChangeTime { get; set; }
    
    public Status Status { get; set; }
    
}