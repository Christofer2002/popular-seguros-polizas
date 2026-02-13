import React from 'react'
import ButtonComponent from './ButtonComponent'
import '../styles/layout.css'

const IconoError = () => (
  <svg width="48" height="48" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
    <circle cx="12" cy="12" r="10"></circle>
    <line x1="15" y1="9" x2="9" y2="15"></line>
    <line x1="9" y1="9" x2="15" y2="15"></line>
  </svg>
)

const ModalError = ({ 
  visible, 
  mensaje, 
  errores = [],
  onCerrar,
  titulo = 'Ha ocurrido un error'
}) => {
  if (!visible) return null

  const tieneErrores = errores && errores.length > 0

  return (
    <div className="modal-overlay" onClick={onCerrar}>
      <div className="modal-content modal-error-content" onClick={(e) => e.stopPropagation()}>
        <div className="modal-error-icono">
          <IconoError />
        </div>
        <h3 className="modal-error-titulo">{titulo}</h3>
        {tieneErrores ? (
          <ul className="modal-error-lista">
            {errores.map((error, index) => (
              <li key={index} className="modal-error-item">{error}</li>
            ))}
          </ul>
        ) : (
          <p className="modal-error-mensaje">{mensaje}</p>
        )}
        <div className="modal-footer modal-footer-center">
          <ButtonComponent texto="Aceptar" onClick={onCerrar} />
        </div>
      </div>
    </div>
  )
}

export default ModalError
