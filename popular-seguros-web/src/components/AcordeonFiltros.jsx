import React, { useState, useRef, useEffect } from 'react'
import { IconoChevronAbajo, IconoFiltro, IconoBuscar, IconoLimpiar } from './Iconos'
import ButtonComponent from './ButtonComponent'
import '../styles/layout.css'

const AcordeonFiltros = ({ 
  titulo = 'Filtros de Búsqueda',
  children,
  inicialmenteAbierto = false,
  onBuscar,
  onLimpiar,
  cargando = false
}) => {
  const [abierto, setAbierto] = useState(inicialmenteAbierto)
  const contenidoRef = useRef(null)
  const [alturaContenido, setAlturaContenido] = useState(0)

  useEffect(() => {
    if (contenidoRef.current) {
      setAlturaContenido(contenidoRef.current.scrollHeight)
    }
  }, [children, abierto])

  const alternarAcordeon = () => {
    setAbierto(prev => !prev)
  }

  return (
    <div className={`acordeon-filtros ${abierto ? 'abierto' : ''}`}>
      <ButtonComponent
        tipo="button"
        clase="acordeon-header"
        onClick={alternarAcordeon}
        variante="secundario"
        texto={null}
        icono={
          <>
            <div className="acordeon-titulo">
              <IconoFiltro />
              <span>{titulo}</span>
            </div>
            <span className={`acordeon-icono ${abierto ? 'rotado' : ''}`}>
              <IconoChevronAbajo />
            </span>
          </>
        }
      />
      <div 
        className="acordeon-contenido"
        style={{ 
          maxHeight: abierto ? `${alturaContenido + 100}px` : '0px'
        }}
      >
        <div className="acordeon-body" ref={contenidoRef}>
          <div className="filtros-grid">
            {children}
          </div>
          {(onBuscar || onLimpiar) && (
            <div className="filtros-acciones">
              {onLimpiar && (
                <ButtonComponent
                  texto="Limpiar"
                  variante="limpiar"
                  onClick={onLimpiar}
                  deshabilitado={cargando}
                  icono={<IconoLimpiar />}
                />
              )}
              {onBuscar && (
                <ButtonComponent
                  texto={cargando ? 'Buscando...' : 'Buscar'}
                  variante="primario"
                  onClick={onBuscar}
                  deshabilitado={cargando}
                  icono={<IconoBuscar />}
                />
              )}
            </div>
          )}
        </div>
      </div>
    </div>
  )
}

export default AcordeonFiltros
