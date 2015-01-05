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

	public Pickup[] _pickupObjects; 
	public ConsoleDialogBox _consoleDialogBox;
	public PromptPanel _promptPanel;
	public ConversationPanel _conversationPanel;
	public ObjectivePanel _objectivePanel;
	public PathAnimation[] _pathAnimations;
	public MovementScript _playerMovement;
	public TextAsset _dialogueTextFile;
	public ApplicationForm _applicationFormObject;
	
	private int _currentPickups = 0;
	private int _currentStage = 0;
	private Dialogue _dialogue;
	private GameState _gameState;

	/*
	 *	Initialise pickups and parse dialogue before showing first conversation window 
	 */
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

	/*
	 * Check to see if player can use pickups and Handle keypress depending on Game State 
	 */ 
	private void Update()
	{
		bool canUsePickups = _applicationFormObject.Trigger.WithinRange && (_currentPickups == PICKUPS_PER_STAGE);
		if(canUsePickups)
		{
			_promptPanel.Show(_dialogue.messages[Dialogue.INPUT_PROMPT]);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			switch (_gameState)
			{
				case GameState.Active:
					if (canUsePickups)
					{
						_promptPanel.Hide();
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

	/*
	 * Shows a ui window of the player saying something based on the _currentStage value
	 */ 
	private void ShowConversation()
	{		
		SetState(GameState.Talking);
		_playerMovement.ShowExamineAnimation();
		_conversationPanel.Show(_dialogue.conversations[_currentStage], OnConversationPanelDismissed);
	}

	/*
	 * Changes the games state and disables movement if necessary
	 */ 
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

	/*
	 * Updates the game objective to either collect or return the pickups
	 */ 
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

	/*
	 * Handle pickup obtained, show an info UI about the pickup and update the objective
	 */ 
	private void OnPickupObtained(int pickupId)
	{
		_currentPickups++;
		_consoleDialogBox.Show(_dialogue.pickups[pickupId], () => SetState(GameState.Active));
		SetState(GameState.Reading);
		UpdateObjective();
	}

	/*
	 * Update the games progress and the visual state of the Application form.  Show a dialogue for the next task
	 */ 
	private void OnPickupUsed()
	{
		_currentPickups = 0;
		_currentStage++;		
		_promptPanel.Hide();
		_applicationFormObject.UpdateVisualState(_currentStage);
		ShowConversation();	
	}

	/*
	 * After a conversation ends the next area of the game is unlocked or if it's the end it plays the end sequence
	 */ 
	private void OnConversationPanelDismissed()
	{
		if (_currentStage == TOTAL_STAGES)
		{
			_objectivePanel.HideObjectivePanel();
			SetState(GameState.Cutscene);
			_applicationFormObject.PlayEndSequence();
		}
		else
		{
			SetState(GameState.Active);
			_pathAnimations[_currentStage].PlayUnlockAnimation();
			_objectivePanel.ShowObjectivePanel();
			UpdateObjective();
			if(_currentStage == 0)
			{
				_promptPanel.Show(_dialogue.messages[Dialogue.MOVE_PROMPT],5);
			}
		}
	}
}
