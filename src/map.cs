using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Runtime.Serialization;
using Godot.Collections;
using System.Text.Json;
using System.Collections.Generic;

public class TileType
{
    public string TileName
    {
        get;
        set;
    }
    public string SpritePath
    {
        get;
        set;
    }
    public int MapIndex
    {
        get;
        set;
    }

}

public partial class map : Node
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        InitMap("res://maps/StandardSaveFile");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        return;
    }

    private void InitMap(string SaveFileDirectory)
    {
        int expectedRowSize; //To be implimented, take from metadata file.
        //load save file into a list of arrays. Each array represents a row in the map.
        var file = FileAccess.Open($"{SaveFileDirectory}/map.txt", FileAccess.ModeFlags.Read);
        List<Int16[]> dataset = new List<Int16[]> { };
        while (file.GetPosition() < file.GetLength())
        {

            Int16[] row = System.Array.ConvertAll<String, Int16>(file.GetCsvLine(), Int16.Parse);
            dataset.Add(row);
            GD.Print(dataset.Count);

        }

        //ensure all rows are equal in size. | TO BE IMPLIMENTED.
        /*
        foreach (Int16[] rowArray in dataset)
        {
            if (rowArray.Count<Int16>() != expectedRowSize)
            {
                throw new ArgumentException("Map file rows are not consistant in Length.")

            }
        }
		*/


        List<TileType> tileTypes = new System.Collections.Generic.List<TileType> { }; //All custom types of tile from the file.

        //parse json file for tile data
        //chatGPT cause i was lazy and frustrated. Fuck JSON files.
        // Read the JSON file
        string jsonString = FileAccess.GetFileAsString(SaveFileDirectory + "/tileData.json");

        // Deserialize JSON into a list of dictionaries
        List<System.Collections.Generic.Dictionary<string, object>> tileData = JsonSerializer.Deserialize<List<System.Collections.Generic.Dictionary<string, object>>>(jsonString);
        // Iterate over each dictionary in the list
        foreach (var tile in tileData)
        {
            TileType tileInformation = new TileType();
            tileInformation.TileName = tile["TileName"].ToString();
            tileInformation.SpritePath = SaveFileDirectory + tile["SpritePath"].ToString();
            tileInformation.MapIndex = int.Parse(tile["MapIndex"].ToString());
            tileTypes.Add(tileInformation);
        }

        //finished loading and parsing the data, use data to construct map.


        var baseTile = GD.Load<PackedScene>("res://Tile.tscn"); // Will load when the script is instanced


        for (int row = 0; row < dataset.Count; row++)
        {

            for (int tile = 0; tile < dataset[row].Count<Int16>(); tile++)
            {
                var inst = baseTile.Instantiate<Node2D>();
                string currentPath = "";
                bool foundFlag = false; //flags true if the tile's value in the map file is found in the json file. If false, the tile is not defined in the JSON
                GD.Print(tileTypes.Count); //ISSUE!
                foreach (TileType tileType in tileTypes)
                {
                    //See which tile this is 

                    if (dataset[row][tile] == tileType.MapIndex)
                    {
                        foundFlag = true;
                        currentPath = tileType.SpritePath;

                    }


                }

                if (foundFlag == false)
                {
                    throw new ArgumentException($"Tile @ ({tile},{row}) value not found");

                }

                inst.GetChild<Sprite2D>(0).Texture = ImageTexture.CreateFromImage(Image.LoadFromFile(currentPath));
                inst.Position = new Vector2(tile * 80, row * 80);
                AddChild(inst);
            }
        }

    }

}
