using Plurally;

namespace ManagementInformation
{
    public static class StringExtensions
    {
        private static string Singularize(this string source)
        {
            var pluralizer = new Pluralizer();

            return pluralizer.IsSingular(source)? source : pluralizer.Singularize(source);
        }

        private static string Pluralize(this string source)
        {
            var pluralizer = new Pluralizer();

            return pluralizer.IsPlural(source) ? source : pluralizer.Pluralize(source);
        }
        public static string Pluralize(this string source, int count)
        {
            var isPlural = count != 1;
            var singular = source.Singularize();
            var plural = source.Pluralize();

            return isPlural ? plural : singular;
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }
    }
}