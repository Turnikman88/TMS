# Task Management System

Design and implement a Tasks Management console application.

The application will be used by a small team of developers, who need to keep track of all the tasks, surrounding a software product they are building.


# Task Management System
This is a console application by which any organization can manage tasks among its employees. 

# Installing
First you need to install:
[Visual Studio](https://visualstudio.microsoft.com/downloads/)

[Git](https://git-scm.com/downloads)

Inside GitBash clone our project by typing this command:
git clone “https://gitlab.com/telerikprojectone_official/task-management-system.git”
# Using TMS
Task Management System can do all sorts of things. You must be logged to use most of the commands. This project has various small options like commenting on task, creating new team, creating new board in team and etc. Every member who creates team becomes member of this team. There are two types of employees, with role Normal or SuperUser. All types of users must log in by username and password An SuperUser has some extra privilege including all privilege of a user. This commands only can be executed by owner (superuser):
* ChangeRole - Changes the role of user.
* EraseHistory - No parameters needed - deletes database.
* RemoveUser - Removes user from the application.
* RemoveTeam – Removes team from the application.
* ShowAllTeams - No parameters needed.
* ShowAllUsers - No parameters needed.

# Features
* Password and login functionality
* Role hierarchy
* Remove functionality
* Help command
* Colorful console
* Banner (Every time when you start the application new ascii art shows)
* Whitespaces between "(quotes) will be accepted as part of the parameter
* A primitive database, thanks to which the program recovers its state after closing
* Command that exports log history in txt formant to Desktop
* Command that deletes the database
* Export history to .txt file on desktop
* EraseHistory deletes the database
* Many others

# Contributing
[Telerik Academy](https://www.telerikacademy.com/) 

>Kalin Balimezov

>Georgi Petrov

