# GameGauge

# Application Setup

1. Pull the "main" branch of the source code repository at the following link:
   [GameGauge on GitHub](https://github.com/zachauker/GameGauge/)

2. If not already installed on your machine, install Docker Desktop which can be found here:
   [Docker Desktop](https://www.docker.com/products/docker-desktop/)

3. Within the source code directory, run the following docker command to pull the latest PostgreSQL Docker image and create a new database for the application:


    - This will download the PostgreSQL image, create a new database with the username `admin` and run it exposed on port `5432` (the default Postgres port).

4. With the database container running, navigate to the source code directory and run the following command to run the database migrations:


    - This creates the database schema and you should see these changes reflected in your database GUI of choice after running the command.

5. Now navigate to the API project directory and run the following commands to seed the database with the required data:

    ```dotnet run AgeRatingSeed
    dotnet run CompanySeed
    dotnet run EngineSeed
    dotnet run GenreSeed
    dotnet run PlatformSeed
    dotnet run GameSeed
    dotnet run ReleaseDateSeed
    dotnet run MediaSeed
    dotnet run GameRelationSeed
    ```


    - Each of these commands will take some time as there are hundreds of thousands of rows of data to retrieve and persist to the database.
    - The commands must be run one at a time so as to not start parallel threads on the Data Context which will cause an exception to be thrown.

6. Once the database is successfully seeded with the required data you can now run the following command to start the application. This will again be ran from the API project directory:

    ```npm install```


9. After the packages have been successfully installed, run the following command to start the front-end web application:

    ```npm run dev```

10. With this running, the application is now accessible and can be navigated to at the following URL:
 [http://localhost:3000](http://localhost:3000)


10. With this running, the application is now accessible and can be navigated to at the following URL:
 [http://localhost:3000](http://localhost:3000)
