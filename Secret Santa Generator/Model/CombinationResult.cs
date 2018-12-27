namespace Secret_Santa_Generator.Model
{
    public class CombinationResult
    {
        public bool IsSequienceCompleted { get; set; }

        private CombinationResult()
        {
            
        }

        public static CombinationResult NotCompleted() => new CombinationResult
        {
            IsSequienceCompleted = false
        };

        public static CombinationResult Completed() => new CombinationResult
        {
            IsSequienceCompleted = true
        };
    }
}