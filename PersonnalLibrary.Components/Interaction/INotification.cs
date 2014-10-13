
namespace PersonnalLibrary.Interaction
{
    public interface INotification
    {
        string Title { get; set; }

        object Content { get; set; }
    }
}
