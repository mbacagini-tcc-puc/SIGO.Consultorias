﻿using Microsoft.IdentityModel.Tokens;
using System;

namespace SIGO.Consultorias.API.Auth
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecurityKey { get; set; }

        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Convert.FromBase64String(SecurityKey));
        public SigningCredentials SigningCredentials => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
    }
}
