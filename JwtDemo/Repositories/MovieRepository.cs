using JwtDemo.Models;

namespace JwtDemo.Repositories;

public class MovieRepository
{
    public static List<Movie> Movies = new List<Movie>()
    {
        new Movie()
        {
            Id = 1,
            Description = "A SuperHero Movie",
            Title = "Avengers",
            Rating = 2.5
        },
        new Movie()
        {
            Id = 2,
            Description = "A SuperHero Movie",
            Title = "Avengers 2",
            Rating = 3.5   
        },
        new Movie()
        {
            Id = 3,
            Description = "A SuperHero Movie",
            Title = "Batman",
            Rating = 3.5
        }
    };
}