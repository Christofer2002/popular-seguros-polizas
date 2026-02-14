import axios from 'axios'
import { API_CONFIG } from '../config/api'
import { agregarInterceptorCamelCase } from '../utils/transformador'

const cliente = axios.create({
  baseURL: API_CONFIG.poliza,
  headers: {
    'Content-Type': 'application/json'
  }
})

// Interceptor para agregar token
cliente.interceptors.request.use(config => {
  const token = localStorage.getItem('token')
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
})

// Interceptor para transformar respuestas de PascalCase a camelCase
agregarInterceptorCamelCase(cliente)

export const obtenerCatalogos = async () => {
  try {
    const respuesta = await cliente.get('/catalogo')
    return respuesta.data
  } catch (error) {
    if (error.response && error.response.data) return Promise.reject(error.response.data)
    return Promise.reject({ mensaje: 'Error al obtener catálogos' })
  }
}
