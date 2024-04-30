using Godot;
using System;
using System.Collections.Generic;


//Handles Unit Subsystem. 
public partial class UnitController : Node
{

    public bool turnFlag = false; //false when its player 1's turn, true when its player 2's
    public List<Node2D> allUnits = new List<Node2D> { };

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SpawnUnit(1, 5, "Here.mp3");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {


    }

    //activates when the end turn button is clicked. 
    private void _turn_button_pressed()
    {
        turnFlag = !turnFlag; // switch turns.

        //GD.Print(turnFlag);
    }

    public void SpawnUnit(int x, int y, string templatePath)
    {
        //instance unit
        var baseUnit = GD.Load<PackedScene>("res://unitTemplate.tscn"); // Will load when the script is instanced
        Node2D inst = baseUnit.Instantiate<Node2D>();

        //slave unit to tile

        inst.SetMeta("X", x);
        inst.SetMeta("Y", y);

        var playerVariables = GetNode<map>("/root/Map");

        foreach (TileRecord tile in playerVariables.Tiles)
        {
            if (tile.x == x && tile.y == y)
            {
                inst.SetMeta("tileID", tile.ID); //add metadata about id
                //GD.Print($"Found tile ID: {tile.ID}");
            }
        }

        //initialize ID
        inst.SetMeta("unitID", allUnits.Count);
        //set position
        inst.Position = new Vector2(x * 80, y * 80);//move to position
        AddChild(inst);//add unit to tree

        //place into masterpool
        allUnits.Add(inst);

    }

    public void moveUnit(int unitID, int x, int y)
    {
        foreach (Node2D unitParent in allUnits)
        {
            if (int.Parse(unitParent.GetMeta("unitID").ToString()) == unitID)
            {
                //check if this is within bounds


                // unit's tile update, also checks if the itle exists

                var playerVariables = GetNode<map>("/root/Map");
                bool foundTile = false;

                foreach (TileRecord tile in playerVariables.Tiles)
                {
                    if (tile.x == x && tile.y == y)
                    {
                        unitParent.SetMeta("tileID", tile.ID); //add metadata about id
                        foundTile = true;
                    }
                }
                if (foundTile == false)
                {
                    throw new ArgumentException($"Cannot move to {x},{y}! No tile found.");
                }

                //move the unit physically
                unitParent.Position = new Vector2(x * 80, y * 80);//move to position

            }
        }
    }

}
