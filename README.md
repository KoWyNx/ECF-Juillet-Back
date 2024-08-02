
# Quiz Application Backend

Ce projet constitue le backend de l'application de quiz, développé en ASP.NET Core. Il expose une API RESTful pour gérer les questions du quiz et les scores des joueurs. Cette documentation détaille également les configurations clés, y compris le scaffolding, l'injection de dépendances et la configuration du service.

## Prérequis

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) ou supérieur
- Un serveur de base de données compatible avec Entity Framework Core (ex: SQL Server)

## Installation

### Configuration de la Base de Données

Ce projet utilise SQL Server pour le stockage des données. Assurez-vous que votre serveur SQL est opérationnel et accessible.

1. Dans le fichier `appsettings.json`, configurez la chaîne de connexion pour pointer vers votre instance SQL Server. Voici un exemple de configuration :

    ```json
    "ConnectionStrings": {
      "MyDbConnection": "Server=srv2024.ddns.net,36494;Database=QuizAventureDB;User Id=ReadOnlyUser;Password=StrongPassword!;Encrypt=False;"
    }
    ```

2. Configurez le DbContext dans le fichier `Program.cs` pour qu'il utilise cette chaîne de connexion :

    ```csharp
    builder.Services.AddDbContext<MyDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnection"));
    });
    ```

3. Après avoir configuré la chaîne de connexion et le DbContext, exécutez le scaffolding pour générer les modèles et le DbContext :

    ```bash
    dotnet ef dbcontext scaffold Name=MyDbConnection Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context-dir Context --context MyDbContext --force
    ```

### Installation des Dépendances

Assurez-vous d'avoir restauré toutes les dépendances nécessaires avant de lancer l'application. Pour ce faire, exécutez la commande suivante dans le répertoire racine du projet :

```bash
dotnet restore
```

### Configuration CORS

Pour permettre au frontend (par exemple, une application React) de communiquer avec ce backend, configurez CORS dans le fichier `Program.cs` :

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Remplacez par l'URL de votre frontend
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
```

N'oubliez pas d'activer la politique CORS configurée dans votre pipeline de requêtes HTTP :

```csharp
app.UseCors("AllowReactApp");
```

## Exécution

### Lancer l'Application

Une fois que tout est configuré, vous pouvez lancer l'application en utilisant la commande suivante :

```bash
dotnet run ou le déboguer
```


L'API sera disponible sur `https://localhost:7014/api/quiz`.

## API Endpoints

L'API expose plusieurs endpoints pour interagir avec les questions et les scores des joueurs. Voici un aperçu :

- **GET** `/api/quiz/questions` : Retourne la liste de toutes les questions.
- **GET** `/api/quiz/questions/{questionId}` : Retourne une question spécifique par son ID.
- **POST** `/api/quiz/player-score` : Soumet un score pour un joueur.
- **GET** `/api/quiz/player-scores` : Retourne la liste des scores des joueurs.

## Structure du Projet

Le projet est organisé comme suit :

```plaintext
backend/
├── Controllers/
│   └── QuizController.cs
├── Models/
│   ├── PlayerScore.cs
│   ├── Question.cs
│   └── QuestionOption.cs
├── Services/
│   ├── IQuizService.cs
│   └── QuizService.cs
├── Context/
│   └── MyDbContext.cs
├── Program.cs
└── appsettings.json
```

## Configuration des Services

### Injection de Dépendances

L'injection de dépendances est configurée dans le fichier `Program.cs`. Voici comment les services principaux sont enregistrés :

```csharp
builder.Services.AddScoped<IQuizService, QuizService>();
```

Cela permet d'injecter `IQuizService` dans vos contrôleurs, où une instance de `QuizService` sera fournie automatiquement.

## Dépendances

Les principales dépendances du projet incluent :

- **ASP.NET Core** : Pour construire l'API RESTful.
- **Entity Framework Core** : Pour l'accès aux données et l'ORM.
- **SQL Server** : Pour le stockage des données.



