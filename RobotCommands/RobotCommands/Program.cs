// See https://aka.ms/new-console-template for more information

using RobotCommands.Lib;

Robot robot = new Robot();
Menu menu = new Menu();
CommandHandler handler = new CommandHandler(robot, menu);
handler.Run();