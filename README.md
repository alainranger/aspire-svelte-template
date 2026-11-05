# Template .NET Aspire + Svelte

Ce dépôt public contient un template `dotnet new` qui génère une solution .NET 10 Aspire avec :

- un AppHost Aspire qui orchestre les services ;
- une API ASP.NET Core avec OpenAPI et les valeurs par défaut Aspire ;
- une SPA Svelte 5 + TypeScript + Vite, configurée pour relayer `/api` vers l'API.

## Prérequis

- .NET 10 SDK ;
- Node.js 20.19+ ou 22.12+ et npm ;
- Docker uniquement si les services ajoutés au projet en ont besoin.

## Installer et utiliser le template depuis les sources

```sh
dotnet new install ./templates/AspireSvelte
dotnet new aspire-svelte -n MonProjet
cd MonProjet
cd frontend && npm install && cd ..
dotnet run --project MonProjet.AppHost
```

Le tableau de bord Aspire affiche les URL des ressources. La page d'accueil fournit un bouton qui appelle `GET /api/hello` à travers le proxy Vite.

Pour désinstaller le template local :

```sh
dotnet new uninstall ./templates/AspireSvelte
```

## Construire et installer le package

```sh
dotnet pack AspireSvelte.Templates.csproj --output ./artifacts
dotnet new install ./artifacts/AspireSvelte.Templates.1.0.0.nupkg
```

## Publication sur GitHub Packages

GitHub Actions compile et teste le template sur chaque push vers `main`. La publication vers GitHub Packages se fait uniquement à partir d’un tag versionné :

```sh
git tag v1.0.0
git push origin v1.0.0
```

Le tag `v1.0.0` publie `AspireSvelte.Templates` en version `1.0.0`. Utilisez un nouveau numéro SemVer pour chaque publication (par exemple `v1.0.1`). Le workflow utilise `GITHUB_TOKEN` ; aucun secret NuGet n’est à configurer.

Après la première publication, rendez le package public dans GitHub : ouvrez **Packages** sur le profil `alainranger`, sélectionnez `AspireSvelte.Templates`, puis **Package settings → Change visibility → Public**. La visibilité du package est distincte de celle du dépôt.

Installer le package publié :

```sh
dotnet nuget add source "https://nuget.pkg.github.com/alainranger/index.json" \
  --name github \
  --username alainranger \
  --password "$GITHUB_PACKAGES_TOKEN" \
  --store-password-in-clear-text
dotnet new install AspireSvelte.Templates::1.0.0 --nuget-source github
```

`GITHUB_PACKAGES_TOKEN` doit être un Personal Access Token (classic) avec la permission `read:packages`. GitHub Packages demande une authentification NuGet même pour télécharger un package public.

Pour mettre à jour une installation locale après publication d’une nouvelle version :

```sh
dotnet new update
```

La solution générée est une SPA (pas du rendu serveur SvelteKit) : le backend ASP.NET Core fournit l'API et Vite sert le frontend en développement.
