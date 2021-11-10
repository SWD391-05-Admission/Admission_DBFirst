using Admission.Data.Models;
using Admission.Data.Repository;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public interface ILoginService
    {
        Role GetRole(int? roleId);
        Task<UserRecord> GetUser(string firebaseToken);
        User GetUser(UserRecord userRecord);
        Task<User> CreateUser(UserRecord userRecord, string app);
        string GenerateJWT(User localUserModel);
    }

    public class LoginService : ILoginService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _iUserRepository;
        private readonly IRoleRepository _iRoleRepository;
        private readonly ICounselorRepository _iCounselorRepository;
        private readonly IStudentRepository _iStudentRepository;
        private readonly IWalletRepository _iWalletRepository;

        public LoginService(IConfiguration config, IUserRepository iUserRepository, IRoleRepository iRoleRepository, ICounselorRepository iCounselorRepository, IStudentRepository iStudentRepository, IWalletRepository iWalletRepository)
        {
            _config = config;
            _iUserRepository = iUserRepository;
            _iRoleRepository = iRoleRepository;
            _iCounselorRepository = iCounselorRepository;
            _iStudentRepository = iStudentRepository;
            _iWalletRepository = iWalletRepository;
        }

        public Role GetRole(int? roleId)
        {
            return _iRoleRepository.GetRole(roleId);
        }

        public async Task<UserRecord> GetUser(string firebaseToken)
        {
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(firebaseToken);
            string uid = decodedToken.Uid;
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);

            if (userRecord != null) return userRecord;
            return null;
        }

        public User GetUser(UserRecord userRecord)
        {
            return _iUserRepository.GetUser(userRecord.Email);
        }

        public async Task<User> CreateUser(UserRecord userRecord, string app)
        {
            var newUser = new User
            {
                Email = userRecord.Email,
                IsActive = true,
            };
            int roleId = _iRoleRepository.GetRoleIdByRolename(app);
            switch (app)
            {
                case "Counselor":
                    newUser.RoleId = roleId;
                    if (await _iUserRepository.InsertUser(newUser))
                    {
                        User user = _iUserRepository.GetUser(userRecord.Email);
                        if (user != null)
                        {
                            Counselor counselor = new()
                            {
                                Id = user.Id,
                                FullName = userRecord.DisplayName,
                                Phone = userRecord.PhoneNumber,
                                Avatar = userRecord.PhotoUrl
                            };
                            if (await _iCounselorRepository.InsertCounselor(counselor)) return user;
                        }
                    }
                    break;
                case "Student":
                    newUser.RoleId = roleId;
                    if (await _iUserRepository.InsertUser(newUser))
                    {
                        User user = _iUserRepository.GetUser(userRecord.Email);
                        if (user != null)
                        {
                            Student student = new()
                            {
                                Id = user.Id,
                                FullName = userRecord.DisplayName,
                                Phone = userRecord.PhoneNumber,
                                Avatar = userRecord.PhotoUrl
                            };
                            if (await _iStudentRepository.InsertStudent(student))
                            {
                                Wallet wallet = new()
                                {
                                    Amount = 100,
                                    StudentId = user.Id
                                };
                                if (await _iWalletRepository.InsertWallet(wallet)) return user;
                            }
                        }
                    }
                    break;
            }

            return null;
        }

        public string GenerateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, _iRoleRepository.GetRole(user.RoleId).RoleName)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(90),
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
    }
}
