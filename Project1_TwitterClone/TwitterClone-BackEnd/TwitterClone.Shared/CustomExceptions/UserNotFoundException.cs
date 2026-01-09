namespace TwitterClone.Shared.CustomExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int userId)
            : base($"User with id {userId} was not found.")
        {
        }

        public UserNotFoundException(string username)
            : base($"User with username '{username}' was not found.")
        {
        }
    }
}

