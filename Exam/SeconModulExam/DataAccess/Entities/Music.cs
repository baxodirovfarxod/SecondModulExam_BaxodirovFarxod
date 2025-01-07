namespace SeconModulExam.DataAccess.Entities;
public class Music
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double MB { get; set; }
    public string AuthorName { get; set; }
    public int QuentityLikes { get; set; }
}
