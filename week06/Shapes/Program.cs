// Program.cs
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Test individual shapes
        Square square = new Square("Red", 5);
        Console.WriteLine($"Square - Color: {square.GetColor()}, Area: {square.GetArea()}");

        Rectangle rectangle = new Rectangle("Blue", 4, 6);
        Console.WriteLine($"Rectangle - Color: {rectangle.GetColor()}, Area: {rectangle.GetArea()}");

        Circle circle = new Circle("Green", 3);
        Console.WriteLine($"Circle - Color: {circle.GetColor()}, Area: {circle.GetArea()}");

        // Create a list of shapes
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Square("Yellow", 2));
        shapes.Add(new Rectangle("Purple", 3, 5));
        shapes.Add(new Circle("Orange", 4));
        shapes.Add(new Square("Black", 2.5));
        shapes.Add(new Rectangle("White", 10, 2));

        // Iterate through the list and display color and area
        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"{shape.GetType().Name} - Color: {shape.GetColor()}, Area: {shape.GetArea():F2}");
        }
    }
}