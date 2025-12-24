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

        var endTime = request.ShowTime.Add(movie.Duration);

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

    public async Task UpdateAsync(SessionModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);
        
        var session = (await _unitOfWork.Sessions.SearchAsync(new SearchContext<Session>
        {
            Filter = x => x.Id == request.Id
        }, ct)).Results.FirstOrDefault();
        
        if (session == null)
            throw new NotFoundException("Role is not found");
        
        var movie = await _movieService.GetByIdAsync(request.MovieId, ct);
        
        var endTime = request.ShowTime.Add(movie.Duration);

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

        var updatedSession = _mapper.Map<Session>(request);
        
        updatedSession.EndTime = endTime;
        
        _unitOfWork.Sessions.Update(request.Id, updatedSession);
        
        await _ticketService.GenerateTicketsAsync(request, ct);

        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task<SessionModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var session = await _unitOfWork.Sessions.GetByIdAsync(id, ct);
        
        return _mapper.Map<SessionModel>(session);
    }

    public async Task<List<SessionModel>> GetAllAsync()
    {
        var sessions = await _unitOfWork.Sessions.SearchAsync(new SearchContext<Session>());
        
        return _mapper.Map<List<SessionModel>>(sessions.Results);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var session = await _unitOfWork.Sessions.GetByIdAsync(id, ct);
        
        if (session == null)
            throw new NotFoundException("Session does not exists");

        _unitOfWork.Sessions.Delete(session);
        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task<List<SessionModel>> GetAllByDateAsync(DateTime date, CancellationToken ct = default)
    {
        var sessions = await _unitOfWork.Sessions.SearchAsync(new SearchContext<Session>
        {
            Filter = s => s.ShowTime.Day == date.Day
        }, ct);
        
        return _mapper.Map<List<SessionModel>>(sessions);
    }

    public async Task<SessionModel> GetByMovieIdAsync(Guid movieId, CancellationToken ct = default)
    {
        var session = (await _unitOfWork.Sessions.SearchAsync(new SearchContext<Session>
        {
            Filter = s => s.MovieId == movieId
        }, ct)).Results.FirstOrDefault();
        
        if(session == null)
            throw new NotFoundException("Session does not exists");
        
        return _mapper.Map<SessionModel>(session);
    }
}