using Mapster;
using SampleCommerce.Common;
using SampleCommerce.DTOs.Users;
using SampleCommerce.Models;
using SampleCommerce.Repositories;
using System.Security.Cryptography;

namespace SampleCommerce.Services
{
    public class UserService : IService<Guid, DtoUserResponse, DtoUserCreateRequest, DtoUserUpdateRequest>
    {
        private readonly UserRepo _userRepo;
        public UserService(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public Result<DtoUserResponse> Create(DtoUserCreateRequest userDto)
        {
            var preValidated = PreValidate(userDto);
            if (!preValidated.Success)
                return Result<DtoUserResponse>.Fail(preValidated.Errors);

            if (_userRepo.EmailExists(userDto.Email))
                return Result<DtoUserResponse>.Fail(ErrorMessages.EmailAlreadyExists);
            try
            {
                User user = userDto.Adapt<User>();
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                _userRepo.Create(user);
                return Result<DtoUserResponse>.Ok(user.Adapt<DtoUserResponse>());
            }
            catch (Exception ex)
            {
                return Result<DtoUserResponse>.Fail(ex.Message);
            }
        }
        public Result<DtoUserResponse> Read(Guid id)
        {
            User? user = _userRepo.GetById(id);
            if (user is null)
                return Result<DtoUserResponse>.Fail(ErrorMessages.UserNotFound);
            return Result<DtoUserResponse>.Ok(user.Adapt<DtoUserResponse>());
        }
        public Result<List<DtoUserResponse>> ReadAll()
        {
            List<User> users = _userRepo.GetAll();
            if (users is null || users.Count == 0)
                return Result<List<DtoUserResponse>>.Ok([]);
            return Result<List<DtoUserResponse>>.Ok(users.Adapt<List<DtoUserResponse>>());
        }
        public Result<DtoUserResponse> Update(Guid id, DtoUserUpdateRequest userDto)
        {
            Result preValidated = PreValidate(userDto);
            if (!preValidated.Success)
                return Result<DtoUserResponse>.Fail(preValidated.Errors);

            User? existingUser = _userRepo.GetById(id);
            if (existingUser is null)
                return Result<DtoUserResponse>.Fail(ErrorMessages.UserNotFound);

            if (existingUser.Email != userDto.Email && _userRepo.EmailExists(userDto.Email))
                return Result<DtoUserResponse>.Fail(ErrorMessages.EmailAlreadyExists);
            try
            {
                userDto.Adapt(existingUser);
                _userRepo.Update(existingUser);
                return Result<DtoUserResponse>.Ok(existingUser.Adapt<DtoUserResponse>());
            }
            catch (Exception ex)
            {
                return Result<DtoUserResponse>.Fail(ex.Message);
            }
        }
        public Result Delete(Guid id)
        {
            User? user = _userRepo.GetById(id);
            if (user is null)
                return Result.Fail(ErrorMessages.UserNotFound);
            try
            {
                _userRepo.Delete(user);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
        public Result<DtoUserResponse> Login(string email, string password)
        {
            email = email.Trim().ToLower();
            User? user = _userRepo.GetByEmail(email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return Result<DtoUserResponse>.Fail(ErrorMessages.UserNotFound);
            return Result<DtoUserResponse>.Ok(user.Adapt<DtoUserResponse>());
        }

        public async Task SendConfirmationEmailAsync(Guid userId, string email, string name, IEmailService emailService, string baseUrl)
        {
            try
            {
                User? user = _userRepo.GetById(userId);
                if (user is null) return;

                var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32)).ToLower();
                user.EmailConfirmationToken = token;
                _userRepo.Update(user);

                var link = $"{baseUrl}/conferma-email?token={token}";
                await emailService.SendAsync(email, name, "Conferma la tua email — Archetipo",
                    EmailTemplates.ConfirmEmail(name, link));
            }
            catch { }
        }

        public async Task<Result> ForgotPasswordAsync(string email, IEmailService emailService, string baseUrl)
        {
            try
            {
                email = email.Trim().ToLower();
                User? user = _userRepo.GetByEmailTracked(email);
                if (user is null) return Result.Ok();

                var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32)).ToLower();
                user.PasswordResetToken = token;
                user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(2);
                _userRepo.Update(user);

                var link = $"{baseUrl}/reimposta-password?token={token}";
                await emailService.SendAsync(email, user.Name, "Reimposta la tua password — Archetipo",
                    EmailTemplates.ResetPassword(user.Name, link));
            }
            catch { }
            return Result.Ok();
        }

        public Result ResetPassword(string token, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 8)
                return Result.Fail(ErrorMessages.PasswordTooShort);

            User? user = _userRepo.GetByResetToken(token);
            if (user is null)
                return Result.Fail(ErrorMessages.InvalidResetToken);

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = null;
            _userRepo.Update(user);
            return Result.Ok();
        }

        public Result ConfirmEmail(string token)
        {
            User? user = _userRepo.GetByConfirmToken(token);
            if (user is null)
                return Result.Fail(ErrorMessages.InvalidConfirmToken);

            user.EmailConfirmed = true;
            user.EmailConfirmationToken = null;
            _userRepo.Update(user);
            return Result.Ok();
        }

        private Result PreValidate(IUserDto dto)
        {
            SanitizeUser(dto);
            var errors = ValidateUserStructure(dto);
            if (errors.Any())
                return Result.Fail(errors);
            return Result.Ok();
        }
        private void SanitizeUser(IUserDto dto)
        {
            if (dto == null) return;
            if (!string.IsNullOrWhiteSpace(dto.Email))
                dto.Email = dto.Email.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(dto.Name))
                dto.Name = dto.Name.Trim();
            if (!string.IsNullOrWhiteSpace(dto.TradingName))
                dto.TradingName = dto.TradingName.Trim();
        }
        private List<string> ValidateUserStructure(IUserDto dto)
        {
            var errors = new List<string>();
            if (!string.IsNullOrEmpty(dto.Name) && dto.Name.Any(char.IsDigit))
                errors.Add(ErrorMessages.InvalidName);
            if (dto.Seller)
            {
                if (string.IsNullOrEmpty(dto.Iva)) errors.Add(ErrorMessages.MissingIVA);
                if (string.IsNullOrEmpty(dto.TradingName)) errors.Add(ErrorMessages.MissingTradingName);
            }
            if (!string.IsNullOrEmpty(dto.Iva) && dto.Iva?.Length > 11)
                errors.Add(ErrorMessages.IvaSizeViolation);
            return errors;
        }
    }
}
