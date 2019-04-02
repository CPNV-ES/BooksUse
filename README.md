# BooksUse
BookUse est un projet en ASP Net Core qui permet de gérer la liste des livres qui sont necessaires à la prochaine année scolaire.

## Exigences

* [Windows Sql Server](https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads)
* [Windows Sql Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017)
* [Visual Studio 2017](https://visualstudio.microsoft.com/fr/downloads/)
* [Net Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2)

> Attention, si Net Core 2.2 n'est pas installé, le projet ne fonctionnera pas

## Base de données

Dans un premier temps, il faut contrôler que les chemins inscrit dans le fichiers [BooksUse.sql](https://github.com/CPNV-ES/BooksUse/blob/master/doc/database/BooksUse.sql) (ligne 25, 28, 31, 38, 40) existe sur le système d'exploitation utilisé et que les droits d'écriture soit présents.

Par la suite, il faut ouvrir le fichier sql : [BooksUse.sql](https://github.com/CPNV-ES/BooksUse/blob/master/doc/database/BooksUse.sql) avec Sql Server Management Studio et l'éxécuter

Pour finir il faut remplir la base de données avec le [seeder](https://github.com/CPNV-ES/BooksUse/blob/master/doc/database/Seeder.sql)

## Ouvrir le projet

Executer cette commande

`git clone https://github.com/CPNV-ES/BooksUse.git` 

Puis ouvrir BooksUse.sln avec Visual Studio 2017

## Configuration

Dans le fichier [config.cs](https://github.com/CPNV-ES/BooksUse/blob/master/BooksUse/config.cs) qui se trouve à la racine du projet, on peut modifier l'utilisateur connecté