using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Tamro.Common.Interfaces;
using Tamro.Entities;
using Tamro.Web.ApiModels;
using Microsoft.EntityFrameworkCore;
using Tamro.Web.Misc;

namespace Tamro.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<UserModel>>> Users()
        {
            var result = await _userService.Load();
            return _mapper.Map<List<UserModel>>(result);
        }

        [HttpPost]
        [ValidateModel]
        [Produces("application/json")]
        public async Task<IActionResult> Create([FromBody] UserModel data)
        {
            if (!ModelState.IsValid) return BadRequest();
            var entity = _mapper.Map<UserEntity>(data);

            if (data.Id == 0)
            {
                await _userService.Create(entity);
            }

            else
            {
                var doesExist = await _userService.Get(data.Id);
                if (doesExist == null)
                {
                    return BadRequest("Requested tax class ID does not exist in database.");
                }

                entity.Id = doesExist.Id;
                await _userService.Update(entity);
            }

            return Ok(entity.Id);
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(int id)
        {
            var doesExist = await _userService.Get(id);
            if (doesExist == null)
            {
                return NotFound("Requested tax class ID does not exist in database.");
            }


            await _userService.Delete(doesExist);
            return Ok();
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.Get(id);
            if (user == null) return NotFound();

            var result = _mapper.Map<UserModel>(user);
            return Ok(result);
        }

        [HttpGet("GetByEmail/{email}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userService.GetByEmail(email);
            if (user == null) return NotFound();
            var result = _mapper.Map<UserModel>(user);
            return Ok(result);
        }
    }
}
