﻿using FreeCourse.Web.Models.PhotoStock;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
	public interface IPhotoStockService
	{
		Task<PhotoViewModel> UploadPhoto(IFormFile photo);
		Task<bool> DeletePhoto(string photoUrl);
	}
}
