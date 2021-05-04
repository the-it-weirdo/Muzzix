
namespace OnlineMusicStore.ViewModels
{

    public enum ContactFormType
    {
        FEEDBACK, REQUEST
    }

    public class ContactUsViewModel
    {
        public FeedbackViewModel FeedbackViewModel { get; set; }

        public RequestViewModel RequestViewModel { get; set; }
    }
}