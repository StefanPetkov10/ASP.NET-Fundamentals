namespace SeminarHub.Common
{
    public class ValidationContracts
    {
        public static class Seminar
        {
            public const int SeminarTopicMinLength = 3;
            public const int SeminarTopicMaxLength = 100;
            public const int SeminarLecturerMinLength = 5;
            public const int SeminarLecturerMaxLength = 60;
            public const int SeminarDetailsMinLength = 10;
            public const int SeminarDetailsMaxLength = 500;
            public const int SeminarDurationMinValue = 30;
            public const int SeminarDurationMaxValue = 180;
            public const string SeminarDateDisplayFormat = "dd/MM/yyyy HH:mm";
        }

        public static class Category
        {
            public const int CategoryNameMinLength = 3;
            public const int CategoryNameMaxLength = 50;
        }
    }
}
