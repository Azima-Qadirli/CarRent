namespace CarRent.Views.Models.BaseEntities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}