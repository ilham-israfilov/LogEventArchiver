# LogEventArchiver

This is a demo app for special case. Application lets you to archive event data from log file to a NoSQL database (LiteDB).
Usage:
dotnet LogEventArchiver.dll -logFile=<path_to_log_file_to_be_processed> -alertThreshold=<duration_threshold_to_mark_alert> -importerMaxReaderThreads=<count_of_threads_for_reading_and_importing>

Accepted log file content looks similar to below:

{"id":"scsmbstgra", "state":"STARTED", "type":"APPLICATION_LOG", "host":"12345", "timestamp":1491377495212}
{"id":"scsmbstgrb", "state":"STARTED", "timestamp":1491377495213}
{"id":"scsmbstgrc", "state":"FINISHED", "timestamp":1491377495218}
{"id":"scsmbstgra", "state":"FINISHED", "type":"APPLICATION_LOG", "host":"12345", "timestamp":1491377495217}
{"id":"scsmbstgrc", "state":"STARTED", "timestamp":1491377495210}
{"id":"scsmbstgrb", "state":"FINISHED", "timestamp":1491377495216}
