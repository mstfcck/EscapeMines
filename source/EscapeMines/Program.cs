using EscapeMines.Abstractions;
using EscapeMines.Abstractions.Services;
using EscapeMines.BoardObjects;
using EscapeMines.Services;
using EscapeMines.TurtleObjects;
using EscapeMines.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;

namespace EscapeMines
{
    class Program
    {
        static void BindInput(string instructions)
        {
            var instructionLines = instructions.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            Console.WriteLine("\nTest Inputs \n------------------------------");

            foreach (var instructionLine in instructionLines)
            {
                Console.WriteLine(instructionLine);
            }
        }

        static void BindOutput(string instructions)
        {
            Console.WriteLine("\nTest Outputs \n------------------------------");

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IInvoker, Invoker>()
                .AddSingleton<IBoard, Board>()
                .AddSingleton<IMine, Mine>()
                .AddSingleton<IExit, Exit>()
                .AddSingleton<ITurtle, Turtle>()
                .AddSingleton<ICommandParserService, CommandParserService>()
                .AddSingleton<ICommandService, CommandService>()
                .AddSingleton<ITurtleMoveService, TurtleMoveService>()
                .AddSingleton<IInstructionValidator, InstructionValidator>()
                .BuildServiceProvider();

            var commandGenerator = serviceProvider.GetService<ICommandParserService>();
            var invoker = serviceProvider.GetService<IInvoker>();

            var commands = commandGenerator.Parse(instructions);
            invoker.SetCommands(commands);
            invoker.InvokeCommands();

            var result = invoker.GetResult();
            Console.WriteLine(result);

            var location = invoker.GetLatestTurtleLocation();
            Console.WriteLine(location);
        }

        static void Main(string[] args)
        {

            // The turtle hits the mine.

            var sampleOne = new StringBuilder();
            sampleOne.AppendLine("5 4");
            sampleOne.AppendLine("1,1 1,3 3,3");
            sampleOne.AppendLine("4 1");
            sampleOne.AppendLine("0 1 N");
            sampleOne.AppendLine("R M L M M");
            sampleOne.Append("R M M M");

            BindInput(sampleOne.ToString());
            BindOutput(sampleOne.ToString());


            // The turtle finds the exit.

            var sampleTwo = new StringBuilder();
            sampleTwo.AppendLine("5 4");
            sampleTwo.AppendLine("1,1 1,3 3,3");
            sampleTwo.AppendLine("4 1");
            sampleTwo.AppendLine("0 1 N");
            sampleTwo.Append("R R M L M M M M L M");

            BindInput(sampleTwo.ToString());
            BindOutput(sampleTwo.ToString());


            // The turtle is still in danger.

            var sampleThree = new StringBuilder();
            sampleThree.AppendLine("5 4");
            sampleThree.AppendLine("1,1 1,3 3,3");
            sampleThree.AppendLine("4 1");
            sampleThree.AppendLine("0 1 N");
            sampleThree.Append("R R M L M M");

            BindInput(sampleThree.ToString());
            BindOutput(sampleThree.ToString());


            Console.ReadKey();
        }
    }
}
