using AuthorizationAndAuthentication.Services.Abstraction.Contracts;
using AuthorizationAndAuthentication.Services.Abstraction.RequestModels;
using AuthorizationAndAuthentication.Services.Abstraction.ResponseModels;
using AuthorizationAndAuthentication.Common.Generics;
using AuthorizationAndAuthentication.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizationAndAuthentication.Repositories.Abstraction.Contracts;
using AuthorizationAndAuthentication.Common.Extensions;
using AuthorizationAndAuthentication.Common.Constants;
using System.Net;
using AuthorizationAndAuthentication.Repositories.Context.Entites;

namespace AuthorizationAndAuthentication.Services.Implementation.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public async Task<IGenericResponse<User>> GetUserByEmailAndPassword(LoginRequest model)
        {
            var response = new GenericResponse<User>();
            try
            {
                #region Validations
                if (model == null || !model.Email.IsValidEmail() && !model.Password.IsValidString())
                {
                    response.SetError(SystemMessages.CHECK_EMAIL_PASSWORD, HttpStatusCode.OK);
                    return response;
                }
                #endregion
                var User = await _userRepository.GetUserByEmailAndPassword(model.Email, model.Password);
                if (User == null || User.Count == 0)
                {
                    response.SetError(SystemMessages.NO_DATA_FOUND, HttpStatusCode.NoContent);
                    return response;
                }
                response.Data = AssignToSubUser(User);
                response.SetSuccess(SystemMessages.SUCCESS, HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is ApplicationErrorException) throw;
                else throw new ApplicationErrorException(ex);
            }
        }
        public async Task<IGenericResponse<UserListResponse>> GetUserListWithPagination(int pageNumber, int pageSize)
        {
            var response = new GenericResponse<UserListResponse>();
            try
            {
                List<UserListModel> users = await _userRepository.GetUserListWithPagination(pageNumber, pageSize);

                if (users == null)
                {
                    response.SetSuccess(SystemMessages.NO_DATA_FOUND, HttpStatusCode.NoContent);
                    return response;
                }
                response.Data = new UserListResponse();
                response.Data.List = AssignToUsersList(users);
                response.Data.PageNumber = pageNumber;
                response.Data.PageSize = pageSize;
                response.Data.TotalCount = (users == null || users.Count() == 0) ? 0 : users.FirstOrDefault() == null ? 0 : users.First().Count;
                response.Data.ResultCount = response.Data.List == null ? 0 : response.Data.List.Count();
                response.SetSuccess(SystemMessages.SUCCESS, HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is ApplicationErrorException) throw;
                else throw new ApplicationErrorException(ex);
            }
        }
        public async Task<IGenericResponse<DeleteResponse>> DeleteUser(int userId,int LoginUserId)
        {
            var response = new GenericResponse<DeleteResponse>();
            try
            {
                response.Data = new DeleteResponse();
                var result = await _userRepository.DeleteUserByUserId(userId, LoginUserId);
                if (result == null)
                {
                    response.SetError(SystemMessages.ERROR, HttpStatusCode.BadRequest);
                    return response;
                }
                if (result.ErrorNo > 0)
                {
                    string message = ConstantMessages.messages.ContainsKey(result.ErrorNo) ? ConstantMessages.messages[result.ErrorNo] : "";
                    response.SetError(message, HttpStatusCode.BadRequest);
                    return response;
                }
                response.Data.IsDeleted = true;
                response.SetSuccess(SystemMessages.ACCOUNT_DELETED, HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is ApplicationErrorException) throw;
                else throw new ApplicationErrorException(ex);
            }
        }
        #region Private Methods
        private User AssignToSubUser(List<UserModel> model)
        {
            User user = new User();
            var entity = model.FirstOrDefault();
            if (entity == null) return user;
            user.UserId = entity.UserId;
            user.Name = entity.Name;
            user.Email = entity.Email;
            user.Status = entity.Status;
            user.IsActive = entity.IsActive;
            user.CreatedBy = entity.CreatedBy;
            user.CreatedDate = entity.CreatedDate;
            user.Roles = GetSubUserRoles(model);
            return user;
        }
        private List<UserRole> GetSubUserRoles(List<UserModel> modelList)
        {
            List<UserRole> entityList = new List<UserRole>();

            List<List<UserModel>> groupedModelList = modelList
                                                .GroupBy(b => new { b.RoleId })
                                                .Select(grp => grp.ToList())
                                                .ToList();

            var exceptions = new System.Collections.Concurrent.ConcurrentQueue<Exception>();
            Parallel.ForEach((groupedModelList), modelValue =>
            {
                try
                {
                    UserRole entity = new UserRole();
                    UserModel role = modelValue.First();
                    entity.RoleId = role.RoleId;
                    entity.RoleName = role.RoleName;
                    entity.RoleDescription = role.RoleDescription;
                    entity.Status = role.StatusUserRole;
                    entity.IsActive = role.IsActiveUserRole;
                    entity.UserModuleAccesses = GetSubUserModule(modelValue);

                    lock (entityList)
                        entityList.Add(entity);
                }
                catch (Exception ex)
                {
                    exceptions.Enqueue(ex);
                }
            });
            return entityList;
        }
        private HashSet<UserModuleAccess> GetSubUserModule(List<UserModel> models)
        {
            HashSet<UserModuleAccess> entityList = new HashSet<UserModuleAccess>(new UserModuleAccessComparer());
            UserModuleAccess UserModuleAccess = null;
            foreach (var model in models)
            {
                UserModuleAccess = new UserModuleAccess();
                UserModuleAccess.AccessLevel = model.AccessLevel;
                UserModuleAccess.UserModuleId = model.ModuleId;
                UserModuleAccess.UserModuleAccessId = model.UserModuleAccessId;
                UserModuleAccess.UserModuleId = model.ModuleId;
                UserModuleAccess.UserRoleId = model.RoleId;

                UserModuleAccess.UserModule = new UserModule()
                {
                    ModuleName = model.ModuleName,
                    UserModuleId = model.ModuleId
                };
                entityList.Add(UserModuleAccess);
            }
            return entityList;
        }
        private List<UserList> AssignToUsersList(List<UserListModel> modelList)
        {
            List<UserList> entityList = new List<UserList>();

            List<List<UserListModel>> groupedModelList = modelList
                                                .GroupBy(b => new { b.UserId })
                                                .Select(grp => grp.ToList())
                                                .ToList();
            var exceptions = new System.Collections.Concurrent.ConcurrentQueue<Exception>();
            Parallel.ForEach((groupedModelList), modelValue =>
            {
                try
                {
                    UserList entity = new UserList();                    
                    UserListModel model = modelValue.FirstOrDefault();
                    if (model == null) return;
                    entity.UserId = model.UserId;
                    entity.Name = model.Name;
                    entity.Email = model.Email;
                    entity.CreatedDate = model.CreatedDate;
                    entity.IsActive = model.IsActive;
                    entity.Status = model.Status;
                    entity.CreatedBy = model.CreatedBy;
                    entity.Roles = GetRolesForList(modelValue);

                    lock (entityList)
                        entityList.Add(entity);
                }
                catch (Exception ex)
                {
                    exceptions.Enqueue(ex);
                }

            });

            return entityList?.ToList();
        }
        private List<Role> GetRolesForList(List<UserListModel> models)
        {
            List<Role> roles = new List<Role>();
            Role role;
            foreach (var model in models)
            {
                role = new Role();
                role.RoleId = model.RoleId;
                role.RoleName = model.RoleName;
                roles.Add(role);
            }
            return roles;
        }
        #endregion
    }
}
