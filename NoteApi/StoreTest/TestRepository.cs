using Moq;
using Note.BLL;
using Note.BLL.DTO;
using Note.Domain;

namespace StoreTest;

public class TestRepository
{
    [Theory]
    [InlineData(1,1,3,3)]
    [InlineData(2,1,3,2)]
    [InlineData(3,1,3,1)]
    public async Task ReturnCollectionTest(int status, int page, int pageSize, int count)
    {
        var mock = new Mock<IRepository>();

        mock.Setup(repo => repo.GetNotes(status, page, pageSize)).Returns(GetNotes(status));
        
        var repo = mock.Object;
        
        var result = repo.GetNotes(status, page, pageSize);
        
        var model= await Assert.IsAssignableFrom<Task<ICollection<NoteResponse>>>(repo.GetNotes(status, page, pageSize));
        
        Assert.Equal(count, model.Count);
    }

    [Fact]
    public async Task AddNoteTest()
    {
        var mock = new Mock<IRepository>();

        mock.Setup(repo => repo.GetNotes(1, 1, 1)).Returns(GetNotes(1));
        

        mock.Setup(repo => repo.Create(It.IsAny<NoteRequest>())).Returns(Task.CompletedTask);
        
        var repo = mock.Object;
        
        var newRequest=new NoteRequest("New Note", "New Description");
        
        await repo.Create(newRequest);
        
        mock.Verify(repo => repo.Create(It.IsAny<NoteRequest>()), Times.Once);
    }

    [Fact]
    public async Task DeleteNoteTest()
    {
        var mock = new Mock<IRepository>();
        
        var repo = mock.Object;
        
        mock.Setup(repo => repo.GetNotes(1, 1, 1)).Returns(GetNotes(1));
        
        mock.Setup(repo => repo.Delete(It.IsAny<long>())).Returns(Task.CompletedTask);
        
        await repo.Delete(1);
        
        mock.Verify(repo => repo.Delete(It.IsAny<long>()), Times.Once);
        
        
    }
    



    private async Task<ICollection<NoteResponse>> GetNotes(int status)
    {
       var notes= new List<NoteResponse>()
        {
            new NoteResponse(1, "TestTitle", "TestContent", DateTime.Now.ToString("yyyy-MM-dd HH:mm"), 1),
            new NoteResponse(2, "TestTitle2", "TestContent2", DateTime.Now.ToString("yyyy-MM-dd HH:mm"), 1),
            new NoteResponse(3, "TestTitle3", "TestContent3",DateTime.Now.ToString("yyyy-MM-dd HH:mm"), 1),
            new NoteResponse(4, "TestTitle4", "TestContent4", DateTime.Now.ToString("yyyy-MM-dd HH:mm"), 2),
            new NoteResponse(5, "TestTitle5", "TestContent5", DateTime.Now.ToString("yyyy-MM-dd HH:mm"), 2),
            new NoteResponse(6, "TestTitle6", "TestContent6", DateTime.Now.ToString("yyyy-MM-dd HH:mm"), 3)
        };

        return notes.Where(x => x.Status.Equals(status)).ToList();
    }

    private ICollection<NoteEntity> _entities = new List<NoteEntity>()
    {
        new NoteEntity
        {
            Id = 1, Title = "TestTitle", Description = "TestContent", ChangeTime = DateTime.Now, Status = Status.InWork
        },
        new NoteEntity
        {
            Id = 2, Title = "TestTitle2", Description = "TestContent2", ChangeTime = DateTime.Now,
            Status = Status.InWork
        },
        new NoteEntity
        {
            Id=3,Title = "TestTitle3",Description = "TestContent3",ChangeTime = DateTime.Now,
            Status = Status.InWork
        },
    };
}