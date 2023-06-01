using AuthService.Shared.ValueObjects.Email;
using AuthService.Shared.ValueObjects.FirstName;
using AuthService.Shared.ValueObjects.LastName;
using JetBrains.Annotations;
using AuthService.Shared.ValueObjects.CreatedAt;
using AuthService.Modules.Core.ValueObjects;
using AuthService.Shared.ValueObjects.Password;
using AuthService.Modules.Users.Core.Entities;

namespace AuthService.Modules.Core.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public Email Email { get; private set; }
        public FirstName FirstName { get; set; }
        public LastName LastName { get; set; }
        public Password Password { get; set; }
        public UserState State { get; private set; }
        public Role Role { get; private set; }
        public CreatedAt CreatedAt { get; set; }
        [CanBeNull] private Provider RegistrationProvider { get; set; }

        private User(Email email, FirstName firstName, LastName lastName, Password password, Role role, UserState state, DateTime createdAt, Provider registrationProvider)
        {
            Id = Guid.NewGuid();
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Role = role;
            State = state;
            CreatedAt = createdAt;
            RegistrationProvider = registrationProvider;
        }
        private User(Email email, FirstName firstName, LastName lastName, Password password, Role role, UserState state, DateTime createdAt)
        {
            Id = Guid.NewGuid();
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Role = role;
            State = state;
            CreatedAt = createdAt;
        }

        private User() { }

        public static User Create(Email email, FirstName firstName, LastName lastName, Password password, Role role, UserState state, DateTime createdAt)
        {
            return new User(email, firstName, lastName, password, role, state, createdAt);
        }

        public static User CreateWithProvider(Email email, FirstName firstName, LastName lastName, Password password, Role role, UserState state, DateTime createdAt, Provider registrationProvider)
        {
            return new User(email, firstName, lastName, password, role, state, createdAt, registrationProvider);
        }
    }
}