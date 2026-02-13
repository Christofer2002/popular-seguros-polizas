import React, { useState, useEffect } from 'react'
import Layout from '../../components/Layout'
import ButtonComponent from '../../components/ButtonComponent'
import TablaComponent from '../../components/TablaComponent'
import ModalComponent from '../../components/ModalComponent'
import ModalConfirmacion from '../../components/ModalConfirmacion'
import ModalError from '../../components/ModalError'
import PaginacionComponent from '../../components/PaginacionComponent'
import AcordeonFiltros from '../../components/AcordeonFiltros'
import { IconoEditar, IconoEliminar } from '../../components/Iconos'
import FormularioPoliza from './components/FormularioPoliza'
import FiltrosPoliza from './components/FiltrosPoliza'
import { PolizaModel, FiltrosPolizaModel } from './models/PolizaModel'
import { obtenerPolizas, crearPoliza, actualizarPoliza, eliminarPoliza } from './services/polizaService'
import { obtenerCatalogos } from '../../services/catalogoService'
import { notificacionExito } from '../../services/notificacionService'
import '../../styles/layout.css'

const polizaInicial = new PolizaModel()
const filtrosIniciales = new FiltrosPolizaModel()

export const Poliza = () => {
  const [polizas, setPolizas] = useState([])
  const [cargando, setCargando] = useState(false)
  const [paginacion, setPaginacion] = useState({ pagina: 1, cantidadRegistros: 10 })
  const [totalPaginas, setTotalPaginas] = useState(1)
  
  const [tiposPoliza, setTiposPoliza] = useState([])
  const [estadosPoliza, setEstadosPoliza] = useState([])
  const [coberturas, setCoberturas] = useState([])
  
  const [filtros, setFiltros] = useState(new FiltrosPolizaModel())
  const [buscando, setBuscando] = useState(false)
  
  const [modalVisible, setModalVisible] = useState(false)
  const [modoEdicion, setModoEdicion] = useState(false)
  const [polizaActual, setPolizaActual] = useState(new PolizaModel())
  const [guardando, setGuardando] = useState(false)
  const [errorFormulario, setErrorFormulario] = useState('')

  const [modalErrorVisible, setModalErrorVisible] = useState(false)
  const [mensajeError, setMensajeError] = useState('')
  const [listaErrores, setListaErrores] = useState([])

  const [confirmarEliminar, setConfirmarEliminar] = useState(null)
  const [confirmarCerrarModal, setConfirmarCerrarModal] = useState(false)
  const [confirmarGuardar, setConfirmarGuardar] = useState(false)

  const columnas = [
    { titulo: 'N° Póliza', campo: 'numeroPoliza' },
    { titulo: 'Tipo', campo: 'tipoPoliza', render: (_, fila) => fila.tipoPoliza?.nombre || '-' },
    { titulo: 'Cédula Asegurado', campo: 'cedulaAsegurado' },
    { 
      titulo: 'Monto Asegurado', 
      campo: 'montoAsegurado',
      render: (valor) => valor ? `₡${Number(valor).toLocaleString('es-CR')}` : '-'
    },
    { titulo: 'Cobertura', campo: 'tipoCobertura', render: (_, fila) => fila.tipoCobertura?.nombre || '-' },
    { 
      titulo: 'Estado', 
      campo: 'estadoPoliza',
      render: (_, fila) => {
        const estado = fila.estadoPoliza?.nombre || '-'
        return (
          <span className={`estado-badge estado-${estado?.toLowerCase()}`}>
            {estado}
          </span>
        )
      }
    },
    { 
      titulo: 'Vencimiento', 
      campo: 'fechaVencimiento',
      render: (valor) => valor ? new Date(valor).toLocaleDateString('es-CR') : '-'
    }
  ]

  useEffect(() => {
    cargarCatalogos()
  }, [])

  useEffect(() => {
    cargarPolizas()
  }, [paginacion.pagina])

  const mostrarError = (mensaje, errores = []) => {
    setMensajeError(mensaje)
    setListaErrores(errores)
    setModalErrorVisible(true)
  }

  const cerrarModalError = () => {
    setModalErrorVisible(false)
    setMensajeError('')
    setListaErrores([])
  }

  const cargarCatalogos = async () => {
    try {
      const respuesta = await obtenerCatalogos()
      
      if (respuesta.exito && respuesta.data) {
        const { tiposPoliza: tipos, estadosPoliza: estados, tiposCoberturas: coberturasList } = respuesta.data
        
        setTiposPoliza(tipos?.map(t => ({ valor: t.id, etiqueta: t.nombre })) || [])
        setEstadosPoliza(estados?.map(e => ({ valor: e.id, etiqueta: e.nombre })) || [])
        setCoberturas(coberturasList?.map(c => ({ valor: c.id, etiqueta: c.nombre })) || [])
      }
    } catch (err) {
      console.error('Error al cargar catálogos:', err)
    }
  }

  const cargarPolizas = async (filtrosAdicionales = {}) => {
    setCargando(true)
    try {
      const parametros = {
        ...paginacion,
        ...filtrosAdicionales
      }
      const respuesta = await obtenerPolizas(parametros)
      if (respuesta.exito) {
        setPolizas(respuesta.data || [])
        setTotalPaginas(respuesta.paginacion?.cantidadPaginas || 1)
      } else {
        mostrarError(respuesta.mensaje, respuesta.errores || [])
      }
    } catch (err) {
      mostrarError(err.mensaje || 'Error al cargar pólizas', err.errores || [])
    } finally {
      setCargando(false)
    }
  }

  const manejarCambioFiltro = (e) => {
    const { name, value } = e.target
    setFiltros(prev => ({ ...prev, [name]: value }))
  }

  const buscarPolizas = async () => {
    setBuscando(true)
    const nuevaPaginacion = { pagina: 1, cantidadRegistros: paginacion.cantidadRegistros }
    setPaginacion(nuevaPaginacion)
    
    // Construir parámetros para el backend
    const parametros = {
      ...nuevaPaginacion
    }
    
    // Agregar solo filtros con valor
    if (filtros.numeroPoliza) parametros.numeroPoliza = filtros.numeroPoliza
    if (filtros.tipoPolizaId) parametros.tipoPolizaId = parseInt(filtros.tipoPolizaId)
    if (filtros.fechaVencimiento) parametros.fechaVencimiento = filtros.fechaVencimiento
    if (filtros.cedulaAsegurado) parametros.cedulaAsegurado = filtros.cedulaAsegurado
    if (filtros.nombre) parametros.nombre = filtros.nombre
    if (filtros.primerApellido) parametros.primerApellido = filtros.primerApellido
    if (filtros.segundoApellido) parametros.segundoApellido = filtros.segundoApellido
    
    try {
      const respuesta = await obtenerPolizas(parametros)
      if (respuesta.exito) {
        setPolizas(respuesta.data || [])
        setTotalPaginas(respuesta.paginacion?.cantidadPaginas || 1)
      } else {
        mostrarError(respuesta.mensaje, respuesta.errores || [])
      }
    } catch (err) {
      mostrarError(err.mensaje || 'Error al buscar pólizas', err.errores || [])
    } finally {
      setBuscando(false)
    }
  }

  const limpiarFiltros = () => {
    setFiltros(filtrosIniciales)
    setPaginacion(prev => ({ ...prev, pagina: 1 }))
    cargarPolizas()
  }

  const abrirModalCrear = () => {
    setPolizaActual(polizaInicial)
    setModoEdicion(false)
    setModalVisible(true)
    setErrorFormulario('')
  }

  const abrirModalEditar = (poliza) => {
    setPolizaActual({
      ...poliza,
      tipoPolizaId: poliza.tipoPolizaId || poliza.tipoPoliza?.id || '',
      estadoPolizaId: poliza.estadoPolizaId || poliza.estadoPoliza?.id || '',
      tipoCoberturaId: poliza.tipoCoberturaId || poliza.tipoCobertura?.id || '',
      fechaVencimiento: poliza.fechaVencimiento ? poliza.fechaVencimiento.split('T')[0] : '',
      fechaEmision: poliza.fechaEmision ? poliza.fechaEmision.split('T')[0] : '',
      fechaInclusion: poliza.fechaInclusion ? poliza.fechaInclusion.split('T')[0] : '',
      periodo: poliza.periodo ? poliza.periodo.split('T')[0] : ''
    })
    setModoEdicion(true)
    setModalVisible(true)
    setErrorFormulario('')
  }

  const intentarCerrarModal = () => {
    const hayCambios = Object.keys(polizaInicial).some(
      key => polizaActual[key] !== polizaInicial[key] && polizaActual[key] !== ''
    )
    if (hayCambios) {
      setConfirmarCerrarModal(true)
    } else {
      cerrarModal()
    }
  }

  const cerrarModal = () => {
    setModalVisible(false)
    setPolizaActual(polizaInicial)
    setErrorFormulario('')
    setConfirmarCerrarModal(false)
  }

  const manejarCambio = (e) => {
    const { name, value } = e.target
    setPolizaActual(prev => ({ ...prev, [name]: value }))
  }

  const intentarGuardar = (e) => {
    e.preventDefault()
    setConfirmarGuardar(true)
  }

  const guardarPoliza = async () => {
    setGuardando(true)
    setErrorFormulario('')
    setConfirmarGuardar(false)

    // Preparar datos para enviar
    const datosEnviar = {
      ...polizaActual,
      montoAsegurado: parseFloat(polizaActual.montoAsegurado) || 0,
      prima: parseFloat(polizaActual.prima) || 0
    }

    try {
      let respuesta
      if (modoEdicion) {
        respuesta = await actualizarPoliza(polizaActual.id, datosEnviar)
      } else {
        respuesta = await crearPoliza(datosEnviar)
      }

      if (respuesta.exito) {
        notificacionExito(modoEdicion ? 'Póliza actualizada correctamente' : 'Póliza creada correctamente')
        cerrarModal()
        cargarPolizas()
      } else {
        mostrarError(respuesta.mensaje, respuesta.errores || [])
      }
    } catch (err) {
      mostrarError(err.mensaje || 'Error al guardar póliza', err.errores || [])
    } finally {
      setGuardando(false)
    }
  }

  const confirmarEliminacion = async () => {
    if (!confirmarEliminar) return
    
    setGuardando(true)
    try {
      const respuesta = await eliminarPoliza(confirmarEliminar.id)
      if (respuesta.exito) {
        notificacionExito('Póliza eliminada correctamente')
        cargarPolizas()
      } else {
        mostrarError(respuesta.mensaje, respuesta.errores || [])
      }
    } catch (err) {
      mostrarError(err.mensaje || 'Error al eliminar póliza', err.errores || [])
    } finally {
      setGuardando(false)
      setConfirmarEliminar(null)
    }
  }

  return (
    <Layout>
      <div className="page-header">
        <h1 className="page-title">Gestión de Pólizas</h1>
        <ButtonComponent texto="+ Nueva Póliza" onClick={abrirModalCrear} />
      </div>

      <AcordeonFiltros
        titulo="Filtros de Búsqueda"
        inicialmenteAbierto={false}
        onBuscar={buscarPolizas}
        onLimpiar={limpiarFiltros}
        cargando={buscando}
      >
        <FiltrosPoliza
          filtros={filtros}
          alCambiar={manejarCambioFiltro}
          tiposPoliza={tiposPoliza}
        />
      </AcordeonFiltros>

      <div className="card">
        <TablaComponent
          columnas={columnas}
          datos={polizas}
          cargando={cargando}
          mensajeVacio="No hay pólizas registradas"
          acciones={(fila) => (
            <>
              <ButtonComponent
                variante="icono-editar"
                icono={<IconoEditar />}
                onClick={() => abrirModalEditar(fila)}
                titulo="Editar"
              />
              <ButtonComponent
                variante="icono-eliminar"
                icono={<IconoEliminar />}
                onClick={() => setConfirmarEliminar(fila)}
                titulo="Eliminar"
              />
            </>
          )}
        />
        
        <PaginacionComponent
          paginaActual={paginacion.pagina}
          totalPaginas={totalPaginas}
          onCambiarPagina={(nuevaPagina) => setPaginacion(prev => ({ ...prev, pagina: nuevaPagina }))}
        />
      </div>

      {/* Modal Crear/Editar */}
      <ModalComponent
        visible={modalVisible}
        titulo={modoEdicion ? 'Editar Póliza' : 'Nueva Póliza'}
        onCerrar={intentarCerrarModal}
        footer={
          <>
            <ButtonComponent 
              texto="Cancelar"
              variante="secundario"
              onClick={intentarCerrarModal}
            />
            <ButtonComponent 
              texto={guardando ? 'Guardando...' : 'Guardar'} 
              onClick={intentarGuardar}
              deshabilitado={guardando}
            />
          </>
        }
      >
        <form onSubmit={intentarGuardar}>
          <FormularioPoliza
            poliza={polizaActual}
            alCambiar={manejarCambio}
            tiposPoliza={tiposPoliza}
            estadosPoliza={estadosPoliza}
            coberturas={coberturas}
            error={errorFormulario}
          />
        </form>
      </ModalComponent>

      {/* Modal Confirmar Guardar */}
      <ModalConfirmacion
        visible={confirmarGuardar}
        titulo={modoEdicion ? '¿Actualizar póliza?' : '¿Crear póliza?'}
        mensaje={modoEdicion 
          ? `¿Está seguro que desea actualizar la póliza ${polizaActual.numeroPoliza}?`
          : '¿Está seguro que desea crear esta nueva póliza?'
        }
        onConfirmar={guardarPoliza}
        onCancelar={() => setConfirmarGuardar(false)}
        textoBtnConfirmar={modoEdicion ? 'Actualizar' : 'Crear'}
        tipo="pregunta"
        cargando={guardando}
      />

      {/* Modal Confirmar Cerrar */}
      <ModalConfirmacion
        visible={confirmarCerrarModal}
        titulo="¿Descartar cambios?"
        mensaje="Tiene cambios sin guardar. ¿Está seguro que desea cerrar sin guardar?"
        onConfirmar={cerrarModal}
        onCancelar={() => setConfirmarCerrarModal(false)}
        textoBtnConfirmar="Descartar"
        tipo="advertencia"
      />

      {/* Modal Confirmar Eliminación */}
      <ModalConfirmacion
        visible={!!confirmarEliminar}
        titulo="¿Eliminar póliza?"
        mensaje={`¿Está seguro que desea eliminar la póliza ${confirmarEliminar?.numeroPoliza}? Esta acción no se puede deshacer.`}
        onConfirmar={confirmarEliminacion}
        onCancelar={() => setConfirmarEliminar(null)}
        textoBtnConfirmar="Eliminar"
        tipo="peligro"
        cargando={guardando}
      />

      {/* Modal Error */}
      <ModalError
        visible={modalErrorVisible}
        mensaje={mensajeError}
        errores={listaErrores}
        onCerrar={cerrarModalError}
      />
    </Layout>
  )
}

export default Poliza
