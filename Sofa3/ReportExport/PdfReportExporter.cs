using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using SprintReportModel = global::Sofa3.Domain.SprintReport.SprintReport;

namespace Sofa3.Domain.ReportExport;

public sealed class PdfReportExporter : IReportExporter
{
    public ExportedReport Export(SprintReportModel report)
    {
        ArgumentNullException.ThrowIfNull(report);

        var data = BuildPdf(report);
        var fileName = BuildFileName(report.SprintName, "pdf");
        return new ExportedReport(fileName, "application/pdf", data);
    }

    private static byte[] BuildPdf(SprintReportModel report)
    {
        var contentBytes = BuildContentStream(report);

        using var stream = new MemoryStream();
        WriteAscii(stream, "%PDF-1.4\n");

        var offsets = new List<int> { 0 };

        offsets.Add((int)stream.Position);
        WriteAscii(stream, "1 0 obj\n<< /Type /Catalog /Pages 2 0 R >>\nendobj\n");

        offsets.Add((int)stream.Position);
        WriteAscii(stream, "2 0 obj\n<< /Type /Pages /Kids [3 0 R] /Count 1 >>\nendobj\n");

        offsets.Add((int)stream.Position);
        WriteAscii(stream, "3 0 obj\n<< /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Resources << /Font << /F1 4 0 R >> >> /Contents 5 0 R >>\nendobj\n");

        offsets.Add((int)stream.Position);
        WriteAscii(stream, "4 0 obj\n<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>\nendobj\n");

        offsets.Add((int)stream.Position);
        WriteAscii(stream, $"5 0 obj\n<< /Length {contentBytes.Length} >>\nstream\n");
        stream.Write(contentBytes, 0, contentBytes.Length);
        WriteAscii(stream, "\nendstream\nendobj\n");

        var xrefStart = (int)stream.Position;
        WriteAscii(stream, "xref\n0 6\n");
        WriteAscii(stream, "0000000000 65535 f \n");

        for (var i = 1; i < offsets.Count; i++)
        {
            WriteAscii(stream, $"{offsets[i]:0000000000} 00000 n \n");
        }

        WriteAscii(stream, "trailer\n<< /Size 6 /Root 1 0 R >>\nstartxref\n");
        WriteAscii(stream, xrefStart.ToString(CultureInfo.InvariantCulture));
        WriteAscii(stream, "\n%%EOF");

        return stream.ToArray();
    }

    private static byte[] BuildContentStream(SprintReportModel report)
    {
        var lines = BuildLines(report);
        var builder = new StringBuilder();
        builder.AppendLine("BT");
        builder.AppendLine("/F1 12 Tf");
        builder.AppendLine("72 760 Td");

        for (var i = 0; i < lines.Count; i++)
        {
            builder.Append('(').Append(EscapePdfText(lines[i])).AppendLine(") Tj");
            if (i < lines.Count - 1)
            {
                builder.AppendLine("0 -16 Td");
            }
        }

        builder.AppendLine("ET");
        return Encoding.ASCII.GetBytes(builder.ToString());
    }

    private static List<string> BuildLines(SprintReportModel report)
    {
        return new List<string>
        {
            "Sofa3 Sprint Report",
            $"Sprint: {report.SprintName}",
            $"Project: {report.ProjectName}",
            $"Version: {report.Version}",
            $"Layout: {report.LayoutName}",
            $"Generated: {report.GeneratedAtUtc:o}",
            $"Summary: {report.Content}",
            $"Header: {report.HeaderText}",
            $"Footer: {report.FooterText}"
        };
    }

    private static string EscapePdfText(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        var builder = new StringBuilder(value.Length);
        foreach (var ch in value)
        {
            builder.Append(ch switch
            {
                '\\' => "\\\\",
                '(' => "\\(",
                ')' => "\\)",
                >= '\u0020' and <= '\u007e' => ch,
                _ => '?'
            });
        }

        return builder.ToString();
    }

    private static string BuildFileName(string sprintName, string extension)
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

    private static void WriteAscii(Stream stream, string value)
    {
        var bytes = Encoding.ASCII.GetBytes(value);
        stream.Write(bytes, 0, bytes.Length);
    }
}


