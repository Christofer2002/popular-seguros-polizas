import axios from 'axios'
import { API_CONFIG } from '../../../config/api'
import { agregarInterceptorCamelCase } from '../../../utils/transformador'

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

export const obtenerPolizas = async (paginacion) => {
  try {
    const respuesta = await cliente.post('/api/poliza/filtros', paginacion)
    return respuesta.data
  } catch (error) {
    if (error.response && error.response.data) return Promise.reject(error.response.data)
    return Promise.reject({ mensaje: 'Error de conexión' })
  }
}

export const crearPoliza = async (datos) => {
  try {
    const respuesta = await cliente.post('/api/poliza', datos)
    return respuesta.data
  } catch (error) {
    if (error.response && error.response.data) return Promise.reject(error.response.data)
    return Promise.reject({ mensaje: 'Error al crear póliza' })
  }
}

export const actualizarPoliza = async (id, datos) => {
  try {
    const respuesta = await cliente.put(`/api/poliza/${id}`, datos)
    return respuesta.data
  } catch (error) {
    if (error.response && error.response.data) return Promise.reject(error.response.data)
    return Promise.reject({ mensaje: 'Error al actualizar póliza' })
  }
}

export const eliminarPoliza = async (id) => {
  try {
    const respuesta = await cliente.delete(`/api/poliza/${id}`)
    return respuesta.data
  } catch (error) {
    if (error.response && error.response.data) return Promise.reject(error.response.data)
    return Promise.reject({ mensaje: 'Error al eliminar póliza' })
  }
}
