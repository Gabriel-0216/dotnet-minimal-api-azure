namespace dotnet_minimal_api_azure.Models
{
    public class ResponseDto 
    {
        public bool Success { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();

        public record Error(string description);
        public void AddError(string description){
            Success = false;
            Errors.Add(new Error(description));
        }
        public void SetSuccess(){
            Success = true;
        }
    }
}