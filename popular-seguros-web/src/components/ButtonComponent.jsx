import React from 'react'
import '../styles/components.css'

const ButtonComponent = ({ 
  texto, 
  tipo = 'button', 
  onClick, 
  clase = '', 
  deshabilitado = false,
  variante = 'primario',
  icono = null,
  titulo = ''
}) => {
  const obtenerClaseVariante = () => {
    switch (variante) {
      case 'secundario':
        return 'btn-secondary-custom'
      case 'limpiar':
        return 'btn-limpiar-custom'
      case 'peligro':
        return 'btn-peligro-custom'
      case 'icono-editar':
        return 'btn-accion-icono btn-editar'
      case 'icono-eliminar':
        return 'btn-accion-icono btn-eliminar'
      case 'texto':
        return 'btn-texto'
      case 'paginacion':
        return 'paginacion-btn paginacion-btn-icono'
      case 'cerrar-modal':
        return 'modal-close'
      default:
        return 'btn-primary-custom'
    }
  }

  return (
    <button 
      type={tipo} 
      onClick={onClick} 
      className={`${obtenerClaseVariante()} ${clase}`}
      disabled={deshabilitado}
      title={titulo}
    >
      {icono && <span className="btn-icono">{icono}</span>}
      {texto && <span>{texto}</span>}
    </button>
  )
}

export default ButtonComponent
