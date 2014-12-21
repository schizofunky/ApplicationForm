using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public enum GameState
	{
		Active,
		Cutscene,
		Reading,		
		Talking
	}

	private const int PICKUPS_PER_STAGE = 3;
	private const int TOTAL_STAGES = 3;	

	[SerializeField]
	private Pickup[] _pickupObjects; 
	[SerializeField]
	private ConsoleDialogBox _consoleDialogBox;
	[SerializeField]
	private PromptPanel _promptPanel;
	[SerializeField]
	private ConversationPanel _conversationPanel;
	[SerializeField]
	private ObjectivePanel _objectivePanel;
	[SerializeField]
	private PathAnimation[] _pathAnimations;
	[SerializeField]
	private MovementScript _playerMovement;
	[SerializeField]
	private TextAsset _dialogueTextFile;
	[SerializeField]
	private AudioSource _pickupSfx;
	[SerializeField]
	private ApplicationForm _applicationFormObject;
	
	private int _currentPickups = 0;
	private int _currentStage = 0;
	private Dialogue _dialogue;
	private GameState _gameState;

	private void Awake () 
	{
		foreach (Pickup pickup in _pickupObjects)
		{
			pickup.OnPickupObtained = OnPickupObtained;
		}
		var dialogueParser = new DialogueParser();
		_dialogue = dialogueParser.parse(_dialogueTextFile.text);
		ShowConversation();
	}

	private void Update()
	{
		bool canUsePickups = _applicationFormObject.Trigger.WithinRange && (_currentPickups == PICKUPS_PER_STAGE);
		if(canUsePickups)
		{
			_promptPanel.Show(_dialogue.messages[Dialogue.INPUT_PROMPT]);
		}
		else
		{
			_promptPanel.Hide();
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			switch (_gameState)
			{
				case GameState.Active:
					if (canUsePickups)
					{
						OnPickupUsed();
					}
				break;
				case GameState.Reading:
					_consoleDialogBox.Close();
				break;
				case GameState.Talking:
					_conversationPanel.ShowNextSentence();
				break;
			}			
		}
	}
	
	private void ShowConversation()
	{		
		SetState(GameState.Talking);
		_playerMovement.SetIdleUp();
		_conversationPanel.Show(_dialogue.conversations[_currentStage], OnConversationPanelDismissed);
	}
	
	private void SetState(GameState state)
	{
		_gameState = state;
		if(state == GameState.Active)
		{
			_playerMovement.EnableMovement();
		}
		else
		{
			_playerMovement.DisableMovement();
		}
	}
	
	private void UpdateObjective()
	{
		if (_currentPickups < PICKUPS_PER_STAGE)
		{
			_objectivePanel.UpdateObjective(string.Format(_dialogue.messages[Dialogue.COLLECT_OBJECTIVE],_dialogue.messages[Dialogue.OBJECTIVE + _currentStage],_currentPickups,PICKUPS_PER_STAGE));
		}
		else
		{
			_objectivePanel.UpdateObjective(string.Format(_dialogue.messages[Dialogue.RETURN_OBJECTIVE],_dialogue.messages[Dialogue.OBJECTIVE + _currentStage]));
		}
	}
	
	private void OnPickupObtained(int pickupId)
	{
		_currentPickups++;
		_pickupSfx.Play();
		_consoleDialogBox.Show(_dialogue.pickups[pickupId], () => SetState(GameState.Active));
		SetState(GameState.Reading);
		UpdateObjective();
	}

	private void OnPickupUsed()
	{
		_currentPickups = 0;
		_currentStage++;		
		_promptPanel.Hide();
		_applicationFormObject.UpdateVisualState(_currentStage);
		ShowConversation();	
	}

	private void OnConversationPanelDismissed()
	{
		if (_currentStage == TOTAL_STAGES)
		{
			_objectivePanel.HideObjectivePanel();
			SetState(GameState.Cutscene);
			_applicationFormObject.PlaySubmitAnimation();
		}
		else
		{
			SetState(GameState.Active);
			_pathAnimations[_currentStage].PlayUnlockAnimation();
			_objectivePanel.ShowObjectivePanel();
			UpdateObjective();
		}
	}
}
