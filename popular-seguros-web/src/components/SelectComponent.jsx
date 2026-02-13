import React from 'react'
import '../styles/components.css'

export default function SelectComponent({ 
  etiqueta, 
  nombre, 
  valor, 
  alCambiar, 
  opciones = [], 
  requerido = false, 
  placeholder = 'Seleccione...', 
  clase = '' 
}) {
  return (
    <div className={`campo-grupo ${clase}`}>
      {etiqueta && <label htmlFor={nombre} className="input-label">{etiqueta}</label>}
      <select
        id={nombre}
        name={nombre}
        value={valor}
        onChange={alCambiar}
        required={requerido}
        className="input-control"
      >
        <option value="">{placeholder}</option>
        {opciones.map((opcion, index) => (
          <option key={index} value={opcion.valor}>
            {opcion.etiqueta}
          </option>
        ))}
      </select>
    </div>
  )
}
