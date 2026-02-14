import React, { useState } from 'react'
import InputComponent from '../components/InputComponent'
import ButtonComponent from '../components/ButtonComponent'
import { iniciarSesion } from '../services/authService'
import '../styles/auth.css'
import { useNavigate } from 'react-router-dom'

export default function Login() {
  const [credenciales, setCredenciales] = useState({ nombreUsuario: '', contrasena: '' })
  const [error, setError] = useState('')
  const [cargando, setCargando] = useState(false)
  const navegar = useNavigate()

  function manejarCambio(e) {
    const { name, value } = e.target
    setCredenciales(prev => ({ ...prev, [name]: value }))
  }

  async function manejarEnvio(e) {
    e.preventDefault()
    setError('')
    if (!credenciales.nombreUsuario || !credenciales.contrasena) {
      setError('Usuario y contraseña son requeridos.')
      return
    }

    setCargando(true)
    try {
      const respuesta = await iniciarSesion(credenciales)
      if (respuesta && respuesta.exito && respuesta.data) {
        localStorage.setItem('usuario', JSON.stringify(respuesta.data))
        navegar('/popular-seguros-polizas/web/clientes')
      } else {
        setError(respuesta?.mensaje || 'Error al iniciar sesión')
      }
    } catch (err) {
      if (err.errores && err.errores.length > 0) {
        setError(err.errores.join(', '))
      } else {
        setError(err.mensaje || 'Credenciales inválidas')
      }
    } finally {
      setCargando(false)
    }
  }

  return (
    <div className="auth-viewport">
      <form className="auth-card" onSubmit={manejarEnvio}>
        <div className="auth-logo">
          <img src={`${import.meta.env.BASE_URL}img/logo_login.png`} alt="Popular Seguros" className="auth-logo-img" />
        </div>
        <h2 className="auth-title">Bienvenido</h2>
        <p className="auth-subtitle">Ingrese sus credenciales para continuar</p>
        {error && <div className="auth-error">{error}</div>}
        <InputComponent 
          etiqueta="Usuario" 
          nombre="nombreUsuario" 
          valor={credenciales.nombreUsuario} 
          alCambiar={manejarCambio} 
          requerido 
          placeholder="Ingrese su usuario" 
        />
        <InputComponent 
          etiqueta="Contraseña" 
          nombre="contrasena" 
          tipo="password" 
          valor={credenciales.contrasena} 
          alCambiar={manejarCambio} 
          requerido 
          placeholder="Ingrese su contraseña" 
        />
        <div className="mt-4" style={{ display: 'flex', justifyContent: 'center' }}>
          <ButtonComponent 
            tipo="submit" 
            texto={cargando ? 'Ingresando...' : 'Ingresar'} 
            deshabilitado={cargando}
          />
        </div>
        <div className="auth-footer">
          <p className="auth-footer-text">Popular Seguros &copy; 2026</p>
        </div>
      </form>
    </div>
  )
}
