namespace MovieReservation.Business.Interface;

public interface IUserService: IBaseService<UserModel, UserModel>
{
    Task<AuthorizationModel> Login(UserModel request, CancellationToken ct = default);
    Task CheckAccesses(CheckAccessModel request, CancellationToken ct = default);
}