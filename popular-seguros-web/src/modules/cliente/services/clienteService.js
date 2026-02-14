import axios from 'axios'
import { API_CONFIG } from '../../../config/api'
import { agregarInterceptorCamelCase } from '../../../utils/transformador'

const cliente = axios.create({
  baseURL: API_CONFIG.cliente,
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

export const obtenerClientes = async (paginacion) => {
  try {
    const respuesta = await cliente.post('/api/Cliente/filtros', paginacion)
    return respuesta.data
  } catch (error) {
    if (error.response && error.response.data) return Promise.reject(error.response.data)
    return Promise.reject({ mensaje: 'Error de conexión' })
  }
}

export const crearCliente = async (datos) => {
  try {
    const respuesta = await cliente.post('/api/Cliente', datos)
    return respuesta.data
  } catch (error) {
    if (error.response && error.response.data) return Promise.reject(error.response.data)
    return Promise.reject({ mensaje: 'Error al crear cliente' })
  }
}

export const actualizarCliente = async (cedula, datos) => {
  try {
    const respuesta = await cliente.put(`/api/Cliente/${cedula}`, datos)
    return respuesta.data
  } catch (error) {
    if (error.response && error.response.data) return Promise.reject(error.response.data)
    return Promise.reject({ mensaje: 'Error al actualizar cliente' })
  }
}

export const eliminarCliente = async (cedula) => {
  try {
    const respuesta = await cliente.delete(`/api/Cliente/${cedula}`)
    return respuesta.data
  } catch (error) {
    if (error.response && error.response.data) return Promise.reject(error.response.data)
    return Promise.reject({ mensaje: 'Error al eliminar cliente' })
  }
}
