using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OwlBudget.Models;

namespace OwlBudget.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var identity = await GetIdentityAsync(loginRequest.User, loginRequest.Password);
        if (identity == null) return BadRequest(new {errorText = "Invalid username or password."});

        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            AuthOptions.Issuer,
            AuthOptions.Audience,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            token = encodedJwt,
            user = new {identity.Name}
        };

        return Ok(response);
    }

    private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
    {
        var user = await _userService.GetUserAsync(username, password);
        if (user == null) return null;
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
            new("Id", $"{user.Id}")
        };
        var claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        return claimsIdentity;
    }
}