using Shared.DataTransfareObject.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        //Login
        //Take Email and Password Then Return Token ,  Email and DisplayName

        Task<UserDTO> LoginAsync(LoginDTO loginDTO);

        //Registration
        //Take Email , Password  , UserName , Display Name And Phone Number Then Return Token , Email and Display Name 

        Task<UserDTO> RegisterAsync (RegisterDTO registerDTO);

        //Check Email 
        //Take Email return Bool
        Task<bool> CheckEmailAsync (string email);

        //Get Current User Address 
        //take Email return AddressDTO

        Task<AddressDTO>GetCurrentUserAddressAsync(string email);

        //Update User Address 
        Task<AddressDTO> UpdateCurrentUserAddressAsync(string email ,AddressDTO addressDTO);

        //Get Current User 

        Task<UserDTO> GetCurrentUserAsync(string email); 

    }
}
