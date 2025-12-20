namespace MovieReservation.Business.Service;

public class GenreService: IGenreService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IVisitor _visitor;
    
    public GenreService(IUnitOfWork unitOfWork, IVisitor visitor ,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _visitor = visitor;
    }
    
    public async Task CreateAsync(GenreModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);
        
        var genre = (await _unitOfWork.MovieGenres.SearchAsync(new SearchContext<MovieGenre>
        {
            Filter = x => x.Name == request.Name
        }, ct)).Results.FirstOrDefault();
        
        if (genre != null)
        {
            throw new DuplicateFoundException("Genre is already exists");
        }
        
        _unitOfWork.MovieGenres.Add(_mapper.Map<MovieGenre>(request));

        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task UpdateAsync(GenreModel request, CancellationToken ct = default)
    {
        _visitor.Visit(request);
        
        var genre = (await _unitOfWork.MovieGenres.SearchAsync(new SearchContext<MovieGenre>
        {
            Filter = x => x.Id == request.Id || x.Name == request.Name
        }, ct)).Results.FirstOrDefault();
        
        if (genre == null)
        {
            throw new NotFoundException("Role is not found");
        }

        var newGenre = _mapper.Map<MovieGenre>(request);
        
        newGenre.UpdatedAt = DateTime.UtcNow;
        
        _unitOfWork.MovieGenres.Update(newGenre.Id, newGenre);

        await _unitOfWork.CompleteAsync(ct);
    }

    public async Task<GenreModel> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var genre = await _unitOfWork.MovieGenres.GetByIdAsync(id, ct);

        return _mapper.Map<GenreModel>(genre);
    }

    public  async Task<List<GenreModel>> GetAllAsync()
    {
        var genres = await _unitOfWork.MovieGenres.SearchAsync(new SearchContext<MovieGenre>());
        
        return _mapper.Map<List<GenreModel>>(genres.Results);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var genre = await _unitOfWork.MovieGenres.GetByIdAsync(id, ct);
        
        if (genre == null)
        {
            throw new NotFoundException("Genre does not exists");
        }

        _unitOfWork.MovieGenres.Delete(genre);
        await _unitOfWork.CompleteAsync(ct);
    }
}