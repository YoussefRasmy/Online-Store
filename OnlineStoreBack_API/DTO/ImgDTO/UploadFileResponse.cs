namespace OnlineStoreBack_API.DTO.ImgDTO;

public enum UploadFileResponseCodes
{
    Success,
    WrongContentType,
    NoFilesFound,
    WrongExtension,
    EmptyFile,
}
public record UploadFileResponse(UploadFileResponseCodes ResponseCode, string Url = "");
