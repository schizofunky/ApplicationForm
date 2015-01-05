using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class ConversationPanel : MonoBehaviour {

	public Text _textUI;

	private Conversation _conversation;
	private Action _onClosedCallback;
	private int _currentSentenceIndex;

	/*
	 * Sets up the Converstaion panel with the passed conversation text and enables it
	 * Also stores an Action delegate reference for when the screen is closed
	 */ 
	public void Show(Conversation conversation, Action onClosedCallback)
	{
		_conversation = conversation;
		_onClosedCallback = onClosedCallback;
		_currentSentenceIndex = -1;
		ShowNextSentence();
		gameObject.SetActive(true);
	}

	/*
	 * Each time this is called it shows the next available sentence in the conversation until it runs out
	 * It then calls the close function
	 */ 
	public void ShowNextSentence()
	{
		if (++_currentSentenceIndex == _conversation.sentences.Count)
		{
			CloseConversationPanel();
		}
		else
		{
			_textUI.text = _conversation.sentences[_currentSentenceIndex];
		}
	}

	private void CloseConversationPanel()
	{
		gameObject.SetActive(false);
		_onClosedCallback();
	}

}
