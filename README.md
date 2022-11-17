
Read-Me

We’ve had some issues with versioning despite doing our best to use git.
We have done our best to gather all needed to run the program and a short guide. 
Some of the tests will not pass as we ran out of time and had a lot of changes in some of our functions the last day. Best of luck.

To run the program, build the database. This is done by using the files in the BuildDB folder on github in the following order:   
Build1 -> Build2 -> functions. These are TXT files and should just be run as SQL queries (This required the initial IMDB, OMDB and WI data).  

To use the cloned repository alter the Connection string in DBcontext, Security.Authentication and DataServices.MovieDataService. 
Then make sure you have the following packages:Xunit, Npgsql.EntityFrameworkCore, Microsoft.AspNetCore.Cryptography.KeyDerivation. 
Set the Web Server project as a startup project. 
