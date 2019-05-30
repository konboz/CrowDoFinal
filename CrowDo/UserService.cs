using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CrowDo
{
    public class UserService : IUserService
    {
        public Result<User> Create(string email, string name, string address, DateTime birthDate)
        {
            var context = new CrowDoDbContext();

            var result = new Result<User>();

            //Validations
            if (IsValidEmail(email) == false)
            {
                result.ErrorCode = 10;
                result.ErrorText = "Invalid email";
                return result;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                result.ErrorCode = 11;
                result.ErrorText = "Invalid name";
                return result;
            }

            if (birthDate.AddYears(18) > DateTime.Now)
            {
                result.ErrorCode = 12;
                result.ErrorText = "Not permited";
                return result;
            }

            var existingEmail = context.Set<User>().Where(m => m.Email == email).Any(); // returns bool //

            if (existingEmail == true)
            {
                result.ErrorCode = 13;
                result.ErrorText = "An account with the same email already exists";
                return result;
            }

            var user = new User()
            {
                Email = email,
                Name = name,
                Address = address,
                BirthDate = birthDate,
                RegistrationDate = DateTime.Now
            };

            context.Add(user);

            if (context.SaveChanges() < 1)  // validation for Savechanges
            {
                result.ErrorCode = 13;
                result.ErrorText = "An error occured while saving data";
                return result;
            }
            result.Data = user;
            return result;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            if (!email.Contains("@"))
            {
                return false;
            }

            return true;
        }

        public Result<bool> Delete(string email)
        {
            var context = new CrowDoDbContext();

            var result = new Result<bool>();

            var user = context.Set<User>()
                .Include(u => u.CreatedProjects)
                .SingleOrDefault(u => u.Email == email);

            if (IsValidEmail(email) == false)
            {
                result.ErrorCode = 14;
                result.ErrorText = "Invalid email";
                return result;
            }
            
            context.Remove(user);

            if (context.SaveChanges() < 1)
            {
                result.ErrorCode = 15;
                result.ErrorText = "An error occured while saving data";
                return result;
            }

            result.Data = true;
            return result;
        }

        public Result<bool> Edit(string email, string newName, string newAddress, DateTime newBirthDate)
        {
            var result = new Result<bool>();
            var context = new CrowDoDbContext();
            var updatedUser = context.Set<User>()
                .SingleOrDefault(u => u.Email == email);

            if (IsValidEmail(email) == false)
            {
                result.ErrorCode = 16;
                result.ErrorText = "Invalid email";
                return result;
            }

            updatedUser.Name = newName;
            updatedUser.Address = newAddress;
            updatedUser.BirthDate = newBirthDate;
          
            if (context.SaveChanges() < 1)
            {
                result.ErrorCode = 15;
                result.ErrorText = "An error occured while saving data";
                return result;
            }

            result.Data = true;
            return result;

        }

    }
}
