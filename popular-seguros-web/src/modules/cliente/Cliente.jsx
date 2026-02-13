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
import FormularioCliente from './components/FormularioCliente'
import FiltrosCliente from './components/FiltrosCliente'
import { ClienteModel, FiltrosClienteModel } from './models/ClienteModel'
import { obtenerClientes, crearCliente, actualizarCliente, eliminarCliente } from './services/clienteService'
import { notificacionExito } from '../../services/notificacionService'
import '../../styles/layout.css'

const clienteInicial = new ClienteModel()
const filtrosIniciales = new FiltrosClienteModel()

const columnas = [
    { titulo: 'Cédula', campo: 'cedulaAsegurado' },
    { titulo: 'Nombre', campo: 'nombre' },
    { titulo: 'Primer Apellido', campo: 'primerApellido' },
    { titulo: 'Segundo Apellido', campo: 'segundoApellido' },
    { titulo: 'Tipo Persona', campo: 'tipoPersona' },
    { titulo: 'Fecha Nacimiento', campo: 'fechaNacimiento', render: (valor) => valor ? new Date(valor).toLocaleDateString('es-CR') : '-' }
]

export const Cliente = () => {
    const [clientes, setClientes] = useState([])
    const [cargando, setCargando] = useState(false)
    const [paginacion, setPaginacion] = useState({ pagina: 1, cantidadRegistros: 10 })
    const [totalPaginas, setTotalPaginas] = useState(1)
    const [filtros, setFiltros] = useState(new FiltrosClienteModel())
    const [buscando, setBuscando] = useState(false)
    const [modalVisible, setModalVisible] = useState(false)
    const [modoEdicion, setModoEdicion] = useState(false)
    const [clienteActual, setClienteActual] = useState(new ClienteModel())
    const [guardando, setGuardando] = useState(false)
    const [errorFormulario, setErrorFormulario] = useState('')
    const [modalErrorVisible, setModalErrorVisible] = useState(false)
    const [mensajeError, setMensajeError] = useState('')
    const [listaErrores, setListaErrores] = useState([])
    const [confirmarEliminar, setConfirmarEliminar] = useState(null)
    const [confirmarCerrarModal, setConfirmarCerrarModal] = useState(false)
    const [confirmarGuardar, setConfirmarGuardar] = useState(false)

    useEffect(() => {
        cargarClientes()
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

    const cargarClientes = async (filtrosAdicionales = {}) => {
        setCargando(true)
        try {
            const parametros = {
                ...paginacion,
                ...filtrosAdicionales
            }
            const respuesta = await obtenerClientes(parametros)
            if (respuesta.exito) {
                setClientes(respuesta.data || [])
                setTotalPaginas(respuesta.paginacion?.cantidadPaginas || 1)
            } else {
                mostrarError(respuesta.mensaje)
            }
        } catch (err) {
            mostrarError(err.mensaje || 'Error al cargar clientes', err.errores || [])
        } finally {
            setCargando(false)
        }
    }

    const manejarCambioFiltro = (e) => {
        const { name, value } = e.target
        setFiltros(prev => ({ ...prev, [name]: value }))
    }

    const buscarClientes = async () => {
        setBuscando(true)
        const nuevaPaginacion = { pagina: 1, cantidadRegistros: paginacion.cantidadRegistros }
        setPaginacion(nuevaPaginacion)

        const parametros = {
            ...nuevaPaginacion
        }

        if (filtros.cedulaAsegurado) parametros.cedulaAsegurado = filtros.cedulaAsegurado
        if (filtros.nombre) parametros.nombre = filtros.nombre
        if (filtros.primerApellido) parametros.primerApellido = filtros.primerApellido
        if (filtros.segundoApellido) parametros.segundoApellido = filtros.segundoApellido
        if (filtros.tipoPersona) parametros.tipoPersona = filtros.tipoPersona

        try {
            const respuesta = await obtenerClientes(parametros)
            if (respuesta.exito) {
                setClientes(respuesta.data || [])
                setTotalPaginas(respuesta.paginacion?.cantidadPaginas || 1)
            } else {
                mostrarError(respuesta.mensaje)
            }
        } catch (err) {
            mostrarError(err.mensaje || 'Error al buscar clientes', err.errores || [])
        } finally {
            setBuscando(false)
        }
    }

    const limpiarFiltros = () => {
        setFiltros(filtrosIniciales)
        setPaginacion(prev => ({ ...prev, pagina: 1 }))
        cargarClientes()
    }

    const abrirModalCrear = () => {
        setClienteActual(clienteInicial)
        setModoEdicion(false)
        setModalVisible(true)
        setErrorFormulario('')
    }

    const abrirModalEditar = (cliente) => {
        setClienteActual({
            ...cliente,
            fechaNacimiento: cliente.fechaNacimiento ? cliente.fechaNacimiento.split('T')[0] : ''
        })
        setModoEdicion(true)
        setModalVisible(true)
        setErrorFormulario('')
    }

    const intentarCerrarModal = () => {
        const hayCambios = Object.keys(clienteInicial).some(
            key => clienteActual[key] !== clienteInicial[key] && clienteActual[key] !== ''
        )
        if (hayCambios) {
            setConfirmarCerrarModal(true)
        } else {
            cerrarModal()
        }
    }

    const cerrarModal = () => {
        setModalVisible(false)
        setClienteActual(clienteInicial)
        setErrorFormulario('')
        setConfirmarCerrarModal(false)
    }

    const manejarCambio = (e) => {
        const { name, value } = e.target
        setClienteActual(prev => ({ ...prev, [name]: value }))
    }

    const intentarGuardar = (e) => {
        e.preventDefault()
        setConfirmarGuardar(true)
    }

    const guardarCliente = async () => {
        setGuardando(true)
        setErrorFormulario('')
        setConfirmarGuardar(false)

        try {
            let respuesta
            if (modoEdicion) {
                respuesta = await actualizarCliente(clienteActual.cedulaAsegurado, clienteActual)
            } else {
                respuesta = await crearCliente(clienteActual)
            }

            if (respuesta.exito) {
                notificacionExito(modoEdicion ? 'Cliente actualizado correctamente' : 'Cliente creado correctamente')
                cerrarModal()
                cargarClientes()
            } else {
                mostrarError(respuesta.mensaje, respuesta.errores || [])
            }
        } catch (err) {
            mostrarError(err.mensaje || 'Error al guardar cliente', err.errores || [])
        } finally {
            setGuardando(false)
        }
    }

    const confirmarEliminacion = async () => {
        if (!confirmarEliminar) return

        setGuardando(true)
        try {
            const respuesta = await eliminarCliente(confirmarEliminar.cedulaAsegurado)
            if (respuesta.exito) {
                notificacionExito('Cliente eliminado correctamente')
                cargarClientes()
            } else {
                mostrarError(respuesta.mensaje, respuesta.errores || [])
            }
        } catch (err) {
            mostrarError(err.mensaje || 'Error al eliminar cliente', err.errores || [])
        } finally {
            setGuardando(false)
            setConfirmarEliminar(null)
        }
    }

    return (
        <Layout>
            <div className="page-header">
                <h1 className="page-title">Gestión de Clientes</h1>
                <ButtonComponent texto="+ Nuevo Cliente" onClick={abrirModalCrear} />
            </div>

            <AcordeonFiltros
                titulo="Filtros de Búsqueda"
                inicialmenteAbierto={false}
                onBuscar={buscarClientes}
                onLimpiar={limpiarFiltros}
                cargando={buscando}
            >
                <FiltrosCliente
                    filtros={filtros}
                    alCambiar={manejarCambioFiltro}
                />
            </AcordeonFiltros>

            <div className="card">
                <TablaComponent
                    columnas={columnas}
                    datos={clientes}
                    cargando={cargando}
                    mensajeVacio="No hay clientes registrados"
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
                titulo={modoEdicion ? 'Editar Cliente' : 'Nuevo Cliente'}
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
                    <FormularioCliente
                        cliente={clienteActual}
                        modoEdicion={modoEdicion}
                        alCambiar={manejarCambio}
                        error={errorFormulario}
                    />
                </form>
            </ModalComponent>

            <ModalConfirmacion
                visible={confirmarGuardar}
                titulo={modoEdicion ? '¿Actualizar cliente?' : '¿Crear cliente?'}
                mensaje={modoEdicion
                    ? `¿Está seguro que desea actualizar los datos del cliente ${clienteActual.nombre}?`
                    : '¿Está seguro que desea crear este nuevo cliente?'
                }
                onConfirmar={guardarCliente}
                onCancelar={() => setConfirmarGuardar(false)}
                textoBtnConfirmar={modoEdicion ? 'Actualizar' : 'Crear'}
                tipo="pregunta"
                cargando={guardando}
            />

            <ModalConfirmacion
                visible={confirmarCerrarModal}
                titulo="¿Descartar cambios?"
                mensaje="Tiene cambios sin guardar. ¿Está seguro que desea cerrar sin guardar?"
                onConfirmar={cerrarModal}
                onCancelar={() => setConfirmarCerrarModal(false)}
                textoBtnConfirmar="Descartar"
                tipo="advertencia"
            />

            <ModalConfirmacion
                visible={!!confirmarEliminar}
                titulo="¿Eliminar cliente?"
                mensaje={`¿Está seguro que desea eliminar al cliente ${confirmarEliminar?.nombre} ${confirmarEliminar?.primerApellido}? Esta acción no se puede deshacer.`}
                onConfirmar={confirmarEliminacion}
                onCancelar={() => setConfirmarEliminar(null)}
                textoBtnConfirmar="Eliminar"
                tipo="peligro"
                cargando={guardando}
            />

            <ModalError
                visible={modalErrorVisible}
                mensaje={mensajeError}
                errores={listaErrores}
                onCerrar={cerrarModalError}
            />
        </Layout>
    )
}

export default Cliente
