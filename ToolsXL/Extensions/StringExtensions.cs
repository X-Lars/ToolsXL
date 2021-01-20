using System;
using System.Text.RegularExpressions;

namespace ToolsXL.Extensions
{
    /// <summary>
    /// Defines the options to be used by the <see cref="Like"/> method.
    /// </summary>
    [Flags]
    public enum LikeOptions
    {
        Default   = 0, // Case insensitive, respect begin and end
        MatchCase = 1, // Case sensitive
        Implicit  = 2  // Filter is prefixed and postfixed with a wildcard
    }

    public static class StringExtensions
    {
        /// <summary>
        /// Compares the <see cref="string"/> against the specified filter.
        /// </summary>
        /// <param name="source">The <see cref="string"/> to compare.</param>
        /// <param name="filter">A <see cref="string"/> specifying the pattern to match.</param>
        /// <param name="prefixWildcard">Specifies if a wildcard prefix is added to the filter.</param>
        /// <returns>A <see cref="bool"/> containing true if the <paramref name="source"/> matches the <paramref name="filter"/> pattern.</returns>
        /// <remarks><i>Use ' ' or '*' as wildcard for any number of characters, use '?' as single character wildcard.</i></remarks>
        public static bool Like(this string source, string filter, LikeOptions options = LikeOptions.Default)
        {
            string wildcard = string.Empty;

            if (options.HasFlag(LikeOptions.Implicit))
                wildcard = ".*";

            if (options.HasFlag(LikeOptions.MatchCase))
            {
                return new Regex($@"^{wildcard}" + Regex.Escape(filter).Replace(@"\?", ".").Replace(@"\ ", ".*").Replace(@"\*", ".*") + $"{wildcard}$", RegexOptions.Singleline).IsMatch(source);
            }
            else
            {
                return new Regex($@"^{wildcard}" + Regex.Escape(filter).Replace(@"\?", ".").Replace(@"\ ", ".*").Replace(@"\*", ".*") + $"{wildcard}$", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(source);
            }
        }
    }
}

