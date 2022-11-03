namespace Library.Data
{
    public class DataConstants
    {
        public class Book
        {
            public const int TitleMaxLength = 50;
            public const int TitleMinLength = 10;

            public const int AuthorMaxLength = 50;
            public const int AuthorMinLength = 5;

            public const int DescriptionMinLength = 5;
            public const int DescriptionMaxLength = 5000;

            public const string MinRating = "0";
            public const string MaxRating = "10";
        }

        public class Category
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 5;
        }
    }
}
