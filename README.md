📌 Projet Fil Rouge : Gestionnaire de Mots de Passe
Description

Ce projet est une application Blazor permettant aux étudiants en enseignement supérieur de gérer leurs mots de passe en toute sécurité. Il offre une interface intuitive et des fonctionnalités avancées comme le chiffrement, la gestion des catégories et un générateur de mots de passe sécurisé.
📂 Fonctionnalités Implémentées

✅ Authentification (3 pts)

    Inscription et connexion des utilisateurs avec gestion des sessions.

✅ Gestion des mots de passe (2 pts)

    Ajout, modification et suppression de mots de passe.

✅ Catégorisation (2 pts)

    Organisation des mots de passe par catégorie.

✅ Recherche rapide (2 pts)

    Barre de recherche permettant de retrouver rapidement un mot de passe par titre.

✅ Chiffrement (3 pts)

    Les mots de passe sont chiffrés avant stockage en base de données.

✅ Générateur de mots de passe (2 pts)

    Génération de mots de passe sécurisés avec options de personnalisation (exclusion des chiffres, caractères spéciaux, etc.).

✅ Interface utilisateur (4 pts)

    Interface responsive et conviviale basée sur MudBlazor.
    Tableau interactif avec filtres et actions rapides (édition, suppression, affichage du mot de passe).

⏳ Fonctionnalités Non Implémentées (ou partiellement)

❌ Mode hors ligne (2 pts)

    Accès local sécurisé sans dépendance réseau non implémenté.

⚠ Sécurité avancée (3 pts)

    Mise en place d’un mot de passe principal et verrouillage après tentatives échouées partiellement implémenté.

⚠ Sauvegarde sur SQLite (5 pts)

    Utilisation d’une base de données SQLite en cours d’intégration.

❌ Tests unitaires et Application mobile (5 pts bonus)

    Tests unitaires et version mobile non encore développés.

🛠 Technologies Utilisées

    Frontend : Blazor avec MudBlazor pour l’UI.
    Backend : ASP.NET Core Web API.
    Base de données : SQLite.
    Sécurité : Chiffrement des mots de passe.
    Architecture : Entity Framework Core, Dependency Injection.

📁 Structure du Projet

.
├── src
│   ├── PasswordManager.Web      # Projet Blazor (Frontend)
│   ├── PasswordManager.Api      # API Backend
│   └── PasswordManager.Core     # Logique métier
├── tests
│   └── PasswordManager.Tests    # Tests unitaires et d'intégration (à implémenter)
└── README.md                    # Documentation du projet

🚀 Déploiement et Exécution
📌 Prérequis

    .NET 9 SDK installé.
    Visual Studio 2022 ou JetBrains Rider.
    Git pour la gestion du code source.

💻 Instructions d'installation

    Cloner le dépôt :

git clone https://github.com/votre-repo/password-manager.git
cd password-manager

Restaurer les dépendances :

dotnet restore

Exécuter le projet :

    dotnet run --project src/PasswordManager.Web

👤 Auteur

    [Votre Nom] - Étudiant(e) en développement logiciel.

📜 Licence

Ce projet est sous licence MIT - voir le fichier LICENSE pour plus de détails.
📩 Rendu du projet

🔗 Lien GitHub : (à ajouter)
📧 Envoi du projet avant le 28 Février 2025 à 23H59 à : theojulien.pro@outlook.com
📚 Liens Utiles

    📖 Documentation Blazor
    📖 Entity Framework Core
    📖 SQLite
