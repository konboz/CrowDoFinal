using System;
using System.Collections.Generic;
using System.Text;

namespace CrowDo
{
    public interface IUserService
    {
        Result<User> Create(string email, string name, string address, DateTime birthDate);
        Result<bool> Edit(string email, string newName, string newAddress, DateTime newBirthDate);
        Result<bool> Delete(string email);
    }
}
