using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransfareObject.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) :ApiBaseController
    {
        //Login 
        [HttpPost ("Login")] //POST: BaseURL/api/Authentication/Login
        public async Task<ActionResult<UserDTO>> Login (LoginDTO loginDTO)
        {
            var User =await _serviceManager.AuthenticationService.LoginAsync(loginDTO);
            return Ok (User);
        }

        //Register
        [HttpPost ("Register")]//POST: BaseURL/api/Authentication/Register 
        public async Task<ActionResult<UserDTO>> Register (RegisterDTO registerDTO)
        {
            var User =await  _serviceManager.AuthenticationService.RegisterAsync(registerDTO);
            return Ok(User);
        }


        //Check Email 
        [HttpGet ("emailexists")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Res=await _serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(Res);
        }

        //Get Current User 
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>>GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var AppUser=await _serviceManager.AuthenticationService.GetCurrentUserAsync(email!);
            return Ok(AppUser);
        }
        //Gert Current User Address
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>>GetCurrentAddressUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Address=await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(email!);
            return Ok(Address);
        }

        //Updata Address 
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTO>>UpdateUserAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Address = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(email,addressDTO);
            return Ok(Address);
        }
    }
}
