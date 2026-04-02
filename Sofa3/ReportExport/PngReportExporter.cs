using System;
using System.Buffers.Binary;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SprintReportModel = global::Sofa3.Domain.SprintReport.SprintReport;

namespace Sofa3.Domain.ReportExport;

public sealed class PngReportExporter : IReportExporter
{
    private static readonly byte[] PngSignature = [137, 80, 78, 71, 13, 10, 26, 10];

    public ExportedReport Export(SprintReportModel report)
    {
        ArgumentNullException.ThrowIfNull(report);

        var data = BuildPng(report);
        var fileName = BuildFileName(report.SprintName, "png");
        return new ExportedReport(fileName, "image/png", data);
    }

    private static byte[] BuildPng(SprintReportModel report)
    {
        var metadata = BuildMetadata(report);
        var color = DeriveColor(metadata);

        using var stream = new MemoryStream();
        stream.Write(PngSignature, 0, PngSignature.Length);
        WriteChunk(stream, "IHDR", BuildIhdrData(1, 1));
        WriteChunk(stream, "tEXt", BuildTextChunk("Comment", metadata));
        WriteChunk(stream, "IDAT", BuildImageData(color));
        WriteChunk(stream, "IEND", Array.Empty<byte>());
        return stream.ToArray();
    }

    private static byte[] BuildIhdrData(int width, int height)
    {
        var data = new byte[13];
        BinaryPrimitives.WriteInt32BigEndian(data.AsSpan(0, 4), width);
        BinaryPrimitives.WriteInt32BigEndian(data.AsSpan(4, 4), height);
        data[8] = 8;
        data[9] = 6;
        data[10] = 0;
        data[11] = 0;
        data[12] = 0;
        return data;
    }

    private static byte[] BuildImageData((byte Red, byte Green, byte Blue, byte Alpha) color)
    {
        var raw = new byte[] { 0, color.Red, color.Green, color.Blue, color.Alpha };
        using var compressed = new MemoryStream();
        using (var zlib = new ZLibStream(compressed, CompressionLevel.SmallestSize, leaveOpen: true))
        {
            zlib.Write(raw, 0, raw.Length);
        }

        return compressed.ToArray();
    }

    private static byte[] BuildTextChunk(string keyword, string text)
    {
        var keywordBytes = Encoding.ASCII.GetBytes(keyword);
        var textBytes = Encoding.ASCII.GetBytes(text);
        var chunk = new byte[keywordBytes.Length + 1 + textBytes.Length];

        Buffer.BlockCopy(keywordBytes, 0, chunk, 0, keywordBytes.Length);
        chunk[keywordBytes.Length] = 0;
        Buffer.BlockCopy(textBytes, 0, chunk, keywordBytes.Length + 1, textBytes.Length);
        return chunk;
    }

    private static void WriteChunk(Stream stream, string chunkType, byte[] data)
    {
        var typeBytes = Encoding.ASCII.GetBytes(chunkType);
        var lengthBytes = new byte[4];
        BinaryPrimitives.WriteInt32BigEndian(lengthBytes, data.Length);

        stream.Write(lengthBytes, 0, lengthBytes.Length);
        stream.Write(typeBytes, 0, typeBytes.Length);
        stream.Write(data, 0, data.Length);

        var crc = Crc32(typeBytes, data);
        var crcBytes = new byte[4];
        BinaryPrimitives.WriteUInt32BigEndian(crcBytes, crc);
        stream.Write(crcBytes, 0, crcBytes.Length);
    }

    private static uint Crc32(byte[] typeBytes, byte[] data)
    {
        var crc = 0xFFFFFFFFu;

        crc = UpdateCrc(crc, typeBytes);
        crc = UpdateCrc(crc, data);

        return ~crc;
    }

    private static uint UpdateCrc(uint crc, byte[] bytes)
    {
        foreach (var b in bytes)
        {
            crc ^= b;
            for (var i = 0; i < 8; i++)
            {
                var mask = (uint)-(int)(crc & 1);
                crc = (crc >> 1) ^ (0xEDB88320u & mask);
            }
        }

        return crc;
    }

    private static (byte Red, byte Green, byte Blue, byte Alpha) DeriveColor(string metadata)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(metadata));
        return (hash[0], hash[1], hash[2], 255);
    }

    private static string BuildMetadata(SprintReportModel report)
    {
        var lines = new[]
        {
            $"Sprint: {report.SprintName}",
            $"Project: {report.ProjectName}",
            $"Version: {report.Version}",
            $"Layout: {report.LayoutName}",
            $"Summary: {report.Content}"
        };

        return string.Join(" | ", lines.Select(SanitizeText));
    }

    private static string SanitizeText(string value)
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
}


