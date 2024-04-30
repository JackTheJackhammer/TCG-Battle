using Godot;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text.Json;


/*

AUTHOR'S NOTE: Please refactor the file structure system.

*/
public partial class Unit : Node2D
{
    //Universal Unit attributes
    public string unitBaseFilePath = "res://StandardUnit/"; //DEBUG -- REMOVE WHEN DEPLOYED.


    //Unit Positional Information

    public int xPosition;
    public int yPosition;
    public Godot.Vector2 pixelPosition;
    public int parentTileID;

    //Unit Attributes
    public int healthPoint;
    public int armourPoint;
    public int powerPoint;
    public int piercing;
    public int stamina;
    public int speed;

    //Weapon Attributes
    public string weaponTemplatePath;
    public bool ranged;
    public int sharpness;
    public int bluntness;
    public int supplylim;
    public int weight;
    public int range; //ranged only
    public int maxpower;
    public int acc; //ranged only
    public int cooldown; //ranged only
    public int ammo; //ranged only



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //World's saddest stack of code... - Willman, Designer of this code. (it seems like yandre dev made this.)
        //await JSON path assignment from subsystem controller
        while (unitBaseFilePath == "")
        {
            //THIS IS BAD IMPLIMENTATION! THIS BLOCKS THE THREAD. BOO! - The original programmer of this section.
        }
        //Load the unit file.
        var unitData = readJSON(unitBaseFilePath + "/Unit JSON template.json"); //of type 
                                                                                //assign unit data

        healthPoint = int.Parse(unitData["Health Point"].ToString());
        armourPoint = int.Parse(unitData["Armour Point"].ToString());
        powerPoint = int.Parse(unitData["Power Point"].ToString());
        piercing = int.Parse(unitData["Piercing"].ToString());
        stamina = int.Parse(unitData["Stamina Point"].ToString());
        speed = int.Parse(unitData["Speed"].ToString());
        weaponTemplatePath = unitData["Weapon File"].ToString();

        //Load the weapon file
        var weaponData = readJSON("weaponry/" + weaponTemplatePath);
        ranged = bool.Parse(weaponData["Is Ranged?"].ToString());
        sharpness = int.Parse(weaponData["Is Ranged?"].ToString());
        bluntness = int.Parse(weaponData["Is Ranged?"].ToString());
        supplylim = int.Parse(weaponData["Supply Limit"].ToString());
        weight = int.Parse(weaponData["Weight"].ToString());
        range = int.Parse(weaponData["Range"].ToString());
        maxpower = int.Parse(weaponData["Max Power"].ToString());
        acc = int.Parse(weaponData["Accuracy"].ToString());
        cooldown = int.Parse(weaponData["Cooldown"].ToString());
        ammo = int.Parse(weaponData["Ammo"].ToString());



    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    Dictionary<string, object> readJSON(string path)
    {
        //chatGPT cause i was lazy and frustrated. Fuck JSON files.
        // Read the JSON file
        string jsonString = FileAccess.GetFileAsString(path);

        if (jsonString == "")
        {
            throw new ArgumentException($"{path} is an invalid file for reading! (Unit.cs)"); //throw exception if this file cannot be read.

        }

        // Deserialize JSON into a list of dictionaries
        Dictionary<string, object> data = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);

        return data;
    }
}
