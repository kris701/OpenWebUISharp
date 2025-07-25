﻿using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions.API
{
	internal class ChatCompletionResponseSourcesMetaData
	{
		[JsonPropertyName("file_id")]
		public string ID { get; set; }
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("score")]
		public double Score { get; set; }
	}
}
