namespace OpenWebUISharp.Tests
{
	public abstract class BaseModelTests
	{
		internal static async Task DeleteAllModels()
		{
			var wrapper = new OpenWebUIWrapper(APIConfiguration.APIKey, APIConfiguration.APIURL);
			var models = await wrapper.GetAllModels();
			foreach (var model in models)
				await wrapper.DeleteModelByID(model.ID);
		}
	}
}
