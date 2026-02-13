import React from 'react'
import '../styles/components.css'

export default function InputComponent({ etiqueta, nombre, tipo = 'text', valor, alCambiar, requerido = false, placeholder = '', maxLength, clase = '', deshabilitado = false }) {
  return (
    <div className={`campo-grupo ${clase}`}>
      {etiqueta && <label htmlFor={nombre} className="input-label">{etiqueta}</label>}
      <input
        id={nombre}
        name={nombre}
        type={tipo}
        value={valor}
        onChange={alCambiar}
        required={requerido}
        placeholder={placeholder}
        maxLength={maxLength}
        className="input-control"
        disabled={deshabilitado}
      />
    </div>
  )
}
