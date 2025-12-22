namespace MovieReservation.Business.Service;

public class SessionService: ISessionService
{
    private readonly ITicketService _ticketService;
    private readonly IMovieService _movieService;
    private readonly IVisitor _visitor;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SessionService(ITicketService ticketService, IMovieService movieService, IVisitor visitor, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _ticketService = ticketService;
        _movieService = movieService;
        _visitor = visitor;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task CreateAsync(SessionModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);

        var movie = await _movieService.GetByIdAsync(request.MovieId, ct);
        if (movie == null)
            throw new NotFoundException("Movie does not exist.");

        if (movie.Duration <= TimeSpan.Zero)
            throw new ArgumentException("Movie duration is invalid.");

        var endTime = request.ShowTime.Add(movie.Duration);

        // 4. Check room availability (time overlap)
        var overlappingSessionsCount =
            await _unitOfWork.Sessions.GetFilteredCountAsync(
                s =>
                    s.RoomId == request.RoomId &&
                    s.Status != SessionStatusEnum.Cancelled &&
                    s.ShowTime < endTime &&
                    s.EndTime > request.ShowTime,
                ct);

        if (overlappingSessionsCount > 0)
            throw new BusinessException("Room is already booked for the selected time slot.");

        var session = _mapper.Map<Session>(request);
        
        session.EndTime = endTime;
        
        _unitOfWork.Sessions.Add(session);
        
        request.Id = session.Id;

        await _ticketService.GenerateTicketsAsync(request, ct);

        await _unitOfWork.CompleteAsync(ct);
    }

    public Task UpdateAsync(SessionModel request, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<SessionModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<SessionModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<SessionModel>> GetAllByDateAsync(DateTime date, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<SessionModel> GetByMovieIdAsync(Guid movieId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}