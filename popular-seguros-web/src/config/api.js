const AMBIENTE = import.meta.env.MODE || 'development'

const URLS = {
  development: {
    cliente: 'https://localhost:44383',
    poliza: 'https://localhost:7075',
    autenticacion: 'https://localhost:7099'
  },
  production: {
    cliente: 'https://devbychris.com/popular-seguros-polizas',
    poliza: 'https://devbychris.com/popular-seguros-polizas',
    autenticacion: 'https://devbychris.com/popular-seguros-polizas'
  }
}

export const API_CONFIG = URLS[AMBIENTE] || URLS.development

export default API_CONFIG
