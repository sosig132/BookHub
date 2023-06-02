namespace Back_End.Models.BaseEntity
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime? DateCreated { get; set; }
        DateTime? DateModified { get; set; }

    }
}
