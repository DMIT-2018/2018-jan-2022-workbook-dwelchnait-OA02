<Query Kind="Program" />

void Main()
{
	//Extension Methods
	//C# is referred to as an extensive language
	//this basically means you can add your own personal "software" to the C# language
	//this "software" extends the capability of C#, such as datatypes (classes), methods (extension methods), ....
	//these capabilities are only available to a project if there are included.
	
	//in this example we will "extend" the functionality of the string class:
	//create an instance of the string class called message
	string message = "hello world";
	Console.WriteLine(message);
	
	//classes have properties and methods
	Console.WriteLine(message.Length); //Length is a property of the string class
	Console.WriteLine(message.Substring(3)); //Substring is a method of the string class
	
	//what if I would like my string to "quack"
	Console.WriteLine(message.Quack()); //Quack() is NOT part of the C# string class, you need to create an extension method
	Console.WriteLine(message);
	
	//what if i have argument to send to my new extension method
	Console.WriteLine(message.Quack(5)); //Quack(argument) mehtod does not exist, lyou need to create an overload extension method

	//what about other strings
	string cheers = "Go Ducks Go";
	Console.WriteLine(cheers.Quack(20));
}

// You can define other methods, fields, classes and namespaces here

//Create extendsion method(s) for the following C# class:  string

//step 1: make a static class to hold the extension method(s)
//			this class can be called anything you like
public static class MyExtensionStringMethods
{
	
	//step 2: Add your pubic static string method(s) to this class
	public static string Quack(this string self)
	{
		//the return datatype from this method will be a string
		//this is the datatype of the class instance we are extending
		//
		//NOTE: you do NOT necessarily need to return a value; that is the rdt could be void
		//
		//the 1st parameter(the error msg does use the word argument) of the method
		//	signature identifies the class the extension method is associated with, string
		//
		//the parameter requires the following syntaz -> this datatype parametername
		//the contents of the parameter will be the contents of the calling instance (eg message)
		
		//your logic for the method
		string result = "quack " + self + " quack";
		return result;
	}

	public static string Quack(this string self, int quacktimes)
	{
		//any additional parameters for the extension method follows
		//	the requried datatype 1st parameter
		//you may have any number of additional parameters
		//code the additional parameters just like any other method parameter

		//your logic for the method
		string quacks = "";
		for(int i = 0; i < quacktimes; i++)
		{
			quacks += " ..quack.. ";
		}
		return $"{self} ({quacks})";
	}
}















