using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtDemo.Models;
using JwtDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using JwtDemo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;


namespace JwtDemo.Controllers;
[ApiController]
[Route("[Controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _service;
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public MoviesController(IMovieService service, IUserService userService, IConfiguration configuration)
    {
        _service = service;
        _userService = userService;
        _configuration = configuration;
    }
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    [HttpPost("/create")]
    public IResult Post([FromBody] Movie movie)
    {
        return Create(movie, _service);
    }

    IResult Create(Movie movie, IMovieService service)
    {
        var result = service.Create(movie);
        return Results.Ok(result);
    }

    [HttpPost("/login")]
    public IResult LoginAuth(UserLogin user)
    {
       return Login(user, _userService);
    }

    IResult Login(UserLogin user, IUserService service)
    {
        if (!string.IsNullOrEmpty(user.Username) && !string.IsNullOrEmpty(user.Password))
        {
            var loggedInUser = service.Get(user);
            if (loggedInUser == null) return Results.NotFound("User Not Found");
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loggedInUser.Username),
                new Claim(ClaimTypes.Email, loggedInUser.EmailAddress),
                new Claim(ClaimTypes.GivenName, loggedInUser.GivenName),
                new Claim(ClaimTypes.Surname, loggedInUser.Surname),
                new Claim(ClaimTypes.Role, loggedInUser.Role)
            };
            var token = new JwtSecurityToken(  
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256));
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Results.Ok(tokenString);
        }
        return Results.BadRequest("Invalid user credentials");
    }

    [HttpGet("/get")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")]
    public IResult GetSingleMovie(int id)
    {
        return GetMovie(id);
    }

    private IResult GetMovie(int id)
    {
        var movie = _service.GetMovie(id);
        if (movie == null) return Results.NotFound("Movie Not Found");
        return Results.Ok(movie);
    }

    [HttpGet("/list")]
    public IResult GetList()

    {
        return List(_service);
    }

    IResult List(IMovieService service)
    {
        var movies = service.List();
        return Results.Ok(movies);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

    public IResult UpdateMovie(Movie movie)
    {
        return Update(movie, _service);
    }

    IResult Update(Movie movie, IMovieService service)
    {
        var updatedMovie = service.Update(movie);
        if (updatedMovie == null) return Results.NotFound("Movie Not Found");
        return Results.Ok(updatedMovie);
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

    public IResult DeleteMovie(int id)
    {

        return Delete(id, _service);

    }

    private IResult Delete(int id, IMovieService service)
    {
        
        var result = service.Delete(id);
        if (!result) Results.BadRequest("Something went wrong");
        return Results.Ok(result);
    }
}