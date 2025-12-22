namespace MovieReservation.Business.Service;

public class MovieService: IMovieService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IVisitor _visitor;
    
    public MovieService(IUnitOfWork unitOfWork ,IVisitor visitor,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _visitor = visitor;
    }
    
    public async Task CreateAsync(MovieModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);
        
        var movie = (await _unitOfWork.Movies.SearchAsync(new SearchContext<Movie>
        {
            Filter = x => x.Title == request.Title
        }, ct)).Results.FirstOrDefault();
        
        if (movie != null)
        {
            throw new DuplicateFoundException("Movie is already exists");
        }

        var genre = await GetGenreById(request.GenreId, ct);

        if (genre == null)
        {
            throw new NotFoundException("Genre not found");
        }
        
        _unitOfWork.Movies.Add(_mapper.Map<Movie>(request));

        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task UpdateAsync(MovieModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);
        
        var movie = (await _unitOfWork.Movies.SearchAsync(new SearchContext<Movie>
        {
            Filter = x => x.Id == request.Id || x.Title == request.Title
        }, ct)).Results.FirstOrDefault();
        
        if (movie == null)
        {
            throw new NotFoundException("Role is not found");
        }
        
        var genre = await GetGenreById(request.GenreId, ct);

        if (genre == null)
        {
            throw new NotFoundException("Genre not found");
        }

        var newMovie = _mapper.Map<Movie>(request);
        
        newMovie.UpdatedAt = DateTime.UtcNow;
        
        _unitOfWork.Movies.Update(newMovie.Id, newMovie);

        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task<MovieModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id, ct);

        return _mapper.Map<MovieModel>(movie);
    }

    public async Task<List<MovieModel>> GetAllAsync()
    {
        var movies = await _unitOfWork.Movies.SearchAsync(new SearchContext<Movie>());
        
        return _mapper.Map<List<MovieModel>>(movies.Results);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var movie = await _unitOfWork.Movies.GetByIdAsync(id, ct);
        
        if (movie == null)
        {
            throw new NotFoundException("Movie does not exists");
        }

        _unitOfWork.Movies.Delete(movie);
        await _unitOfWork.CompleteAsync(ct);
    }

    private async Task<GenreModel?> GetGenreById(Guid id, CancellationToken ct = default)
    {
        var genre = (await _unitOfWork.MovieGenres.SearchAsync(new SearchContext<MovieGenre>
        {
            Filter = x => x.Id == id
        }, ct)).Results.FirstOrDefault();
        
        return _mapper.Map<GenreModel>(genre);
    }
}