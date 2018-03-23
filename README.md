## Triplann API
## Author: John Dulaney
---

This repo is the home to the Triplann API. This c#/.net API works in tandem with the Client Application. Processing all CRUD operations sent to it. To Install the needed API, follow this short installation guide.

1. Download .net core [here.](https://www.microsoft.com/net/download/windows)

1. Clone the API repo in a seperate folder than the client side app
```
git clone git@github.com:john-dulaney/Triplann-API.git
```

3. Open your terminal, navigate to the API's folder, and run the following 2 commands:
```
dotnet ef migrations add <INSERT A NAME HERE>
```
then
```
dotnet ef database update
```

4. Assuming you didnt throw any errors during those 2 commands, you are ready to start the api up for use. Use the following command to start the API service:
```
dotnet run
```

Thats It! Start a new terminal window, and return to my client-side repo's readme [here.](https://github.com/john-dulaney/Triplann)
