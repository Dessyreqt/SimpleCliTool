# SimpleCliTool
SimpleCliTool is a base console application that doesn't have any dependencies and can be used to quickly make a CLI environment with some nice features. Among them:

## Command Discovery
SimpleCliTool comes with a built in `ListCommands` command that shows the user all of the commands available in the CLI environment.

Additionally there is also a built in `Help` command which provides a quick summary for each command available including parameters and usage.

## Command Completion
SimpleCliTool comes with the ability to complete commands via the `Tab` key. Additionally partial commands are accepted as inputs: using `o` instead of typing `OpenConfiguration` is perfectly fine. 

If another command is added that also starts with "o", SimpleCliTool will automatically output all commands that start with the user input so they can see that their entry was ambiguous. The `Tab` autocomplete also cycles through all matches.

## Ease of Extension
Adding commands to the SimpleCliTool is extremely easy. A new command can be added with minimal additional hooks or wires into the system.

## Built in Error Handling
If an error occurs in a command you've written, SimpleCliTool will automatically report that error back to the console and continue as normal.

## Built in Required Validation
If a parameter is missing from a command the user inputs, there is built in validation to catch those errors before they ever make it to your command logic

# Adding a Command

## The Basics
Adding a command to the simple cli tool is very simple. Simply create your command class anywhere within the Logic/User folder (it can be in a subfolder). Make sure the classname ends with the word "Command" and inherits from the `Command` Class. Implement the constructor, override the HelpText property, and override the Handle method. And that's it! SimpleCliTool will automatically find your command on startup and add it to its command list without you having to add any extra code so that autocomplete and execution works with the command.

## Parameters
If your command requires paramaters, you can also override the Parameters property. An example of what this might look like is found in the `HelpCommand` class. Named parameters can be created like so: `new CommandParameter("n", "name", "The name being passed into the command.", true)`. In such a case the command documentation would look like this:

```
Usage: Command -n <name>

Parameters:
  <name> - The name being passed into the command.
```

That `true` as the last parameter tells SimpleCliTool that this parameter is required and to check for its presence before running the command.

## AppState
SimpleCliTool uses an `AppState` object to keep track of the things happening in the program. This appState is passed to each command by default. Feel free to add properties to keep track of whatever information you need between commands within the AppState.
