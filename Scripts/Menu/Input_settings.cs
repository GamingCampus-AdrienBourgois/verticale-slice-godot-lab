using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Input_settings : Control
{
	int posclick = 0;
	Node action_list = null;
	private Scene_transition SceneTransition = null;

	bool is_remapping = false;
	string action_to_remap = null;
	Node remapping_button = null;
	private string[] input_actions = {"Up","Down","Left","Right","Interact","Accept","Use"};


	PackedScene button_input = ResourceLoader.Load("res://Scenes/Menu/Input_button.tscn") as PackedScene;
	public override void _Ready()
	{
		SceneTransition = GetParent().GetNode<Scene_transition>("SceneTransition");

		action_list = GetNode("PanelContainer").GetNode("MarginContainer").GetNode("VBoxContainer").GetNode("ScrollContainer").GetNode("ActionList");
		CreateActionList();
	}

	public override void _Process(double delta)
	{
	}

	private void CreateActionList()
	{
		InputMap.LoadFromProjectSettings();
		foreach(Node item in action_list.GetChildren())
		{
			item.QueueFree();
		}
		foreach(String action in input_actions){
			var button = button_input.Instantiate();
			Label action_label = (Label)button.FindChild("LabelAction");
			Label input_label = (Label)button.FindChild("LabelInput");
			
			action_label.Text = action;

			var events = InputMap.ActionGetEvents(action);
			if(events != null && events.Count() > 0)
			{
				input_label.Text = events[0].AsText().TrimSuffix(" (Physical)");
			}
			else
			{
				GD.Print("Action: " + action + ", No Events");
				input_label.Text = "";
			}

			action_list.AddChild(button);
			Input_button temp = (Input_button)button;
			temp.action = action;
			temp.Connect("ButtonPressed", new Callable(this,"_on_input_button_pressed"));
		}
	}

	private void _on_input_button_pressed(Node button,string action)
	{
		if (!is_remapping)
		{
			is_remapping = true;
			action_to_remap = action;
			remapping_button = button;
			Node label_button = button.FindChild("LabelInput");
			var temp = (Label)label_button;
			temp.Text = "Press key to bind...";
			
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (is_remapping)
		{
			if(@event is InputEventKey || @event is InputEventMouseButton && @event.IsPressed())
			{
				InputMap.ActionEraseEvents(action_to_remap);
				InputMap.ActionAddEvent(action_to_remap, @event);
				_update_action_list((Button)remapping_button, @event);

				is_remapping = false;
				action_to_remap = null;
				remapping_button = null;

				AcceptEvent();
			}
		}
		else 
		{
			if(@event.IsActionPressed("ui_cancel"))
			{
				SceneTransition.Call("changeScene","Scenes/Menu/Menu.tscn"); 
			}
		}
	}

	public void _update_action_list(Button button,InputEvent Event)
	{
		Node label_node = button.FindChild("LabelInput");
		Label label_button = (Label)label_node;
		label_button.Text = Event.AsText().TrimSuffix(" (Physical)");
	}

}
