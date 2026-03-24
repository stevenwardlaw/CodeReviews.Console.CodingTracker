Project: Tracking coding time.

Requirements/Comments/Thoughts:
Use Dapper instead of ADO.NET for the SQLite side of things.
Use Spectre Console for displaying the information. This was really good as it allowed me to display the records in a proper table and looked much nicer. Also having the ability to allow interactive user selections is nice. It also had the added benefit of ensuring the option that was being returned to the code - removing the risk of the user typing anything they wanted into it.
Validations on dates, selections, end time must be after start time.
Use separate classes - this was good as it kept everything organised. It also made me think of where to put certain methods as some of them could have been in a couple of different classes.
Initially did the date format the same the previous one (dd-MM-yyyy HH:mm) but then changed this to match the format that SQLite reads dates (ISO 8601). I chose the date I'm familiar with from the SQL I used in work (yyyy-MM-dd HH:mm).
Created the config file - I found this a bit tricky to find out how to actually read the fields from the json file. Struggled to find information online about this but did find it eventually!
Coding session is used when reading the records from the table.
Duration is calculated by the code.
