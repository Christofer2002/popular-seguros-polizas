import React from 'react'
import { IconoSinDatos } from './Iconos'
import '../styles/layout.css'

const TablaComponent = ({ 
  columnas = [], 
  datos = [], 
  cargando = false,
  acciones,
  mensajeVacio = 'No hay registros disponibles'
}) => {
  if (cargando) {
    return (
      <div className="loading">
        <div className="spinner"></div>
        <p>Cargando...</p>
      </div>
    )
  }

  if (datos.length === 0) {
    return (
      <div className="sin-datos">
        <div className="sin-datos-icon"><IconoSinDatos /></div>
        <p>{mensajeVacio}</p>
      </div>
    )
  }

  return (
    <div className="tabla-container">
      <table className="tabla">
        <thead>
          <tr>
            {acciones && <th className="th-acciones">Acciones</th>}
            {columnas.map((col, index) => (
              <th key={index}>{col.titulo}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          {datos.map((fila, filaIndex) => (
            <tr key={filaIndex}>
              {acciones && (
                <td className="tabla-acciones">
                  {acciones(fila)}
                </td>
              )}
              {columnas.map((col, colIndex) => (
                <td key={colIndex}>
                  {col.render ? col.render(fila[col.campo], fila) : fila[col.campo]}
                </td>
              ))}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}

export default TablaComponent
