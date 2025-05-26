using System.Buffers;
using UnityEngine;

//abstract class
//This cannot be made into an object
public abstract class Car
{
    private string owner = "";
    
    //Protected is like private, but
    //inherited classess can access it
    protected string colour = "Green";

    public float speed = 5;
    public int wheelCount = 4;

    //Constructor
    public Car()
    {
        Debug.Log("Constructor");
        owner = "KOTOR";
    }

    //Overloaded Constructor
    public Car(string name) 
    { 
        owner = name;
    }

    public string VictorySpeech() 
    {
        return owner + " proves that " + colour + " is cool ";
    }

    //virtual Allows this function to be overridden
    public virtual void SetOwner(string name) 
    {
        owner = name;
    }

    //We don't have to define abstract functions
    public abstract string Honk();
}

public class F1 : Car 
{ 
    public F1() 
    { 
        speed = 10;
        colour = "Red";
        wheelCount = 6;
        SetOwner("Andrew");
    }

    public override void SetOwner(string name) 
    { 
        base.SetOwner("F1 Driver " + name);
    }

    public override string Honk() 
    {
        return "VROOOM";
    }
}

public class Clown : Car
{
    public override string Honk()
    {
        return "Iee Yesss";
    }

    public void EjectClown() 
    {
        Debug.Log("Clown goes flying");
    }

    //Overloaded Function
    public void EjectClown(int number) 
    {
        Debug.Log(number + " of clowns goes flying");
    }
}