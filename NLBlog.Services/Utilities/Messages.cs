using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLBlog.Services.Utilities
{
    public static class Messages
    {
        public static class Category
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç kategori bulunamadı";
                return "Böyle bir kategori bulunamadı";
            }
            public static string Add(string categoryName)
            {
                return $"{categoryName} adlı kategori başarı ile eklenmiştir.";
            }

            public static string Update(string categoryName)
            {
                return $"{categoryName} adlı kategori başarı ile güncellenmiştir.";
            }
            public static string Delete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarı ile silinimiştr.";
            }
            public static string HardDelete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarı ile veritabanından silinimiştr.";
            }
        }
        public static class Article
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç makale bulunamadı";
                return "Böyle bir makale bulunamadı";
            }
            public static string Add(string articleName)
            {
                return $"{articleName} adlı makale başarı ile eklenmiştir.";
            }

            public static string Update(string articleName)
            {
                return $"{articleName} adlı makale başarı ile güncellenmiştir.";
            }
            public static string Delete(string articleName)
            {
                return $"{articleName} adlı makale başarı ile silinimiştr.";
            }
            public static string HardDelete(string articleName)
            {
                return $"{articleName} adlı makale başarı ile veritabanından silinimiştr.";
            }
        }
    }
}
