import React from 'react'
import ButtonComponent from './ButtonComponent'
import '../styles/layout.css'

export default function ModalComponent({ 
  visible, 
  titulo, 
  onCerrar, 
  children, 
  footer 
}) {
  if (!visible) return null

  return (
    <div className="modal-overlay" onClick={onCerrar}>
      <div className="modal-content" onClick={e => e.stopPropagation()}>
        <div className="modal-header">
          <h3 className="modal-title">{titulo}</h3>
          <ButtonComponent
            variante="cerrar-modal"
            texto="×"
            onClick={onCerrar}
          />
        </div>
        <div className="modal-body">
          {children}
        </div>
        {footer && (
          <div className="modal-footer">
            {footer}
          </div>
        )}
      </div>
    </div>
  )
}
