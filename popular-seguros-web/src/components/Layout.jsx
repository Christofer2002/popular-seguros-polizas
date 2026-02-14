import React, { useState } from 'react'
import { NavLink, useNavigate } from 'react-router-dom'
import { IconoClientes, IconoPolizas, IconoCerrarSesion } from './Iconos'
import ButtonComponent from './ButtonComponent'
import ModalConfirmacion from './ModalConfirmacion'
import '../styles/layout.css'

const Layout = ({ children }) => {
  const navigate = useNavigate()
  const [mostrarModalCerrarSesion, setMostrarModalCerrarSesion] = useState(false)

  const abrirModalCerrarSesion = () => {
    setMostrarModalCerrarSesion(true)
  }

  const cerrarModalCerrarSesion = () => {
    setMostrarModalCerrarSesion(false)
  }

  const confirmarCerrarSesion = () => {
    localStorage.removeItem('token')
    localStorage.removeItem('usuario')
    navigate('/popular-seguros-polizas/web/')
  }

  return (
    <div className="layout-container">
      <aside className="sidebar">
        <div className="sidebar-header">
          <img src={`${import.meta.env.BASE_URL}img/logo_login.png`} alt="Popular Seguros" className="sidebar-logo" />
        </div>
        
        <nav className="sidebar-nav">
          <NavLink to="/popular-seguros-polizas/web/clientes" className={({ isActive }) => `sidebar-link ${isActive ? 'activo' : ''}`}>
            <IconoClientes />
            <span>Clientes</span>
          </NavLink>
          <NavLink to="/popular-seguros-polizas/web/polizas" className={({ isActive }) => `sidebar-link ${isActive ? 'activo' : ''}`}>
            <IconoPolizas />
            <span>Pólizas</span>
          </NavLink>
        </nav>

        <div className="sidebar-footer">
          <ButtonComponent
            texto="Cerrar Sesión"
            icono={<IconoCerrarSesion />}
            variante="texto"
            clase="sidebar-link cerrar-sesion"
            onClick={abrirModalCerrarSesion}
          />
        </div>
      </aside>

      <main className="main-content">
        {children}
      </main>

      <ModalConfirmacion
        visible={mostrarModalCerrarSesion}
        titulo="Cerrar Sesión"
        mensaje="¿Está seguro que desea cerrar la sesión?"
        textoConfirmar="Cerrar Sesión"
        textoCancelar="Cancelar"
        onConfirmar={confirmarCerrarSesion}
        onCancelar={cerrarModalCerrarSesion}
        tipo="advertencia"
      />
    </div>
  )
}

export default Layout
