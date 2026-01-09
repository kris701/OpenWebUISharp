using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWebUISharp.Models.Knowledgebases.API
{
	internal class GetAllKnowledgebasesOutput
	{
		public List<OpenWebUIKnowledgebaseModel> Items { get; set; }
		public int Count { get; set; }
	}
}
