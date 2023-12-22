using JwtDemo.Models;

namespace JwtDemo.Services;

public interface IMovieService
{
    public Movie Create(Movie movie);
    public Movie GetMovie(int id);
    public List<Movie> List();
    public Movie Update(Movie movie);
    public bool Delete(int id);
}