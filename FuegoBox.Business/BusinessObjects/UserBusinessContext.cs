using FuegoBox.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuegoBox.Shared.DTO.User;
using FuegoBox.DAL.DBObjects;
using AutoMapper;
using FuegoBox.Business.Exceptions;
using FuegoBox.DAL.Exceptions;

namespace FuegoBox.Business.BusinessObjects
{
    public class UserBusinessContext
    {
        UserDetail UserDBObject;
        IMapper mapper;
        public UserBusinessContext()
        {
            UserDBObject = new UserDetail();
        }
        public UserBasicDTO RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                if (UserDBObject.UserEmailExists(userRegisterDTO.Email))
                {
                    throw new UserNameAlreadyExistsException();
                }
            }
            catch (NotFoundException ex)
            {
                userRegisterDTO.HashPassword = PasswordHasher.PasswordHashing(userRegisterDTO.Password);
                UserBasicDTO newuserBasicDTO = UserDBObject.AddUser(userRegisterDTO);
                return newuserBasicDTO;
            }
            catch (UserNameAlreadyExistsException ex)
            {
                throw new UserNameAlreadyExistsException();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
           

            return null;
        }
    }

}
