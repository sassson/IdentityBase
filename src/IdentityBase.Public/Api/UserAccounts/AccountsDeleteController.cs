// Copyright (c) Russlan Akiev. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace IdentityBase.Public.Api.UserAccounts
{
    using System;
    using System.Threading.Tasks;
    using IdentityBase.Models;
    using IdentityBase.Services;
    using IdentityServer4.AccessTokenValidation;
    using Microsoft.AspNetCore.Mvc;
    using ServiceBase.Authorization;
    using ServiceBase.Mvc;

    [TypeFilter(typeof(ExceptionFilter))]
    [TypeFilter(typeof(ModelStateFilter))]
    [TypeFilter(typeof(BadRequestFilter))]
    public class UserAccountDeleteController : ApiController
    {
        private readonly UserAccountService _userAccountService;

        public UserAccountDeleteController(
            UserAccountService userAccountService)
        {
            this._userAccountService = userAccountService;
        }
        
        [HttpDelete("useraccounts/{UserAccountId}")]
        [ScopeAuthorize("idbase", AuthenticationSchemes =
            IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete([FromRoute]Guid userAccountId)
        {
            UserAccount userAccount = await this._userAccountService
                .LoadByIdAsync(userAccountId);

            if (userAccount == null)
            {
                return this.NotFound();
            }
            
            await this._userAccountService.DeleteByIdAsync(userAccountId);

            return this.Ok(); 
        }
    }
}