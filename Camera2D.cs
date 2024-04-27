using Godot;
using System;
using System.Numerics;

public partial class Camera2D : Godot.Camera2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        float movementStep = 5f;
        if (Input.IsKeyPressed(Key.W))
        {
            this.Position += new Godot.Vector2(0f, -movementStep);
        }
        if (Input.IsKeyPressed(Key.A))
        {
            this.Position += new Godot.Vector2(-movementStep, 0f);
        }
        if (Input.IsKeyPressed(Key.S))
        {
            this.Position += new Godot.Vector2(0f, movementStep);
        }
        if (Input.IsKeyPressed(Key.D))
        {
            this.Position += new Godot.Vector2(movementStep, 0f);
        }
    }

    //get leo to do this
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
        {
            switch (mouseEvent.ButtonIndex)
            {
                //Activates when mouse wheel goes up
                case MouseButton.WheelUp:
                    Zoom = Zoom * 2; // coded by Leo
                    break;

                //Activates when mouse wheel goes down
                case MouseButton.WheelDown:


                    Zoom = Zoom / 2;
                    break;
            }
        }
    }

}
