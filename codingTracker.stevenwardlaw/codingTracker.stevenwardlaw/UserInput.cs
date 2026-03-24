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
                    int addedRows = UserInsertRecord();
                    if (addedRows > 0) AnsiConsole.MarkupLine("The record was [green]successfully added.[/]");
                    else AnsiConsole.MarkupLine("[red]Warning:[/] No records were added.");
                    break;
                case "Update a record":
                    AnsiConsole.Clear();
                    CodingController.GetAllRecords();
                    int updatedRows = UserUpdateRecord();
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

        private static int UserUpdateRecord()
        {
            int id = GetIdFromUser("Please enter the ID of the record you want to update: ");
            string startTime = GetDateFromUser("Please enter the new start date and time (in the following format yyyy-mm-dd hh:mm e.g. 2025-12-30 16:05): ");
            string endTime = GetDateFromUser("Please enter the new end date and time (in the following format yyyy-mm-dd hh:mm e.g. 2025-12-30 16:05): ");

            while (!DataValidation.IsEndDateAfter(startTime, endTime))
            {
                AnsiConsole.MarkupLine("[red]Error:[/] The end time must be [bold]after[/] the start time.");
                startTime = GetDateFromUser("Please enter the new start date and time (in the following format yyyy-mm-dd hh:mm e.g. 2025-12-30 16:05): ");
                endTime = GetDateFromUser("Please enter the new end date and time (in the following format yyyy-mm-dd hh:mm e.g. 2025-12-30 16:05): ");
            }
            int num = CodingController.UpdateRecord(id, startTime, endTime);
            return num;
        }

        private static int UserInsertRecord()
        {
            string startTime = GetDateFromUser("Please enter the start date and time (in the following format yyyy-mm-dd hh:mm e.g. 2025-12-30 16:05): ");
            string endTime = GetDateFromUser("Please enter the end date and time (in the following format yyyy-mm-dd hh:mm e.g. 2025-12-30 16:05): ");

            while (!DataValidation.IsEndDateAfter(startTime, endTime))
            {
                AnsiConsole.MarkupLine("[red]Error:[/] The end time must be [bold]after[/] the start time.");
                startTime = GetDateFromUser("Please enter the start date and time (in the following format yyyy-mm-dd hh:mm e.g. 2025-12-30 16:05): ");
                endTime = GetDateFromUser("Please enter the end date and time (in the following format yyyy-mm-dd hh:mm e.g. 2025-12-30 16:05): ");
            }
            int num = CodingController.InsertRecord(startTime, endTime);
            return num;
        }

        public static string ViewRecordOptions()
        {
            string dateFilter;
            string sortOrder;

            // Get option for filtering
            string dateFilterOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Would you like to filter for records that started in a specific time period?")
                .AddChoices("No, all records", "Past day", "Past week", "Past month", "Past year"));

            switch (dateFilterOption)
            {
                case "Past day":
                    dateFilter = "WHERE starttime > datetime('now','-1 days') ";
                    break;
                case "Past week":
                    dateFilter = "WHERE starttime > datetime('now','-7 days') ";
                    break;
                case "Past month":
                    dateFilter = "WHERE starttime > datetime('now','-1 month') ";
                    break;
                case "Past year":
                    dateFilter = "WHERE starttime > datetime('now','-1 year') ";
                    break;
                default:
                    dateFilter = "";
                    break;
            }

            // Get option for sorting
            string sortFilterOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Would you like to sort the records based on the Start Time or End Time?")
                .AddChoices("No", "Start Time (ascending)", "Start Time (descending)",
                "End Time (ascending)", "End Time (descending)"));

            switch (sortFilterOption)
            {
                case "Start Time (ascending)":
                    sortOrder = "ORDER BY starttime ASC";
                    break;
                case "Start Time (descending)":
                    sortOrder = "ORDER BY starttime DESC";
                    break;
                case "End Time (ascending)":
                    sortOrder = "ORDER BY endtime ASC";
                    break;
                case "End Time (descending)":
                    sortOrder = "ORDER BY endtime DESC";
                    break;
                default:
                    sortOrder = "";
                    break;
            }

            string fullSql = "SELECT * from codingTracker " + dateFilter + sortOrder;
            return fullSql;

        }
    }
}
