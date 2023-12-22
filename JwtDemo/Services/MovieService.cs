﻿using JwtDemo.Models;
using JwtDemo.Repositories;

namespace JwtDemo.Services;

public class MovieService : IMovieService
{
    public Movie Create(Movie movie)
    {
        movie.Id = MovieRepository.Movies.Count + 1;
        MovieRepository.Movies.Add(movie);
        return movie;
    }

    public Movie GetMovie(int id)
    {
        var movie = MovieRepository.Movies.FirstOrDefault(o => o.Id == id);
        if (movie == null) return null;
        return movie;
    }

    public List<Movie> List()
    {

        var movies = MovieRepository.Movies;
        return movies;
        
    }

    public Movie Update(Movie movie)
    {
        var oldMovie = MovieRepository.Movies.FirstOrDefault(o => o.Id == movie.Id);
        if (oldMovie == null) return null;
        oldMovie.Title = movie.Title;
        oldMovie.Description = movie.Description;
        oldMovie.Rating = movie.Rating;
        return movie;

    }

    public bool Delete(int id)
    {
        var movie = MovieRepository.Movies.FirstOrDefault(o => o.Id == id);
        if (movie == null) return false;
        MovieRepository.Movies.Remove(movie);
        return true;

    }
}