﻿using FreeCourse.Web.Models;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
