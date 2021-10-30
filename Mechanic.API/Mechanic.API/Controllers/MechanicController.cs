using Mechanic.API.Helpers;
using Mechanic.API.Models;
using Mechanic.Contracts.Services;
using Mechanic.Models.Mechanic;
using Mechanic.ViewModels.Mechanic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Mechanic.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MechanicController : ControllerBase
    {
        private readonly IMechanicService _mechanicService;
        public MechanicController(IMechanicService mechanicService)
        {
            _mechanicService = mechanicService;
        }

        //[HttpGet("MechanicType")]
        //public async Task<ResponseMetaData<IEnumerable<MechanicTypeViewModel>>> GetAllMechanicType()
        //{
        //    var result = await _mechanicService.GetAllMechanicType();
        //    return ResponseMetaData<IEnumerable<MechanicTypeViewModel>>.CreateResponse(HttpStatusCode.OK, result);
        //}

        [HttpGet("Genres")]
        public async Task<ResponseMetaData<IEnumerable<GenresViewModel>>> GetAllGenres()
        {
            var result = await _mechanicService.GetAllGenres();
            return ResponseMetaData<IEnumerable<GenresViewModel>>.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost("AddGenres")]
        public async Task<IActionResult> Create([FromBody] Genres model)
        {
            var result = await _mechanicService.AddGenres(model);
            if (result.ID > 0)
                return Ok(new Response(message: "Genres Created", data: result));

            return BadRequest(new Response(message: "Genres creation failed!"));
        }
        [HttpPost("UpdateGenres")]
        public async Task<IActionResult> Update([FromBody] Genres model)
        {
            var result = await _mechanicService.UpdateGenres(model);
            if (result.ID > 0)
                return Ok(new Response(message: "Genres Updated", data: result));

            return BadRequest(new Response(message: "Genres updation failed!"));
        }
        [HttpPost("DeleteGenres")]
        public async Task<IActionResult> Delete([FromBody] Genres model)
        {
            var result = await _mechanicService.UpdateGenres(model);
            if (result.ID > 0)
                return Ok(new Response(message: "Genres Deleted", data: result));

            return BadRequest(new Response(message: "Genres Deletion failed!"));
        }

        [HttpGet("Movies")]
        public async Task<ResponseMetaData<IEnumerable<MoviesViewModel>>> GetAllMovies()
        {
            var result = await _mechanicService.GetAllMovies();
            return ResponseMetaData<IEnumerable<MoviesViewModel>>.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost("AddMovies")]
        public async Task<IActionResult> CreateMovies([FromBody] Movies model)
        {
            var result = await _mechanicService.AddMovies(model);
            if (result.ID > 0)
                return Ok(new Response(message: "Movies Created", data: result));

            return BadRequest(new Response(message: "Movies creation failed!"));
        }
        [HttpPost("UpdateMovies")]
        public async Task<IActionResult> UpdateMovies([FromBody] Movies model)
        {
            var result = await _mechanicService.UpdateMovies(model);
            if (result.ID > 0)
                return Ok(new Response(message: "Movies Updated", data: result));

            return BadRequest(new Response(message: "Movies updation failed!"));
        }
        [HttpPost("DeleteMovies")]
        public async Task<IActionResult> DeleteMovies([FromBody] Movies model)
        {
            var result = await _mechanicService.DeleteMovies(model);
            if (result.ID > 0)
                return Ok(new Response(message: "Movies Deleted", data: result));

            return BadRequest(new Response(message: "Movies Deletion failed!"));
        }
    }
}
