using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VinylTrackerWPF.Models;

namespace VinyltrackerWPF.Utils
{
    internal static class VinylCollectionUtils
    {
        public static string GetFavoriteGenre(this IEnumerable<VinylRecord> vinyls)
        {
            if (vinyls == null || !vinyls.Any()) return "N/A";

            var topGenre = vinyls
            .GroupBy(v => v.Genre)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefault();

            return topGenre ?? "N/A";
        }

        public static bool CheckClient(this string user_id)
        {
            if (!string.IsNullOrEmpty(user_id)) return true;
            return false;
        }

        public static (int Count, double TotalValue, string FavoriteGenre) GetRefreshStats(this List<VinylRecord> vinyls)
        {
            return (
                vinyls.Count,
                vinyls.Sum(v => v.RecomandedPrice),
                vinyls.GetFavoriteGenre()
            );
        }
        
    }
}
