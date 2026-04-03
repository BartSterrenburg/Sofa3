using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sofa3.Domain.ReportExport
{

    internal static class ReportExportFileNameHelper
    {
        public static string BuildFileName(string sprintName, string extension)
        {
            var baseName = SanitizeFileName(string.IsNullOrWhiteSpace(sprintName) ? "sprint-report" : sprintName);
            return $"{baseName}.{extension}";
        }

        private static string SanitizeFileName(string value)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var builder = new StringBuilder(value.Length);

            foreach (var ch in value)
            {
                if (Array.IndexOf(invalidChars, ch) >= 0 || char.IsWhiteSpace(ch))
                {
                    builder.Append('-');
                }
                else
                {
                    builder.Append(ch);
                }
            }

            var sanitized = builder.ToString().Trim('-', '.');
            return string.IsNullOrWhiteSpace(sanitized) ? "sprint-report" : sanitized;
        }
    }

}