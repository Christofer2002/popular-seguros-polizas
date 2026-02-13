import React from 'react'
import { IconoAnterior, IconoSiguiente } from './Iconos'
import ButtonComponent from './ButtonComponent'
import '../styles/layout.css'

const PaginacionComponent = ({ 
  paginaActual, 
  totalPaginas, 
  onCambiarPagina 
}) => {
  return (
    <div className="paginacion">
      <ButtonComponent
        variante="paginacion"
        icono={<IconoAnterior />}
        onClick={() => onCambiarPagina(paginaActual - 1)}
        deshabilitado={paginaActual <= 1}
        titulo="Página anterior"
      />
      <span className="paginacion-info">
        Página {paginaActual} de {totalPaginas || 1}
      </span>
      <ButtonComponent
        variante="paginacion"
        icono={<IconoSiguiente />}
        onClick={() => onCambiarPagina(paginaActual + 1)}
        deshabilitado={paginaActual >= totalPaginas}
        titulo="Página siguiente"
      />
    </div>
  )
}

export default PaginacionComponent
