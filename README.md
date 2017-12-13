# EscapeMines
Goal - the goal for the turtle is to escape mines and search for exit.

There are 2 projects 
1) TurtleEscapeMinesGame - a console app with the logic 
2) Test project - With few tests

To run the project, you have to give the file location with all the settings:

Sample File Example:
5 4
1,1 1,3 3,3
4 2
0 1 N
M R M M M M R M M 
R M M M
L L M L M M M M
M R M M M

1st line is the board settings
2nd line is the mine coordinates
3rd line is the exit coordinate
4th is the turtle start point and facing direction
5th line to the end of the file are the sequences of moves

To run the program from cmd from the bin/Debug folder of the app
> TurtleEscapeMinesGame.exe "C:\Users\..\..\Test.txt"
