using System;

namespace Sofa3.Domain.ReportExport;

public sealed class ExportedReport
{
    public ExportedReport(string fileName, string mimeType, byte[] data)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(mimeType);
        ArgumentNullException.ThrowIfNull(data);

        FileName = fileName;
        MimeType = mimeType;
        Data = data.Length == 0 ? Array.Empty<byte>() : data.ToArray();
    }

    public string FileName { get; }

    public string MimeType { get; }

    public byte[] Data { get; }
}

