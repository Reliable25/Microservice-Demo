namespace UserService.Application.Interfaces
{
    public interface IUserEventPublisher
    {
        Task PublishUserLoggedInEvent(int userId);
    }
}
