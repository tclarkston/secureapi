using System;
using WebApi.Entities;

namespace WebApi.Services.Interfaces
{
    public interface IUserService
    {
        Token Authenticate(string username, string password);
    }
}
