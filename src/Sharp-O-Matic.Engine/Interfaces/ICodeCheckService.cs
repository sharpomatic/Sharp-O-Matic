namespace SharpOMatic.Engine.Interfaces;

public interface ICodeCheck
{
    Task<List<CodeCheckResultModel>> CodeCheck(CodeCheckRequestModel request);
}
