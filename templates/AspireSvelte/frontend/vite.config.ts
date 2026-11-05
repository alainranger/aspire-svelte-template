import { svelte } from '@sveltejs/vite-plugin-svelte'
import { defineConfig } from 'vite'

export default defineConfig({
  plugins: [svelte()],
  server: {
    host: '0.0.0.0',
    port: Number(process.env['PORT'] ?? 5173),
    strictPort: true,
    proxy: {
      '/api': {
        target: process.env['services__api__http__0'] ?? 'http://localhost:5000',
        changeOrigin: true,
      },
    },
  },
})
