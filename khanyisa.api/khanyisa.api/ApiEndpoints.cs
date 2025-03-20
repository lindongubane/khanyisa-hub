namespace khanyisa.api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Users
    {
        public const string Base = $"{ApiBase}/users";

        public const string Create = "";
        public const string Get = $"{{userIdOrUsername}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";

        public const string Rate = $"{Base}/{{id:guid}}/ratings";
        public const string DeleteRating = $"{Base}/{{id:guid}}/ratings";
    }

    public static class Ratings
    {
        private const string Base = $"{ApiBase}/ratings";

        public const string GetUserRatings = $"{Base}/me";
    }
}
