namespace SharpOMatic.Editor.DataTransferObjects;

public class AssetUploadRequest
{
    public required IFormFile File { get; set; }
    public required string Name { get; set; }
    public AssetScope Scope { get; set; }
    public Guid? RunId { get; set; }
}
