using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class ConversationPanel : MonoBehaviour {

	[SerializeField]
	private Text _textUI;

	private Conversation _conversation;
	private Action _onClosedCallback;
	private int _currentSentenceIndex;

	public void Show(Conversation conversation, Action onClosedCallback)
	{
		_conversation = conversation;
		_onClosedCallback = onClosedCallback;
		_currentSentenceIndex = -1;
		ShowNextSentence();
		gameObject.SetActive(true);
	}

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
