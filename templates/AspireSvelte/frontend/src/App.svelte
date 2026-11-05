<script lang="ts">
  let message = $state('Cliquez pour tester la connexion avec l’API.')
  let isLoading = $state(false)

  async function loadGreeting() {
    isLoading = true

    try {
      const response = await fetch('/api/hello')
      if (!response.ok) {
        throw new Error(`L’API a répondu avec le statut ${response.status}.`)
      }

      const result: { message: string } = await response.json()
      message = result.message
    } catch (error) {
      message =
        error instanceof Error
          ? error.message
          : 'Impossible de joindre l’API.'
    } finally {
      isLoading = false
    }
  }
</script>

<svelte:head>
  <meta
    name="description"
    content="Un template de démarrage .NET Aspire avec API ASP.NET Core et frontend Svelte."
  />
</svelte:head>

<main>
  <div class="eyebrow">.NET Aspire · ASP.NET Core · Svelte 5</div>
  <h1>Bienvenue dans <span>AspireSvelte</span></h1>
  <p class="intro">
    Votre projet est prêt. L’AppHost Aspire orchestre l’API et cette interface.
  </p>

  <section class="api-card" aria-labelledby="api-title">
    <div>
      <h2 id="api-title">Vérifier l’API</h2>
      <p class="message" aria-live="polite">{message}</p>
    </div>
    <button onclick={loadGreeting} disabled={isLoading}>
      {isLoading ? 'Connexion…' : 'Appeler /api/hello'}
    </button>
  </section>
</main>
