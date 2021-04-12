using Microsoft.Toolkit.Uwp.Notifications;

namespace LearningTimer
{
    class NotificationManager
    {
        private static ToastContentBuilder learnEndNotification;
        private static ToastContentBuilder breakEndNotification;

        static NotificationManager()
        {
            learnEndNotification = new ToastContentBuilder()
                .AddArgument("action", "endOfLearn")
                .AddText("Time for a break")
                .AddText("You've been learning for 45 mins now")
                .AddButton(new ToastButton()
                .SetContent("Make a break")
                .AddArgument("action", "endOfLearnButton"));

            breakEndNotification = new ToastContentBuilder()
               .AddArgument("action", "endOfBreak")
               .AddText("Come back to learning")
               .AddText("You've been having a break for 15 mins now") //tu czasy realne
               .AddButton(new ToastButton()
               .SetContent("Start learning again")
               .AddArgument("action", "endOfBreakButton"));
        }

        public static void showLearnEndNotification()
        {
            learnEndNotification.Show();
        }

        public static void showBreakEndNotification()
        {
            breakEndNotification.Show();
        }
    }
}
