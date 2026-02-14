import React, { useState, useEffect, useCallback } from 'react'
import InputComponent from '../../../components/InputComponent'
import SelectComponent from '../../../components/SelectComponent'
import ModalComponent from '../../../components/ModalComponent'
import FormularioCliente from '../../cliente/components/FormularioCliente'
import { buscarClientePorCedula } from '../../cliente/services/clienteService'
import { ClienteModel } from '../../cliente/models/ClienteModel'

const FormularioPoliza = ({ 
  poliza, 
  alCambiar, 
  tiposPoliza = [],
  estadosPoliza = [],
  coberturas = [],
  error 
}) => {
  const [clienteEncontrado, setClienteEncontrado] = useState(null)
  const [buscandoCliente, setBuscandoCliente] = useState(false)
  const [modalClienteVisible, setModalClienteVisible] = useState(false)

  const buscarCliente = useCallback(async (cedula) => {
    if (!cedula || cedula.length < 3) {
      setClienteEncontrado(null)
      return
    }

    setBuscandoCliente(true)
    try {
      const respuesta = await buscarClientePorCedula(cedula)
      if (respuesta && respuesta.data) {
        setClienteEncontrado(respuesta.data)
      } else {
        setClienteEncontrado(null)
      }
    } catch {
      setClienteEncontrado(null)
    } finally {
      setBuscandoCliente(false)
    }
  }, [])

  useEffect(() => {
    const timeoutId = setTimeout(() => {
      buscarCliente(poliza.cedulaAsegurado)
    }, 500)

    return () => clearTimeout(timeoutId)
  }, [poliza.cedulaAsegurado, buscarCliente])

  const obtenerNombreCompleto = (cliente) => {
    if (!cliente) return ''
    return `${cliente.nombre} ${cliente.primerApellido} ${cliente.segundoApellido || ''}`.trim()
  }

  return (
    <>
      {error && <div className="mensaje-error">{error}</div>}
      <div className="form-grid">
        <InputComponent
          etiqueta="Número Póliza"
          nombre="numeroPoliza"
          valor={poliza.numeroPoliza}
          alCambiar={alCambiar}
          requerido
          placeholder="Ej: POL-001"
          maxLength={20}
        />
        <SelectComponent
          etiqueta="Tipo Póliza"
          nombre="tipoPolizaId"
          valor={poliza.tipoPolizaId}
          alCambiar={alCambiar}
          opciones={tiposPoliza}
          requerido
        />
        <div className="campo-con-sugerencia">
          <InputComponent
            etiqueta="Cédula Asegurado"
            nombre="cedulaAsegurado"
            valor={poliza.cedulaAsegurado}
            alCambiar={alCambiar}
            requerido
            placeholder="Ingrese la cédula"
            maxLength={20}
          />
          {buscandoCliente && (
            <div className="sugerencia-cliente buscando">
              Buscando...
            </div>
          )}
          {!buscandoCliente && clienteEncontrado && (
            <div 
              className="sugerencia-cliente encontrado"
              onClick={() => setModalClienteVisible(true)}
            >
              <span className="icono-check">✓</span>
              {obtenerNombreCompleto(clienteEncontrado)}
              <span className="ver-detalle">Ver detalle</span>
            </div>
          )}
          {!buscandoCliente && !clienteEncontrado && poliza.cedulaAsegurado && poliza.cedulaAsegurado.length >= 3 && (
            <div className="sugerencia-cliente no-encontrado">
              Cliente no encontrado
            </div>
          )}
        </div>
        <InputComponent
          etiqueta="Monto Asegurado"
          nombre="montoAsegurado"
          tipo="number"
          valor={poliza.montoAsegurado}
          alCambiar={alCambiar}
          requerido
          placeholder="0.00"
        />
        <InputComponent
          etiqueta="Fecha Emisión"
          nombre="fechaEmision"
          tipo="date"
          valor={poliza.fechaEmision}
          alCambiar={alCambiar}
          requerido
        />
        <InputComponent
          etiqueta="Fecha Vencimiento"
          nombre="fechaVencimiento"
          tipo="date"
          valor={poliza.fechaVencimiento}
          alCambiar={alCambiar}
          requerido
        />
        <SelectComponent
          etiqueta="Cobertura"
          nombre="tipoCoberturaId"
          valor={poliza.tipoCoberturaId}
          alCambiar={alCambiar}
          opciones={coberturas}
          requerido
        />
        <SelectComponent
          etiqueta="Estado Póliza"
          nombre="estadoPolizaId"
          valor={poliza.estadoPolizaId}
          alCambiar={alCambiar}
          opciones={estadosPoliza}
          requerido
        />
        <InputComponent
          etiqueta="Prima"
          nombre="prima"
          tipo="number"
          valor={poliza.prima}
          alCambiar={alCambiar}
          requerido
          placeholder="0.00"
        />
        <InputComponent
          etiqueta="Periodo"
          nombre="periodo"
          tipo="date"
          valor={poliza.periodo}
          alCambiar={alCambiar}
          requerido
        />
        <InputComponent
          etiqueta="Fecha Inclusión"
          nombre="fechaInclusion"
          tipo="date"
          valor={poliza.fechaInclusion}
          alCambiar={alCambiar}
          requerido
        />
        <InputComponent
          etiqueta="Aseguradora"
          nombre="aseguradora"
          valor={poliza.aseguradora}
          alCambiar={alCambiar}
          requerido
          placeholder="Nombre de la aseguradora"
          maxLength={100}
        />
      </div>

      <ModalComponent
        visible={modalClienteVisible}
        onCerrar={() => setModalClienteVisible(false)}
        titulo="Información del Asegurado"
        footer={
          <button 
            className="btn-secondary-custom"
            onClick={() => setModalClienteVisible(false)}
          >
            Cerrar
          </button>
        }
      >
        <FormularioCliente
          cliente={clienteEncontrado || new ClienteModel()}
          soloLectura={true}
          alCambiar={() => {}}
          error=""
        />
      </ModalComponent>
    </>
  )
}

export default FormularioPoliza
