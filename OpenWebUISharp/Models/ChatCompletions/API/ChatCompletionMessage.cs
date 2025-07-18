﻿using System.Text.Json.Serialization;

namespace OpenWebUISharp.Models.ChatCompletions.API
{
	internal class ChatCompletionMessage
	{
		[JsonPropertyName("role")]
		public string Role { get; set; }
		[JsonPropertyName("content")]
		public string Content { get; set; }
	}
}
