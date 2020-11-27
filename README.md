# Multi-Threaded TCP Server and Client



## Summary

* This example illustrates how a multi-threaded TCP Server listens and accepts socket connections from clients. Two clients each connects to the TCP server and send hello message two times. TCP server responds to each message by sending welcome message.
* This example assumes that a message is less than 1024 bytes.



## Environment

* Runtime: .NET Core 3.1
* Language: C#
* IDE: VSCode



## How to run the program

* Make sure you have dotnet 3.1 or up.
* In the project directory, open up a command prompt and run  `dotnet run server`.
* Open up another command prompt and run `dotnet run client`.
* Observe how the server and clients communicate with each other.