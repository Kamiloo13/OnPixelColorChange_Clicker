# OnPixelColorChange_Clicker

This is a simple C# console application that enables you to automate mouse clicks when a particular pixel color changes.

*Note: This code was developed for auto-fishing in **Core Keeper**. If you want to modify the mouse clicking pattern, you can do so by changing `DeviceManipulator.cs`*

## How it works

The program lets you choose a pixel color on your screen and checks continuously if the color of that pixel has changed. Once the color changes, the program simulates a mouse click.

## Usage

- NumPad1: Save pixel position
- NumPad2: Start clicker
- NumPad3: Stop clicker

## Installation

1. Clone or download the repository
2. Open the solution in Visual Studio
3. Build the solution
4. Run the `OnPixelColorChange_Clicker.exe` file in the bin folder

## Dependencies

- .NET 6.0
- System.Drawing.Common 7.0.0

## License
This project is licensed under the <u>**MIT License**</u>.
