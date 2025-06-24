using OpenWebUISharp.Models.ChatCompletions;
using OpenWebUISharp.Models.Knowledgebases;
using OpenWebUISharp.Models.Models;
using OpenWebUISharp.Models.Tools;

namespace OpenWebUISharp
{
	public interface IOpenWebUIWrapper
	{
		public string Token { get; set; }
		public string APIURL { get; set; }

		public Task<List<Model>> GetAllModels();

		public Task<Conversation> Query(Conversation conversation, string modelId, ConversationOptions? options = null);

		public Task<List<KnowledgebaseModel>> GetAllKnowledgebases();
		public Task<KnowledgebaseModel> GetKnowledgebase(string id);
		public Task<KnowledgebaseModel> CreateKnowledgebase(string name);
		public Task DeleteKnowledgebase(KnowledgebaseModel knowledgebase);
		public Task AddFileToKnowledgebase(string text, KnowledgebaseModel collection, string fileName);
		public Task RemoveFileFromCollection(string fileID, KnowledgebaseModel collection);

		public Task<List<ToolModel>> GetAllTools();
	}
}
