namespace Sofa3.Domain.SprintReview;

public sealed class SprintReviewDocument
{
    public Guid DocumentId { get; }
    public string FileName { get; }
    public DateTime UploadedAt { get; }

    public SprintReviewDocument(Guid documentId, string fileName, DateTime uploadedAt)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException("File name is required.", nameof(fileName));
        }

        DocumentId = documentId;
        FileName = fileName;
        UploadedAt = uploadedAt;
    }
}

