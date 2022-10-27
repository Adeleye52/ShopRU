# ShopRu UPL API

## Prerequisites
Before you begin, ensure you have installed the following
* Install the .NET Core `SDK 6`. Download link can be found [here](https://docs.microsoft.com/en-us/dotnet/core/install/sdk?pivots=os-windows) - Choose an operating system of your choice to download SDK.
* Install the `ASP.NET Core Runtime 6`. Download link can be found [here](https://dotnet.microsoft.com/download/dotnet-core) - Choose runtime for your OS

## Install development tools. 
Visual Studio 2022 community edition or Visaul Code is the preferred IDE
* Windows: Download `Viual Studio 2022` community or `Visual Studio Code` [here](https://visualstudio.microsoft.com/downloads/?utm_medium=microsoft&utm_source=docs.microsoft.com&utm_campaign=button+cta&utm_content=download+vs2019)
* Mac or Linux: Download `Visual Studio Code` [here](https://visualstudio.microsoft.com/downloads/?utm_medium=microsoft&utm_source=docs.microsoft.com&utm_campaign=button+cta&utm_content=download+vs2019) You can also use rider
* The installation guide for Visual Studio can be followed [here](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2019)
* The setting up of Visual Studio Code can be followed [here](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2019)


## Setup project

1. Clone the repository
	`git clone https://github.com/Prunedge-Dev-Team/stanbic-upl-api.git`
2. Create appsettings.Development.json and add the following 
3. 
   ```json
   {
        "ConnectionStrings": {
            "DefaultConnection": "Server=161.35.131.73,8033;Database=StanbicUplDb;User Id=sa;Password=yxH3nV9o4L;MultipleActiveResultSets=true"
        },
        "ModuleId": "UHJ1bmVFZGVnZV9VQVQ6MA==",
        "Authorization": "basic cmJ4RGV2ZWxvcGVyMjpyYjVkM3YzbDBwM3I=",
        "StanbicRedBox": {
            "Url": "https://dev.stanbicibtc.com:8443/uat/redbox/services/request-manager",
            "BvnBaseUrl": "https://dev.stanbicibtc.com:8443",
            "PrivateCreditCheckBaseUrl": "https://10.234.135.40:8443",
            "CbnCreditCheckUrl": "https://10.234.135.44:8443/uat/redbox/services/request-manager",
            "PathUrl": {
            "BvnApi": "uat/redbox/service/api/bvn/getbvndetails",
            "PrivateCreditCheckApi": "uat/redbox/v2/services/loans/worthiness"
            }
        },
        "SENDGRID_KEY": "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",
        "SENDER_EMAIL": "noreply@prunedge.com"
   }
   ```

4. `cd src.API`
5. Run `dotnet restore` to install package dependencies
6. Run `dotnet build` to build the application


## Database Migration
### Entity Framework Core tools
1.  `dotnet ef` must be installed as a global or local tool. Install `dotnet ef` as a global tool with the following command:
    `dotnet tool install --global dotnet-ef`
2. In the terminal change directory into the `API` project directory
3. Apply the migration to the database to create the schema.
    `dotnet ef database update`

Visit this link [here](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli) for more information on using `ef core tool`

## Running the application locally on your machine
Make sure all the above instructions are done completely before running the application
1. If you are using `Visual Studio`
    * Right click on the `API` project and select `Set as Startup Project`
    * Then press the `F5` to run the application or click on the `IIS Express` run button on the standard toolbar to run the application

2. If you are using `Visual Studio Code`
    * In your terminal, change directory into src/API directory where the solution file is located and then run the following: 
        `dotnet restore`
        `dotnet run`
    * The above commands will restore missing application packages, build and then run the application
3. Either ways will make the project web APIs accessible on Swagger through this `http://localhost:5000/swagger/index.html`

## Migrations 
```bash
cd src/API
dotnet ef migrations add [NextMigrationName]
dotnet ef migrations script [LastMigrationName] --idempotent
```

LastMigrationName = Initial
