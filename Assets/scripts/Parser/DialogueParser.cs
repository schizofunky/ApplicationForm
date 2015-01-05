using System.Collections.Generic;
using System.Collections;
using SimpleJSON;

public class DialogueParser {
	/*
	 * Parses a JSON dialogue file
	 */ 
	public Dialogue parse(string dialogueText)
	{
		var data = JSON.Parse(dialogueText);
		Dialogue dialogue;
		dialogue.conversations = ParseConversations(data["conversations"].AsArray);
		dialogue.pickups = ParsePickupData(data["pickups"].AsArray);
		dialogue.messages = ParseMessages(data["messages"].AsArray);
		return dialogue;
	}

	/*
	 * Parses the conversation nodes of the JSON 
	 */ 
	private List<Conversation> ParseConversations(JSONArray conversationObjects)
	{
		var conversationList = new List<Conversation>();
		foreach (JSONNode conversationData in conversationObjects)
		{
			Conversation conversation;
			conversation.sentences = new List<string>();
			foreach (JSONNode sentenceData in conversationData["sentences"].AsArray)
			{
				conversation.sentences.Add(sentenceData.Value);
			}
			conversationList.Add(conversation);
		}
		return conversationList;
	}

	/*
	 * Parses the pickup nodes of the JSON 
	 */ 
	private List<PickupData> ParsePickupData(JSONArray pickupObjects)
	{		
		var pickupList = new List<PickupData>();
		foreach (JSONNode pickupData in pickupObjects)
		{
			PickupData pickup;
			pickup.name = pickupData["name"].Value;
			pickup.description = pickupData["description"].Value;
			pickupList.Add(pickup);
		}
		return pickupList;
	}	

	/*
	 * Parses the ui message nodes of the JSON 
	 */ 
	private Dictionary<string,string> ParseMessages(JSONArray messages)
	{		
		var dictionary = new Dictionary<string,string>();
		foreach (JSONNode message in messages)
		{
			dictionary[message["key"].Value] = message["value"].Value;
		}
		return dictionary;
	}
}
/*
 * Structs to hold all of the parsed data, using more readable names
 */ 
public struct Conversation
{
	public List<string> sentences; 
}

public struct PickupData
{
	public string name; 
	public string description;
}

public struct Dialogue
{
	public static string INPUT_PROMPT = "INPUT_PROMPT";
	public static string MOVE_PROMPT = "MOVE_PROMPT";
	public static string COLLECT_OBJECTIVE = "COLLECT_OBJECTIVE";
	public static string RETURN_OBJECTIVE = "RETURN_OBJECTIVE";
	public static string OBJECTIVE = "OBJECTIVE_";
	public List<Conversation> conversations;
	public List<PickupData> pickups;
	public Dictionary<string,string> messages;
}
