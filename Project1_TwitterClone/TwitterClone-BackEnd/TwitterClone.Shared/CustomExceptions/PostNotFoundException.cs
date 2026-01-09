namespace TwitterClone.Shared.CustomExceptions
{
    public class PostNotFoundException : Exception
    {
        public PostNotFoundException(int postId)
            : base($"Post with id {postId} was not found.")
        {
        }
    }
}

