# Ignite Your Tech Sparks ❇️🎆✨🎇✴️
CodeSparks helps to transform ideas into reality through collaboration with people, programs, and AI.
It is designed to empower you with your tech journey. ‍ Whether you're a curious beginner or a seasoned professional looking to explore new horizons, CodeSparks provides an open and engaging environment to develop your tech skills.

## Future vision
* Interactive Learning: Master tech concepts through interactive lessons, puzzles, and challenges.
* Gamified Experience: Earn points, badges, and climb the leaderboard to stay motivated and engaged.
* Personalized Learning Paths: Explore curated learning paths tailored to your skill level and interests.
* Supportive Community: Connect with fellow sparkers, share experiences, and learn from each other.

## Contact
Telegram chat: https://t.me/codesparks



## Build, run, contributing
### How to run
The project in this repo is ASP.NET Core app, so the only prerequisite is .NET 8 installed. Just use typical commands to run the project:
`dotnet build`
`dotnet run`
or open folder/solution in your favourite IDE and run it.

### How to run with database
If you would like to have a full dev experience - you also need to configure database.
You need to do 3 simple things to configure database:
1. Create a database in PostgreSQL.
You need to [install PostgreSQL](https://www.postgresql.org/download/) for this. It's pretty cool database, which is usually supported fast for new version of EF Core. But you can use any database, in that case - you will need to change migration scripts. So, it may be easier to install Postgres.
   
2. Check the appsettings.json file and change the connection string to match the database that you plan to user and run migrations.
Or, even better - change your [personal secrest.json](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows) file and add there something like:
```json
{
  "ConnectionStrings:PostgresConnection": "Host=dpg-cq9mcrdds78s739fng50-a.oregon-postgres.render.com;Port=5432;Database=sql_db_s0e5;Username=sql_db_s0e5_user;Password=FObdGBdZ4V9iJZcY7BCEQ6Xj0zJlArBI"
}
```

3. [Apply migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli) with
```markdown
dotnet ef database update
```

### Contributing
We welcome contributions! If you're passionate about tech education and want to help us make CodeSparks even better, feel free to:
- Report Issues: If you encounter any bugs or issues, please report them through the GitHub issue tracker.
- Suggest Improvements: Share your ideas and suggestions for enhancing the platform.
- Create Pull Requests: If you have coding skills, contribute bug fixes or new features through pull requests.

And of course suggest your ideas in the [issues](https://github.com/CodeSparks-org/CodeSparks/issues) or on [codesparks.org](https://codesparks.org/Sparks/Create?category=Idea)

### Another question?
Just add another item in the [issues](https://github.com/CodeSparks-org/CodeSparks/issues) 
