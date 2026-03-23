using Spectre.Console;

namespace codingTracker.stevenwardlaw
{
    internal static class UserInput
    {
        public static void DisplayOptions()
        {
            string option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .AddChoices("View all records", "Add a record", "Update a record", "Delete a record"));

            switch (option)
            {
                case "View all records":
                    AnsiConsole.Clear();
                    CodingController.GetAllRecords();
                    break;
                case "Add a record":
                    AnsiConsole.Clear();
                    int addedRows = CodingController.InsertRecord(
                        GetDateFromUser("Please enter the start date & time (in the following format dd-mm-yyy hh:mm): "),
                        GetDateFromUser("Please enter the end date & time (in the following format dd-mm-yyy hh:mm): "));
                    if (addedRows > 0) AnsiConsole.MarkupLine("The record was [green]successfully added.[/]");
                    else AnsiConsole.MarkupLine("[red]Warning:[/] No records were added.");
                    break;
                case "Update a record":
                    AnsiConsole.Clear();
                    CodingController.GetAllRecords();
                    int updatedRows = CodingController.UpdateRecord(
                        GetIdFromUser("Please enter the ID of the record you want to update: "),
                        GetDateFromUser("Please enter the new start date and time (in the following format dd-mm-yyy hh:mm): "),
                        GetDateFromUser("Please enter the new end date and time (in the following format dd-mm-yyy hh:mm): "));
                    if (updatedRows > 0) AnsiConsole.MarkupLine("The record was [green]successfully updated.[/]");
                    else AnsiConsole.MarkupLine("[red]Warning:[/] No records were updated.");
                    break;
                case "Delete a record":
                    AnsiConsole.Clear();
                    CodingController.GetAllRecords();
                    int deletedRows = CodingController.DeleteRecord(
                        GetIdFromUser("Please enter the ID of the record you want to delete: "));
                    if (deletedRows > 0) AnsiConsole.MarkupLine("The record was [green]successfully deleted.[/]");
                    else AnsiConsole.MarkupLine("[red]Warning:[/] No records were deleted.");
                    break;
            }
        }

        private static string GetDateFromUser(string message)
        {
            string date = "";
            date = AnsiConsole.Ask<string>($"[bold]{message}[/]");
            while (!DataValidation.ValidateDate(date))
            {
                date = AnsiConsole.Ask<string>("[bold]That is not in the correct date format, please enter a correct date: [/]");
            }
            
            return date;
        }

        private static int GetIdFromUser(string message)
        {
            string input = AnsiConsole.Ask<string>($"[bold]{message}[/]");
            while (!DataValidation.ValidateNumber(input))
            {
                input = AnsiConsole.Ask<string>("[bold]That is not a valid number, please enter an ID number: [/]");
            }
            return Convert.ToInt16(input);
        }
    }
}
