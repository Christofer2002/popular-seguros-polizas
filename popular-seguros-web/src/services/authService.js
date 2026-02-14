import axios from 'axios'
import { API_CONFIG } from '../config/api'
import { agregarInterceptorCamelCase } from '../utils/transformador'

const cliente = axios.create({
  baseURL: API_CONFIG.autenticacion,
  headers: {
    'Content-Type': 'application/json'
  }
})

// Interceptor para transformar respuestas de PascalCase a camelCase
agregarInterceptorCamelCase(cliente)

export async function iniciarSesion(datos) {
  try {
    const respuesta = await cliente.post('/api/Autenticacion/login', {
      nombreUsuario: datos.nombreUsuario,
      contraseña: datos.contrasena
    })
    return respuesta.data
  } catch (error) {
    if (error.response && error.response.data) return Promise.reject(error.response.data)
    return Promise.reject({ mensaje: 'Error de conexión' })
  }
}

export async function verificarDisponibilidadUsuario(nombreUsuario) {
  try {
    const respuesta = await cliente.get(`/api/Autenticacion/verificar-usuario/${nombreUsuario}`)
    return respuesta.data
  } catch (error) {
    if (error.response && error.response.data) return Promise.reject(error.response.data)
    return Promise.reject({ mensaje: 'Error al verificar usuario' })
  }
}
