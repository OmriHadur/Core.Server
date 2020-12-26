using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Server.Common.Helpers
{
    public interface IPasswordHasher
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
