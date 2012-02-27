namespace Phorcys.Services.Contracts
{
    public class Confirmation
    {
        public bool WasSuccessful { get; set; }
        public int SavedId { get; set; }
        public string Message { get; set; }

        public static Confirmation CreateSuccessConfirmation(string message = "", int savedId = 0)
        {
            return new Confirmation { WasSuccessful = true, Message = message, SavedId = savedId };
        }

        public static Confirmation CreateFailureConfirmation(string message = "", int savedId = 0)
        {
            return new Confirmation { WasSuccessful = false, Message = message, SavedId = savedId };
        }
    }
}
