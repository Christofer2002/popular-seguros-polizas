import React from 'react'
import ButtonComponent from './ButtonComponent'
import '../styles/layout.css'

const IconoAdvertencia = () => (
  <svg width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
    <path d="M10.29 3.86L1.82 18a2 2 0 0 0 1.71 3h16.94a2 2 0 0 0 1.71-3L13.71 3.86a2 2 0 0 0-3.42 0z"></path>
    <line x1="12" y1="9" x2="12" y2="13"></line>
    <line x1="12" y1="17" x2="12.01" y2="17"></line>
  </svg>
)

const IconoPregunta = () => (
  <svg width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
    <circle cx="12" cy="12" r="10"></circle>
    <path d="M9.09 9a3 3 0 0 1 5.83 1c0 2-3 3-3 3"></path>
    <line x1="12" y1="17" x2="12.01" y2="17"></line>
  </svg>
)

const ModalConfirmacion = ({ 
  visible, 
  titulo = '¿Está seguro?',
  mensaje,
  onConfirmar,
  onCancelar,
  textoBtnConfirmar = 'Confirmar',
  textoBtnCancelar = 'Cancelar',
  tipo = 'pregunta', // 'pregunta' | 'advertencia' | 'peligro'
  cargando = false
}) => {
  if (!visible) return null

  const obtenerVariante = () => {
    switch (tipo) {
      case 'peligro':
        return 'peligro'
      default:
        return 'primario'
    }
  }

  return (
    <div className="modal-overlay" onClick={onCancelar}>
      <div className="modal-content modal-confirmacion-content" onClick={(e) => e.stopPropagation()}>
        <div className={`modal-confirmacion-icono modal-icono-${tipo}`}>
          {tipo === 'advertencia' || tipo === 'peligro' ? <IconoAdvertencia /> : <IconoPregunta />}
        </div>
        <h3 className="modal-confirmacion-titulo">{titulo}</h3>
        {mensaje && <p className="modal-confirmacion-mensaje">{mensaje}</p>}
        <div className="modal-footer modal-footer-center">
          <ButtonComponent 
            texto={textoBtnCancelar}
            variante="secundario"
            onClick={onCancelar}
            deshabilitado={cargando}
          />
          <ButtonComponent 
            texto={cargando ? 'Procesando...' : textoBtnConfirmar}
            variante={obtenerVariante()}
            onClick={onConfirmar}
            deshabilitado={cargando}
          />
        </div>
      </div>
    </div>
  )
}

export default ModalConfirmacion
