namespace AdvancedRestfulConcerns.Api.Helpers
{
    public interface IPropertyCheckerService
    {
        public bool TypeHasProperties<T>(string fields);
    }
}